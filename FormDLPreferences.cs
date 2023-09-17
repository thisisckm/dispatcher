using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dispatcher
{
    public partial class FormDLPreferences : Form
    {
        public FormDLPreferences()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DLPreferences dlPreferences = new DLPreferences();
            dlPreferences.APIEndpoint = textBoxURL.Text;
            dlPreferences.Username = textBoxUsername.Text;
            dlPreferences.Password = textBoxPassword.Text;
            dlPreferences.LabelPrinterName = comboBoxLabelPrinter.Text;
            BusinessProcess.Instance.StoreDLPreferences(dlPreferences);

            this.DialogResult = DialogResult.OK;
        }

        private void FormDLPreferences_Load(object sender, EventArgs e)
        {
            UIUpdate();
        }

        private void UIUpdate()
        {
            comboBoxLabelPrinter.Items.Clear();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBoxLabelPrinter.Items.Add(printer);
            }

            DLPreferences dlPreferences = BusinessProcess.Instance.RetrieveDLPreferences();

            this.textBoxURL.Text = dlPreferences?.APIEndpoint;
            this.textBoxUsername.Text = dlPreferences?.Username;
            this.textBoxPassword.Text = dlPreferences?.Password;
            this.comboBoxLabelPrinter.Text = dlPreferences?.LabelPrinterName;
            
        }
    }
}
