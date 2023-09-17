using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Dispatcher
{
    public partial class FormMain : Form
    {

        private StatusBarPanel statusBarPanelMessage;        
        private CancellationTokenSource tokenSourcePrintingProcess;
        private CancellationToken tokenPrintingProcess;

        private CancellationTokenSource tokenSourceDPDTrackingRefUpdate;
        private CancellationToken tokenDPDTrackingRefUpdate;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CreateMyStatusBar();
            UIUpdate();
        }

        private void FormMain_Closing(object sender, FormClosingEventArgs e)
        {
            if (buttonProcessPrinting.Text == "Stop")
            {
                string message = "Please stop the process before closing the Dispatcher";
                MessageBox.Show(message);
                e.Cancel = true;
            }            
        }

        private void CreateMyStatusBar()
        {
            // Create a StatusBar control.
            StatusBar statusBarMain = new StatusBar();
            // Create two StatusBarPanel objects to display in the StatusBar.
            statusBarPanelMessage = new StatusBarPanel();
            StatusBarPanel statusBarPanelTime = new StatusBarPanel();
            
            // Display the first panel with a sunken border style.
            statusBarPanelMessage.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            // Initialize the text of the panel.
            statusBarPanelMessage.Text = "Ready...";
            // Set the AutoSize property to use all remaining space on the StatusBar.
            statusBarPanelMessage.AutoSize = StatusBarPanelAutoSize.Spring;

            // Display the second panel with a raised border style.
            statusBarPanelTime.BorderStyle = StatusBarPanelBorderStyle.Raised;

            // Create ToolTip text that displays time the application was started.
            statusBarPanelTime.ToolTipText = "Started: " + System.DateTime.Now.ToShortTimeString();
            // Set the text of the panel to the current date.
            statusBarPanelTime.Text = System.DateTime.Today.ToLongDateString();
            // Set the AutoSize property to size the panel to the size of the contents.
            statusBarPanelTime.AutoSize = StatusBarPanelAutoSize.Contents;

            // Display panels in the StatusBar control.
            statusBarMain.ShowPanels = true;

            // Add both panels to the StatusBarPanelCollection of the StatusBar.			
            statusBarMain.Panels.Add(statusBarPanelMessage);
            statusBarMain.Panels.Add(statusBarPanelTime);

            // Add the StatusBar to the form.
            this.Controls.Add(statusBarMain);
        }

        private async void ButtonProcessPrinting_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Text == "Start")
            {
                tokenSourcePrintingProcess = new CancellationTokenSource();
                tokenPrintingProcess = tokenSourcePrintingProcess.Token;                
                buttonProcessPrinting.Text = "Stop";
                await Task.Factory.StartNew(() => PrintingWork(tokenPrintingProcess), TaskCreationOptions.LongRunning);
                
            }
            else
            {
                tokenSourcePrintingProcess.Cancel();                
                tokenSourcePrintingProcess.Dispose();
                buttonProcessPrinting.Text = "Start";
            }
            
        }

        public void PrintingWork(CancellationToken token)
        {
            // Perform a long running work...
            string cancelledWithException = null;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    this.Invoke(new MethodInvoker(
                        delegate {
                            statusBarPanelMessage.Text = "Reading from ERP";
                        }
                    ));

                    var listJob = BusinessProcess.Instance.FetchUnPrintJobs();
                    this.Invoke(new MethodInvoker(delegate { statusBarPanelMessage.Text = "Printing..."; }));
                    var result = BusinessProcess.Instance.DoPrinting(listJob);
                    this.Invoke(
                        new MethodInvoker(delegate { statusBarPanelMessage.Text = "Closing Printing Jobs..."; }));
                    BusinessProcess.Instance.CloseJobs(result);
                    this.Invoke(new MethodInvoker(delegate { statusBarPanelMessage.Text = "Waiting..."; }));
                    Task.Delay(1000 * 30, token).Wait(token);
                }
                catch(OperationCanceledException)
                {
                    break;
                }
                catch(Exception ex)
                {                    
                    cancelledWithException = ex.Message;
                    break;
                }
                

            }
            this.Invoke(new MethodInvoker(delegate {
                if (cancelledWithException != null)
                {
                    statusBarPanelMessage.Text = $"Exception::{cancelledWithException}";
                    buttonProcessPrinting.Text = "Start";
                }
                else
                {
                    statusBarPanelMessage.Text = "Ready";
                }
            }));
        }

        private void ToolStripButtonAboutUs_Click(object sender, EventArgs e)
        {
            var aboutUsDialog = new AboutBox();
            aboutUsDialog.ShowDialog(this);
        }

        private void notifyIconMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIconMain.Visible = false;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIconMain.Visible = true;
                notifyIconMain.ShowBalloonTip(3000);
                this.ShowInTaskbar = false;
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            var businessProcess = BusinessProcess.Instance;
            if (businessProcess.IsLoggedIn() == false)
            {
                var loginDialog = new FormLogin();
                loginDialog.ShowDialog(this);
            }
            UpdateLoginView();
        }

        private void UpdateLoginView()
        {
            var businessProcess = BusinessProcess.Instance;
            if (businessProcess.IsLoggedIn())
            {
                string _environment = businessProcess.Environment == Environment.Test ? "Test" : "Live";
                toolStripLabelLogin.Text = $"{_environment} | {businessProcess.UserProfile.Name} (Sign Out)";

            }
            else
            {
                toolStripLabelLogin.Text = "Sign In";
            }
        }

        private void toolStripLabelLogin_Click(object sender, EventArgs e)
        {
            if (buttonProcessPrinting.Text == "Start")
            {
                var businessProcess = BusinessProcess.Instance;
                if (businessProcess.IsLoggedIn())
                {
                    businessProcess.DoLogout();
                    UpdateLoginView();
                }
                var loginDialog = new FormLogin();
                loginDialog.ShowDialog(this);
                UpdateLoginView();
            }
            else
            {
                string message = "Please stop the process before logout";                
                MessageBox.Show(message);
            }
        }

        private void buttonDPDSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            var businessProcess = BusinessProcess.Instance;
            fdlg.Title = "Select DPD Tracker Ref file";
            fdlg.InitialDirectory = businessProcess.RetrieveLastDPDLocation();
            fdlg.Filter = "DPD Tracker Ref File (*.csv)|*.csv";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBoxDPDTrackerRefFile.Text = fdlg.FileName;
                businessProcess.StoreLastDPDLocation(fdlg.FileName);
            }
        }

        private async void buttonUpdateDPDTrackingRef_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Text == "Update")
            {

                tokenSourceDPDTrackingRefUpdate = new CancellationTokenSource();
                tokenDPDTrackingRefUpdate = tokenSourceDPDTrackingRefUpdate.Token;                
                await Task.Factory.StartNew(() => DPDTrackingRefUpdate(tokenDPDTrackingRefUpdate), TaskCreationOptions.LongRunning);                
            }
            else
            {
                tokenSourceDPDTrackingRefUpdate.Cancel();
                tokenSourceDPDTrackingRefUpdate.Dispose();                
            }
            
        }

        private void DPDTrackingRefUpdate(CancellationToken token)
        {
            // Perform a long running work...
            string cancelledWithException = null;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    this.Invoke(new MethodInvoker(
                        delegate {
                            buttonDPDSelectFile.Enabled = false;
                            buttonUpdateDPDTrackingRef.Enabled = false;
                        }
                    ));

                    var businessProcess = BusinessProcess.Instance;
                    DPDUpdateTrackingRefReult result = businessProcess.DPDUpdateTrackingRef(textBoxDPDTrackerRefFile.Text);                    

                    this.Invoke(new MethodInvoker(
                       delegate {
                           buttonDPDSelectFile.Enabled = true;
                           buttonUpdateDPDTrackingRef.Enabled = true;
                           MessageBox.Show(this, $"Updating is end with {result.Success} successful update", "Updating completed");
                       }
                    ));
                    Task.Delay(1000 * 30, token).Wait(token);
                }
                catch (OperationCanceledException)
                {
                    this.Invoke(new MethodInvoker(
                        delegate {
                            buttonDPDSelectFile.Enabled = true;
                            buttonUpdateDPDTrackingRef.Enabled = true;
                        }
                    ));
                }
                catch (Exception ex)
                {
                    this.Invoke(new MethodInvoker(
                        delegate {
                            buttonDPDSelectFile.Enabled = true;
                            buttonUpdateDPDTrackingRef.Enabled = true;
                        }
                    ));                    
                    cancelledWithException = ex.Message;
                    break;
                }

            }            
        }

        private void dpdPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formDPDPreferences = new FormDPDPreferences();
            formDPDPreferences.ShowDialog(this);
        }

        private void perferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formPreferences = new FormPreferences();
            if (formPreferences.ShowDialog(this) == DialogResult.OK)
            {
                UIUpdate();
                BusinessProcess.Instance.ApplyChanges();
            }
        }

        private void UIUpdate()
        {
            UserPreferences userPreferences = BusinessProcess.Instance.RetrieveUserPreferences();

            if (userPreferences != null)
            {
                buttonProcessPrinting.Enabled = true;

                dpdPreferencesToolStripMenuItem.Enabled = userPreferences.ServiceDPD == UserPreferences.SERVICE_API_WB;
                aPCPreferencesToolStripMenuItem.Enabled = userPreferences.ServiceAPC == UserPreferences.SERVICE_API_WB;
                dLPreferencesToolStripMenuItem.Enabled = userPreferences.ServiceDL == UserPreferences.SERVICE_API_WB;
            }
            else
            {
                buttonProcessPrinting.Enabled = false;
                dpdPreferencesToolStripMenuItem.Enabled = false;
                aPCPreferencesToolStripMenuItem.Enabled = false;
                dLPreferencesToolStripMenuItem.Enabled = false;
            }
        }

        private void aPCPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formAPCPreferences = new FormAPCPreferences();
            formAPCPreferences.ShowDialog(this);
        }

        private void dLPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formDLPreferences = new FormDLPreferences();
            formDLPreferences.ShowDialog(this);
        }
    }
}
