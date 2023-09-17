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
    public partial class FormPreferences : Form
    {        

        private BusinessProcess businessProcess;

        public FormPreferences()
        {
            InitializeComponent();
            businessProcess = BusinessProcess.Instance;
            List<Printer> printers = businessProcess.ListPrinter();

            UserPreferences userPreferences = businessProcess.RetrieveUserPreferences();

            foreach (Printer printer in printers)
            {
                comboBoxPrinters.Items.Add(printer);

                if (printer.ID == userPreferences?.PrinterID)
                {
                    comboBoxPrinters.SelectedItem = printer;
                }
            }
            
            comboBoxDPD.SelectedItem = userPreferences?.ServiceDPD ?? UserPreferences.SERVICE_DISABLE;
            comboBoxAPC.SelectedItem = userPreferences?.ServiceAPC ?? UserPreferences.SERVICE_DISABLE;
            comboBoxDL.SelectedItem = userPreferences?.ServiceDL ?? UserPreferences.SERVICE_DISABLE;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UserPreferences userPreferences = new UserPreferences();

            userPreferences.PrinterID = (int)((comboBoxPrinters.SelectedItem as Printer)?.ID);
            userPreferences.ServiceDPD = comboBoxDPD.SelectedItem as String;
            userPreferences.ServiceAPC = comboBoxAPC.SelectedItem as String;
            userPreferences.ServiceDL = comboBoxDL.SelectedItem as String;
            businessProcess.StoreUserPreferences(userPreferences);
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public UserPreferences GetUserPreferences()
        {
            UserPreferences userPreferences = new UserPreferences();

            return userPreferences;
        }
    }
}
