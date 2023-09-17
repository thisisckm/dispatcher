namespace Dispatcher
{
    public partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonProcessPrinting = new System.Windows.Forms.Button();
            this.groupBoxPrintingProcess = new System.Windows.Forms.GroupBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonPreferences = new System.Windows.Forms.ToolStripSplitButton();
            this.perferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dpdPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPCPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAboutUs = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelLogin = new System.Windows.Forms.ToolStripLabel();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBoxDPDTrackerRefUpdate = new System.Windows.Forms.GroupBox();
            this.textBoxDPDTrackerRefFile = new System.Windows.Forms.TextBox();
            this.buttonUpdateDPDTrackingRef = new System.Windows.Forms.Button();
            this.buttonDPDSelectFile = new System.Windows.Forms.Button();
            this.groupBoxPrintingProcess.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.groupBoxDPDTrackerRefUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonProcessPrinting
            // 
            this.buttonProcessPrinting.Location = new System.Drawing.Point(12, 23);
            this.buttonProcessPrinting.Name = "buttonProcessPrinting";
            this.buttonProcessPrinting.Size = new System.Drawing.Size(75, 23);
            this.buttonProcessPrinting.TabIndex = 0;
            this.buttonProcessPrinting.Text = "Start";
            this.buttonProcessPrinting.UseVisualStyleBackColor = true;
            this.buttonProcessPrinting.Click += new System.EventHandler(this.ButtonProcessPrinting_Click);
            // 
            // groupBoxPrintingProcess
            // 
            this.groupBoxPrintingProcess.AutoSize = true;
            this.groupBoxPrintingProcess.Controls.Add(this.buttonProcessPrinting);
            this.groupBoxPrintingProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxPrintingProcess.Location = new System.Drawing.Point(0, 385);
            this.groupBoxPrintingProcess.Name = "groupBoxPrintingProcess";
            this.groupBoxPrintingProcess.Size = new System.Drawing.Size(800, 65);
            this.groupBoxPrintingProcess.TabIndex = 1;
            this.groupBoxPrintingProcess.TabStop = false;
            this.groupBoxPrintingProcess.Text = "Printing Process";
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonPreferences,
            this.toolStripSeparator1,
            this.toolStripButtonAboutUs,
            this.toolStripLabelLogin});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(800, 25);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripSplitButtonPreferences
            // 
            this.toolStripSplitButtonPreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonPreferences.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.perferencesToolStripMenuItem,
            this.toolStripSeparator2,
            this.dpdPreferencesToolStripMenuItem,
            this.aPCPreferencesToolStripMenuItem,
            this.dLPreferencesToolStripMenuItem});
            this.toolStripSplitButtonPreferences.Image = global::Dispatcher.Properties.Resources.perferences;
            this.toolStripSplitButtonPreferences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonPreferences.Name = "toolStripSplitButtonPreferences";
            this.toolStripSplitButtonPreferences.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButtonPreferences.Text = "toolStripSplitButton1";
            // 
            // perferencesToolStripMenuItem
            // 
            this.perferencesToolStripMenuItem.Name = "perferencesToolStripMenuItem";
            this.perferencesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.perferencesToolStripMenuItem.Text = "Preferences";
            this.perferencesToolStripMenuItem.Click += new System.EventHandler(this.perferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // dpdPreferencesToolStripMenuItem
            // 
            this.dpdPreferencesToolStripMenuItem.Name = "dpdPreferencesToolStripMenuItem";
            this.dpdPreferencesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.dpdPreferencesToolStripMenuItem.Text = "DPD Preferences";
            this.dpdPreferencesToolStripMenuItem.Click += new System.EventHandler(this.dpdPreferencesToolStripMenuItem_Click);
            // 
            // aPCPreferencesToolStripMenuItem
            // 
            this.aPCPreferencesToolStripMenuItem.Name = "aPCPreferencesToolStripMenuItem";
            this.aPCPreferencesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.aPCPreferencesToolStripMenuItem.Text = "APC Preferences";
            this.aPCPreferencesToolStripMenuItem.Click += new System.EventHandler(this.aPCPreferencesToolStripMenuItem_Click);
            // 
            // dLPreferencesToolStripMenuItem
            // 
            this.dLPreferencesToolStripMenuItem.Name = "dLPreferencesToolStripMenuItem";
            this.dLPreferencesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.dLPreferencesToolStripMenuItem.Text = "DL Preferences";
            this.dLPreferencesToolStripMenuItem.Click += new System.EventHandler(this.dLPreferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAboutUs
            // 
            this.toolStripButtonAboutUs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAboutUs.Image = global::Dispatcher.Properties.Resources.about_us_icon;
            this.toolStripButtonAboutUs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAboutUs.Name = "toolStripButtonAboutUs";
            this.toolStripButtonAboutUs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAboutUs.Text = "toolStripButtonAboutUs";
            this.toolStripButtonAboutUs.Click += new System.EventHandler(this.ToolStripButtonAboutUs_Click);
            // 
            // toolStripLabelLogin
            // 
            this.toolStripLabelLogin.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelLogin.IsLink = true;
            this.toolStripLabelLogin.Name = "toolStripLabelLogin";
            this.toolStripLabelLogin.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabelLogin.Text = "Sign In";
            this.toolStripLabelLogin.Click += new System.EventHandler(this.toolStripLabelLogin_Click);
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconMain.BalloonTipText = "No Worries, I am still running";
            this.notifyIconMain.BalloonTipTitle = "Dispatcher";
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "notifyIconMain";
            this.notifyIconMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMain_MouseDoubleClick);
            // 
            // groupBoxDPDTrackerRefUpdate
            // 
            this.groupBoxDPDTrackerRefUpdate.AutoSize = true;
            this.groupBoxDPDTrackerRefUpdate.Controls.Add(this.textBoxDPDTrackerRefFile);
            this.groupBoxDPDTrackerRefUpdate.Controls.Add(this.buttonUpdateDPDTrackingRef);
            this.groupBoxDPDTrackerRefUpdate.Controls.Add(this.buttonDPDSelectFile);
            this.groupBoxDPDTrackerRefUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxDPDTrackerRefUpdate.Location = new System.Drawing.Point(0, 324);
            this.groupBoxDPDTrackerRefUpdate.Name = "groupBoxDPDTrackerRefUpdate";
            this.groupBoxDPDTrackerRefUpdate.Size = new System.Drawing.Size(800, 61);
            this.groupBoxDPDTrackerRefUpdate.TabIndex = 3;
            this.groupBoxDPDTrackerRefUpdate.TabStop = false;
            this.groupBoxDPDTrackerRefUpdate.Text = "DPD Tracker Ref Update";
            this.groupBoxDPDTrackerRefUpdate.Visible = false;
            // 
            // textBoxDPDTrackerRefFile
            // 
            this.textBoxDPDTrackerRefFile.Enabled = false;
            this.textBoxDPDTrackerRefFile.Location = new System.Drawing.Point(13, 20);
            this.textBoxDPDTrackerRefFile.Name = "textBoxDPDTrackerRefFile";
            this.textBoxDPDTrackerRefFile.ReadOnly = true;
            this.textBoxDPDTrackerRefFile.Size = new System.Drawing.Size(363, 20);
            this.textBoxDPDTrackerRefFile.TabIndex = 2;
            // 
            // buttonUpdateDPDTrackingRef
            // 
            this.buttonUpdateDPDTrackingRef.Enabled = false;
            this.buttonUpdateDPDTrackingRef.Location = new System.Drawing.Point(462, 19);
            this.buttonUpdateDPDTrackingRef.Name = "buttonUpdateDPDTrackingRef";
            this.buttonUpdateDPDTrackingRef.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateDPDTrackingRef.TabIndex = 1;
            this.buttonUpdateDPDTrackingRef.Text = "Update";
            this.buttonUpdateDPDTrackingRef.UseVisualStyleBackColor = true;
            this.buttonUpdateDPDTrackingRef.Click += new System.EventHandler(this.buttonUpdateDPDTrackingRef_Click);
            // 
            // buttonDPDSelectFile
            // 
            this.buttonDPDSelectFile.Enabled = false;
            this.buttonDPDSelectFile.Location = new System.Drawing.Point(382, 19);
            this.buttonDPDSelectFile.Name = "buttonDPDSelectFile";
            this.buttonDPDSelectFile.Size = new System.Drawing.Size(74, 23);
            this.buttonDPDSelectFile.TabIndex = 0;
            this.buttonDPDSelectFile.Text = "Select File";
            this.buttonDPDSelectFile.UseVisualStyleBackColor = true;
            this.buttonDPDSelectFile.Click += new System.EventHandler(this.buttonDPDSelectFile_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxDPDTrackerRefUpdate);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.groupBoxPrintingProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Dispatcher v1.3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_Closing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.groupBoxPrintingProcess.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.groupBoxDPDTrackerRefUpdate.ResumeLayout(false);
            this.groupBoxDPDTrackerRefUpdate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProcessPrinting;
        private System.Windows.Forms.GroupBox groupBoxPrintingProcess;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonAboutUs;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelLogin;
        private System.Windows.Forms.GroupBox groupBoxDPDTrackerRefUpdate;
        private System.Windows.Forms.TextBox textBoxDPDTrackerRefFile;
        private System.Windows.Forms.Button buttonUpdateDPDTrackingRef;
        private System.Windows.Forms.Button buttonDPDSelectFile;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonPreferences;
        private System.Windows.Forms.ToolStripMenuItem dpdPreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPCPreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem dLPreferencesToolStripMenuItem;
    }
}

