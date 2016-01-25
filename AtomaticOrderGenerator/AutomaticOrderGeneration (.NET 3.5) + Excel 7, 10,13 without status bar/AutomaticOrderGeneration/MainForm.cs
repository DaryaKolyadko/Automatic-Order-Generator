using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutomaticOrderGeneration
{
    public partial class MainForm : Form
    {
        private List<PaymentRecord> document;
        private string prologue;
        private string epilogue;
        private DataTable tableOrder;
        private string[] columnNames = {"Дата док.", "N док.", "Корреспондент код", 
                                       "Корреспондент счет", "Номинал кредит"};

        public static String fileName;

        public MainForm()
        {
            InitializeComponent();
            tableOrder = new DataTable();
           
            foreach (string name in columnNames)
            {
                tableOrder.Columns.Add(name, typeof(String));
            }
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "z:\\Выписка\\Сортировка";
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            dialog.Multiselect = false;

            StreamReader reader;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if((reader = new StreamReader(dialog.OpenFile(), Encoding.Default)) != null)
                {
                    fileName = dialog.FileName;

                    if (!TryWriteFileIntoTable(reader))
                        MessageBox.Show("Проверьте правильность выбранного файла", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool TryWriteFileIntoTable(StreamReader reader)
        {
            document = OrderFileParser.ParseOrderFile(reader, out prologue, out epilogue);

            if (document.Count == 0)
                return false;

            tableOrder.Clear();

            foreach (PaymentRecord record in document)
            {
                tableOrder.Rows.Add(record.documentDate.ToString("dd.MM.yyyy"), record.documentNumber,
                    record.correspondentCode, record.correspondentAccount, record.ratingCredit.ToString("N", Program.cultureInfo));
            }

            gridOrder.DataSource = null;
            gridOrder.Columns.Clear();
            gridOrder.DataSource = tableOrder;
            gridOrder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCheckBoxColumn column;

            foreach (DataGridViewColumn c in gridOrder.Columns)
                c.ReadOnly = true;
            
            foreach (String filial in OrderFileGenerator.filialNames)
            {
                column = new DataGridViewCheckBoxColumn();
                column.HeaderText = filial;
                column.Name = filial;
                column.HeaderCell.Style.ForeColor = Color.SlateBlue;
                column.HeaderCell.Style.Font = new Font(gridOrder.Font, FontStyle.Bold);
                gridOrder.Columns.Add(column);
            }
            
            return true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GenerateOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateOrder();
        }


        private void GenerateOrderButton_Click(object sender, EventArgs e)
        {
            GenerateOrder();
        }

        private void GenerateOrder()
        { 
            DialogResult result = DialogResult.None;
            bool everythingIsOk = true;

            if(gridOrder.ColumnCount == 0)
            {
                MessageBox.Show("Нет данных для генерации выписки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Properties.Settings.Default.RegisterPath.Equals(String.Empty))
            {
                MessageBox.Show("Не указан путь к реестру. Укажите его во вкладке Сервис->Настройки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                String date = tableOrder.Rows[0].Field<String>(columnNames.First());

                if (!OrderFileGenerator.ConnectToExcelRegisterAndGetInformation(date))
                {
                    result = MessageBox.Show("Возникли проблемы с файлом реестра. Не найдены записи на требуемую дату: " +  
                        date, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Runtime.InteropServices.COMException exc)
            {
                everythingIsOk = false;
                result = MessageBox.Show("Возникли проблемы с файлом реестра. Не найдена страница с именем " +
                    Properties.Settings.Default.RegisterSheetName + ". Проверьте имя страницы и файл реестра.\n" + exc.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (result == DialogResult.OK)
                    result = MessageBox.Show("Хотите продолжить генерацию выписки без импорта данных из реестра? " +
                       "(Т.е. если сумма одна на несколько филиалов, то она дублируется во всех филиалах, а ее " +
                       "изменение придется выполнять вручную)", "Предложение", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes || everythingIsOk)
            {
                List<List<int>> filialMarks = new List<List<int>>();

                int firstCheckBoxColumnIndex = gridOrder.Columns.IndexOf(gridOrder.Columns[OrderFileGenerator.filialNames.First()]);
                int columnCount = gridOrder.Columns.Count;
                int rowCount = gridOrder.Rows.Count;

                for (int i = 0; i < rowCount; i++)
                {
                    List<int> list = new List<int>();

                    for (int j = firstCheckBoxColumnIndex; j < columnCount; j++)
                    {
                        if (Convert.ToBoolean(gridOrder.Rows[i].Cells[j].Value))
                        {
                            list.Add(j - firstCheckBoxColumnIndex);
                        }
                    }

                    if (list.Count == 0)
                    {
                        list.Add((int)OrderFileGenerator.Filials.Minsk);
                    }

                    filialMarks.Add(list);
                }

                if (filialMarks.Count == 0)
                {
                    MessageBox.Show("Нет данных для генерации выписки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    OrderFileGenerator.SaveOrderIntoFile(prologue, document, filialMarks, epilogue);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if(OrderFileGenerator.Register != null)
                        OrderFileGenerator.Register.Close(false, false, false);
                    OrderFileGenerator.ExcelApp.Quit();
                    GC.Collect();
                }
            }
            else 
            {
                if (OrderFileGenerator.Register != null)
                    OrderFileGenerator.Register.Close(false, false, false);
                OrderFileGenerator.ExcelApp.Quit();
                GC.Collect();
            }
        }


        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автоматический генератор выписок\nВерсия 1.1\n\nНовое в этой версии: импорт данных из " +
                "реестра\n\n2015 г.", "О программе", MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }
        

        private void RegisterPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegiterPathForm form = new RegiterPathForm();
            form.ShowDialog();
        }


        private void RegisterSheetNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterExcelSheetForm form = new RegisterExcelSheetForm();
            form.ShowDialog();
        }


        private void gridOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell cell = gridOrder.CurrentCell as DataGridViewCheckBoxCell;

            if (cell != null)
            {
                cell.Value = cell.Value == null || !((bool)cell.Value);
                gridOrder.RefreshEdit();
                gridOrder.NotifyCurrentCellDirty(true);
            }
        }

        private void gridOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gridOrder.RefreshEdit();
        }
    }
}