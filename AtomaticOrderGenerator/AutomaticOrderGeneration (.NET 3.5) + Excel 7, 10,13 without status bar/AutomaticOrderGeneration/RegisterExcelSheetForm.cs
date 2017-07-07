using System;
using System.Windows.Forms;

namespace AutomaticOrderGeneration
{
    public partial class RegisterExcelSheetForm : Form
    {
        public RegisterExcelSheetForm()
        {
            InitializeComponent();
        }

        private void RegisterExcelSheetForm_Load(object sender, EventArgs e)
        {
            textBoxRegisterSheetName.Text = Properties.Settings.Default.RegisterSheetName;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RegisterSheetName = textBoxRegisterSheetName.Text;
            Properties.Settings.Default.Save();
        }
    }
}
