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
    public partial class FormAPCPreferences : Form
    {
        public FormAPCPreferences()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
            APCPrinterPreferences apcPrinterPreferences = new APCPrinterPreferences();
            apcPrinterPreferences.APIEndpoint = textBoxAPIEndpoint.Text;
            apcPrinterPreferences.Username = textBoxUsername.Text;
            apcPrinterPreferences.Password = textBoxPassword.Text;
            apcPrinterPreferences.LabelPrinterName = comboBoxLabelPrinterName.Text;
            apcPrinterPreferences.PrintingFormat = comboBoxLabelFormat.Text;
            BusinessProcess.Instance.StoreAPCPrinterPreferences(apcPrinterPreferences);

            this.DialogResult = DialogResult.OK;
        }

        private void FormAPCPreferences_Load(object sender, EventArgs e)
        {
            UIUpdate();
        }

        private void UIUpdate()
        {

            comboBoxLabelPrinterName.Items.Clear();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBoxLabelPrinterName.Items.Add(printer);
            }

            APCPrinterPreferences printerPreferences = BusinessProcess.Instance.RetrieveAPCPrinterPreferences();
            
            this.textBoxAPIEndpoint.Text = printerPreferences?.APIEndpoint;
            this.textBoxUsername.Text = printerPreferences?.Username;
            this.textBoxPassword.Text = printerPreferences?.Password;
            this.comboBoxLabelPrinterName.Text = printerPreferences?.LabelPrinterName;
            this.comboBoxLabelFormat.Text = printerPreferences?.PrintingFormat;
        }
    }
}
