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
    public partial class FormDPDPreferences : Form
    {
        public FormDPDPreferences()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDPDPreferences_Load(object sender, EventArgs e)
        {
            comboBoxPrinter.Items.Clear();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBoxPrinter.Items.Add(printer);
            }
            
            var _bussinessProcess = BusinessProcess.Instance;
            DPDPrinterPreferences dpdPrinterPreferences = _bussinessProcess.RetrieveDPDPrinterPreferences();            
            comboBoxPrinter.SelectedItem = dpdPrinterPreferences?.PrinterName;
            comboBoxFormat.SelectedItem = dpdPrinterPreferences?.PrintFormat;
            comboBoxTemplate.SelectedItem = dpdPrinterPreferences?.Template;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var _bussinessProcess = BusinessProcess.Instance;
            var dpdPreferences = new DPDPrinterPreferences();

            var printerName = comboBoxPrinter.SelectedItem?.ToString();
            var format = comboBoxFormat.SelectedItem?.ToString();
            var template = comboBoxTemplate.SelectedItem?.ToString();

            dpdPreferences.PrinterName = printerName;
            dpdPreferences.PrintFormat = format;
            dpdPreferences.Template = template;

            _bussinessProcess.StoreDPDPrinterPreferences(dpdPreferences);

            this.Close();
        }
    }
}
