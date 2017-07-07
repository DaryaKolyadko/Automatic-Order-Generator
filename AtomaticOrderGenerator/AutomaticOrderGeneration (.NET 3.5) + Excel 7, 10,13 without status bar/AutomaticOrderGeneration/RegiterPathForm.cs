using System;
using System.Windows.Forms;

namespace AutomaticOrderGeneration
{
    public partial class RegiterPathForm : Form
    {
        public RegiterPathForm()
        {
            InitializeComponent();
        }

        private void RegiterPathForm_Load(object sender, EventArgs e)
        {
            textBoxCurrentRegisterPath.Text = Properties.Settings.Default.RegisterPath;
        }

        private void ButtonChooseRegisterFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog registerFileDialog = new OpenFileDialog();

            if (Properties.Settings.Default.RegisterPath.Length > 0)
                registerFileDialog.InitialDirectory = Properties.Settings.Default.RegisterPath.Substring(0, Properties.Settings.Default.RegisterPath.LastIndexOf('\\'));
            
            registerFileDialog.Multiselect = false;

            if (registerFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileExtension = registerFileDialog.FileName.Substring(registerFileDialog.FileName.LastIndexOf('.'));
               
                if (fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx"))
                    textBoxCurrentRegisterPath.Text = registerFileDialog.FileName;
                else
                    MessageBox.Show("Проверьте правильность выбранного файла. Это должен быть файл с расширением .xls или .xlsx", 
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RegisterPath = textBoxCurrentRegisterPath.Text;
            Properties.Settings.Default.Save();
        }
    }
}