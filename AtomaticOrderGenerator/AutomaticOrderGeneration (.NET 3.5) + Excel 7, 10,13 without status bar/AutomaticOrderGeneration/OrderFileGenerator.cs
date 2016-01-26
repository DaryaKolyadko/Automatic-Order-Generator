using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Interop = Microsoft.Office.Interop;

namespace AutomaticOrderGeneration
{
    class OrderFileGenerator
    {
        public enum Filials
        {
            Minsk = 0,
            MarGorka,
            Volkovisk,
            Luninec,
            Smorgon
        }

        public static String[] filialNames = { "Минск", "Марьина Горка", "Волковыск", "Лунинец", "Сморгонь" };
        public static DataTable registerPart;
        public static int filialRegisterIndent = 0;
        public static Interop.Excel.Application ExcelApp = null;
        public static Interop.Excel.Workbook Register = null;
        public static String resultFileName;

        private static System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-GB");
        private static String[] filialShortNames = { "Мн", "Мг", "В", "Л", "С" };

        private static String[] filialCodes = { "153001", "153000", "153011", "153101", "153111" };
        private static String[] filialAdditionalCodes = { "", "153001", "", "150501", "152111" };
        private static String[] filialSpecialAccounts = { "", "3140186450061", "", "3140100020021", "" }; // если correspondentAccount совпадает с этим кодом, то используем filialAdditionalCodes
        private static String[] indents = {"{0, 28:0.00}", "{0, 21:0.00}"}; // отступ вправо, отступ влево
        private static String standard = "{0, 26:0.00}"; // без отступов
        //private static String[] indents = { "{0, 28}", "{0, 21}" }; // отступ вправо, отступ влево
        //private static String standard = "{0, 26}"; // без отступов
        private static int previousWasMoved = 0;
        private static String indent; // смещение индексов филиалов в файле реестра (относительно массива имен)
        private static int indexGeneralSum = 1;
        private static String strGeneralSum = "Сумма ордера";
        private static Dictionary<String, double> generalSumDictionary; // код ордера, общая сумма
        private static String excelCellStart = "A2";
        private static String excelCellEnd = "A";
        private static String[] paperAccounts = {"3140000000101"}; // с этого счета приходят платежи со Сморгони и Волковыска
        // они ничем не отличаются, кроме номера документа, и их нет в реестре. поэтому их не ищем даже в реестре
        // они есть только на бумагах
        private static List<String>[] paperFilials = {new List<string>(){
                                                                        filialNames[(int)Filials.Volkovisk], 
                                                                        filialNames[(int)Filials.Smorgon]}};
        private static List<String>[]paperAdditionalCodes = {new List<string>() { "152011", "152111" }};
        public static void SaveOrderIntoFile(string prologue, List<PaymentRecord> content, List<List<int>> filialMarks,
            string epilogue)
        {
            List<String> lines = new List<String>();

            int count = content.Count;

            for (int i = 0; i < count; i++)
            {
                if (filialMarks[i].Count == 1)
                {
                    int filialIndex = Array.IndexOf(filialSpecialAccounts, content[i].correspondentAccount);
                    int paperAccountIndex = Array.IndexOf(paperAccounts, content[i].correspondentAccount);

                    if (filialIndex != -1)
                    {
                        lines.Add(ChangedPaymentRecord(content[i], filialIndex, true, paperAccountIndex));
                    }
                    else
                        lines.Add(ChangedPaymentRecord(content[i], filialMarks[i].First(), false, paperAccountIndex));
                }
                else
                {
                    int filialCount = filialMarks[i].Count;
                    int paperAccountIndex = Array.IndexOf(paperAccounts, content[i].correspondentAccount);

                    if (paperAccountIndex > -1)
                    {
                        for (int j = 0; j < filialCount; j++)
                            if (j == 0)
                                lines.Add(ChangedPaymentRecord(content[i], filialMarks[i][j],  true, true, false, paperAccountIndex));
                            else
                                lines.Add(ChangedPaymentRecord(content[i], filialMarks[i][j],  true, false, false, paperAccountIndex));
                    }
                    else
                    {
                        for (int j = 0; j < filialCount; j++)
                        {
                            if (j == 0)
                                lines.Add(ChangedPaymentRecord(content[i], filialMarks[i][j], true, true, false));
                            else
                                lines.Add(ChangedPaymentRecord(content[i], filialMarks[i][j], true, false, false));
                        }
                    }
                }
            }

            foreach (KeyValuePair<String, double> val in generalSumDictionary)
            {
                if(val.Value != 0)
                {
                    Clipboard.SetText(val.Key);
                    MessageBox.Show("Обнаружено несоответствие. Не совпадают итоговая сумма и сумма по филиалам:\nНомер док. " + 
                        val.Key, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            String allLines = "";

            foreach (String line in lines)
                allLines += line + "\r\n";

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.RestoreDirectory = true;
            dialog.FileName = MainForm.fileName.Remove(MainForm.fileName.Length - 4) + "_П_";
            resultFileName = dialog.FileName;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            File.WriteAllText(dialog.FileName, prologue, Encoding.Default);
            File.AppendAllText(dialog.FileName, allLines, Encoding.Default);
            File.AppendAllText(dialog.FileName, epilogue, Encoding.Default);
        }


        private static String ChangedPaymentRecord(PaymentRecord record, int filial, bool additionalCode)
        {
            return ChangedPaymentRecord(record, filial, false, false, additionalCode, -1);
        }

        private static String ChangedPaymentRecord(PaymentRecord record, int filial, bool additionalCode, int paperAccountIndex)
        {
            return ChangedPaymentRecord(record, filial, false, false, additionalCode, paperAccountIndex);
        }

        private static String ChangedPaymentRecord(PaymentRecord record, int filial, bool complex, bool firstInComplex,
            bool additionalCode)
        {
            return ChangedPaymentRecord(record, filial, complex, firstInComplex, additionalCode, -1);
        }

        private static String ChangedPaymentRecord(PaymentRecord record, int filial, bool complex, bool firstInComplex, 
            bool additionalCode, int paperAccountIndex)
        {
            string result = String.Format("{0, -12}{1, 7}{2, 3}",
                record.documentDate.ToString("dd.MM.yyyy"), record.documentNumber,
                record.operationCode);

            if (additionalCode)
            {
                result += String.Format("{0, 10}", filialAdditionalCodes[filial]);
            }
            else
            {
                if (paperAccountIndex != -1 && CheckPaperFilial(paperAccountIndex, filial, record.documentNumber))
                    result += String.Format("{0, 10}", paperAdditionalCodes[paperAccountIndex][paperFilials[paperAccountIndex].IndexOf(filialNames[filial])]);
                else    
                    result += String.Format("{0, 10}", filialCodes[filial]);
            }

            result += String.Format("{0}{1, 15}{2, 29}", record.correspondentCode, 
                record.correspondentAccount, record.ratingDebit);

            if ((filial == (int)Filials.Minsk && !complex) || additionalCode)
            {
                if(generalSumDictionary.Keys.Contains(record.documentNumber))
                {
                    generalSumDictionary[record.documentNumber] -= record.ratingCredit;
                }
                
                result += String.Format(cultureInfo, standard, record.ratingCredit);

                if (previousWasMoved == 1)
                    previousWasMoved = 0;

                return result;
            }

            if (firstInComplex || !complex)
            {
                indent = indents[previousWasMoved];
                previousWasMoved++;
                previousWasMoved %= 2;
            }

            if (paperAccountIndex != -1)
            {
                if (!generalSumDictionary.ContainsKey(record.documentNumber)) // если сумма с нескольких филиалов
                    generalSumDictionary.Add(record.documentNumber, 0); // записей на филиалы Сморгонь и Волковыск в реестре нету
            }
            else
            {
                double credit = FindCreditForRecord(record, filial);

                if (credit == -1)
                {
                    Clipboard.SetText(record.documentNumber);
                    throw new Exception("Обнаружено несоответствие. Не найдена запись в реестре для:\nНомер док.: " +
                                        record.documentNumber + "\nФилиал: " + filialNames[filial]);
                }
                else
                {
                    record.ratingCredit = credit;
                }
            }
            result += String.Format(cultureInfo, indent + " {1}", record.ratingCredit, filialShortNames[filial]);
            
            return result;
        }


        public static bool ConnectToExcelRegisterAndGetInformation(string dateToFind)
        {
            generalSumDictionary = new Dictionary<String, double>(); // код ордера, общая сумма

            // Создаём приложение
            ExcelApp = new Interop.Excel.Application();
            ExcelApp.Visible = false;
            // Открываем книгу           
            Register = ExcelApp.Workbooks.Open(Properties.Settings.Default.RegisterPath,
                0, true, 5, "", "", false, Interop.Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
            
            // Выбираем таблицу(лист)
            Interop.Excel.Worksheet RegisterSheet = (Interop.Excel.Worksheet)Register.Worksheets[Properties.Settings.Default.RegisterSheetName];
            registerPart = new DataTable();
         
            // Выполняем поиск диапазона нужных строк (по дате выписки) и заполняем данными registerPart
            Interop.Excel.Range range = RegisterSheet.get_Range(excelCellStart, excelCellEnd + RegisterSheet.UsedRange.Rows.Count);
            Interop.Excel.Range firstFind = null;
            Interop.Excel.Range currentFind;
            Interop.Excel.Range usedRange = RegisterSheet.UsedRange;

            int usedColumnsCount = RegisterSheet.UsedRange.Columns.Count;
            Interop.Excel.Range columnNamesRange = RegisterSheet.get_Range("A1", (char)('A' + usedColumnsCount -1) + "1");//"A" + usedColumnsCount);

            // считываем имена столбцов из первой строки
            bool found = false;
            bool found_1 = false;

            for (int i = 2; i <= usedColumnsCount; i++)
            {
                String columnName = String.Empty;

                try
                {
                    columnName = (columnNamesRange.Cells[1, i] as Interop.Excel.Range).Value2.ToString();
                }
                catch (NullReferenceException)
                {
                    break;
                }

                if (!found)
                {
                    if (columnName.Contains(filialNames.First()))
                    {
                        found = true;
                        filialRegisterIndent = i;
                    }
                    
                    if(!found_1)
                    {
                        if(columnName.Contains(strGeneralSum))
                        {
                            found_1 = true;
                        }
                    }
                }
                registerPart.Columns.Add(new DataColumn(columnName));
            }

            currentFind = range.Find(dateToFind, Type.Missing, Interop.Excel.XlFindLookIn.xlValues, Interop.Excel.XlLookAt.xlPart,
                Interop.Excel.XlSearchOrder.xlByRows, Interop.Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);

            while (currentFind != null)
            {
                // Сохраняем первый найденный  
                if (firstFind == null)
                {
                    firstFind = currentFind;
                }
                // Если мы не сдвинулись в поиске, значит это конец поиска 
                else if (currentFind.get_Address(Interop.Excel.XlReferenceStyle.xlA1)
                      == firstFind.get_Address(Interop.Excel.XlReferenceStyle.xlA1))
                {
                    break;
                }

                // помещаем данные в registerPart
                List<String> paramsList = new List<String>();
                int row = currentFind.Row;
                int columnsNeed = filialRegisterIndent + filialNames.Length - 1;

                for (int i = 2; i <= columnsNeed; i++)
                {
                    Object value = (usedRange.Cells[row, i] as Interop.Excel.Range).Value;

                    if (value == null)
                        paramsList.Add(String.Empty);
                    else
                        paramsList.Add(value.ToString());
                }

                generalSumDictionary.Add(paramsList[0], Convert.ToDouble(paramsList[indexGeneralSum], Program.cultureInfo));
                registerPart.Rows.Add(paramsList.ToArray());
                
                // ищем дальше, начиная с последней найденной ячейки
                currentFind = range.FindNext(currentFind);
            }

            if (firstFind == null)
                return false;

            filialRegisterIndent -= 2;

            return true;
        }


        private static double FindCreditForRecord(PaymentRecord record, int filial)
        {
            double credit = -1;
            DataRow[] rows = registerPart.Select("[" + registerPart.Columns[0].ColumnName + "] like '%" + record.documentNumber + "%'");

            try
            {
                if (rows.Length == 1)
                {
                    credit = Convert.ToDouble(rows.First()[filial + filialRegisterIndent]);
                    generalSumDictionary[record.documentNumber] -= credit;
                }
                else
                {
                    // один раз select как-то криво отработал. хз почему. поэтому добавила этот кусок. всегда только одна запись с таким номером документа
                    foreach (DataRow r in rows)
                    {
                        if (r[0].ToString() == record.documentNumber)
                        {
                            credit = Convert.ToDouble(r[filial + filialRegisterIndent]);
                            generalSumDictionary[record.documentNumber] -= credit;
                        }
                    }
                }
            }
            catch (Exception) { }
            
            return credit;
        }

        private static bool CheckPaperFilial(int paperAccountIndex, int filial, String documentNumber)
        {
            if (paperFilials[paperAccountIndex].Contains(filialNames[filial]))
                return true;
            StringBuilder filialsStr = new StringBuilder(paperFilials[paperAccountIndex].First());
            int paperFilialsCount = paperFilials[paperAccountIndex].Count;
            for (int i = 1; i < paperFilialsCount; i++)
                filialsStr.Append(", " + paperFilials[paperAccountIndex][i]);
            filialsStr.Append('.');
            throw new Exception("Обнаружено несоответствие для записи с номером документа " + documentNumber +
                ".\nКорресп. счету " + paperAccounts[paperAccountIndex] + " могут сооответствовать филиалы: " +
                                filialsStr);
        }
    }
}