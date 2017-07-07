using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using static AutomaticOrderGeneration.Util.OrderFileGenerator;

namespace AutomaticOrderGeneration
{
    public partial class RegisterFilialsCodesForm : Form
    {
        public RegisterFilialsCodesForm()
        {
            InitializeComponent();
        }

        private void RegisterFilialsCodesForm_Load(object sender, EventArgs e)
        {
            StringCollection filialCodes = Properties.Settings.Default.FilialsCodes;
            textBoxMinskCode.Text = filialCodes[(int)Filials.Minsk];
            textBoxMarGorkaCode.Text = filialCodes[(int)Filials.MarGorka];
            textBoxVolkoviskCode.Text = filialCodes[(int)Filials.Volkovisk];
            textBoxLuninecCode.Text = filialCodes[(int)Filials.Luninec];
            textBoxSmorgonCode.Text = filialCodes[(int)Filials.Smorgon];
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            StringCollection updatedCodes = new StringCollection();
            updatedCodes.Add(textBoxMinskCode.Text);
            updatedCodes.Add(textBoxMarGorkaCode.Text);
            updatedCodes.Add(textBoxVolkoviskCode.Text);
            updatedCodes.Add(textBoxLuninecCode.Text);
            updatedCodes.Add(textBoxSmorgonCode.Text);
            Properties.Settings.Default.FilialsCodes = updatedCodes;
            Properties.Settings.Default.Save();
        }
    }
}