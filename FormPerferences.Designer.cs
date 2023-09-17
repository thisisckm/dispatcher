
namespace Dispatcher
{
    partial class FormPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.groupBoxSelectPrinter = new System.Windows.Forms.GroupBox();
            this.comboBoxPrinters = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxServices = new System.Windows.Forms.GroupBox();
            this.comboBoxAPC = new System.Windows.Forms.ComboBox();
            this.labelAPC = new System.Windows.Forms.Label();
            this.comboBoxDPD = new System.Windows.Forms.ComboBox();
            this.labelDPD = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxDL = new System.Windows.Forms.ComboBox();
            this.labelDL = new System.Windows.Forms.Label();
            this.groupBoxSelectPrinter.SuspendLayout();
            this.groupBoxServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSelectPrinter
            // 
            this.groupBoxSelectPrinter.Controls.Add(this.comboBoxPrinters);
            this.groupBoxSelectPrinter.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSelectPrinter.Name = "groupBoxSelectPrinter";
            this.groupBoxSelectPrinter.Size = new System.Drawing.Size(362, 52);
            this.groupBoxSelectPrinter.TabIndex = 1;
            this.groupBoxSelectPrinter.TabStop = false;
            this.groupBoxSelectPrinter.Text = "Select Printer";
            // 
            // comboBoxPrinters
            // 
            this.comboBoxPrinters.FormattingEnabled = true;
            this.comboBoxPrinters.Location = new System.Drawing.Point(7, 20);
            this.comboBoxPrinters.Name = "comboBoxPrinters";
            this.comboBoxPrinters.Size = new System.Drawing.Size(343, 21);
            this.comboBoxPrinters.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(299, 193);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxServices
            // 
            this.groupBoxServices.Controls.Add(this.comboBoxDL);
            this.groupBoxServices.Controls.Add(this.labelDL);
            this.groupBoxServices.Controls.Add(this.comboBoxAPC);
            this.groupBoxServices.Controls.Add(this.labelAPC);
            this.groupBoxServices.Controls.Add(this.comboBoxDPD);
            this.groupBoxServices.Controls.Add(this.labelDPD);
            this.groupBoxServices.Location = new System.Drawing.Point(12, 71);
            this.groupBoxServices.Name = "groupBoxServices";
            this.groupBoxServices.Size = new System.Drawing.Size(362, 113);
            this.groupBoxServices.TabIndex = 3;
            this.groupBoxServices.TabStop = false;
            this.groupBoxServices.Text = "Services";
            // 
            // comboBoxAPC
            // 
            this.comboBoxAPC.FormattingEnabled = true;
            this.comboBoxAPC.Items.AddRange(new object[] {
            "Disable",
            "Legacy",
            "API with Write Back"});
            this.comboBoxAPC.Location = new System.Drawing.Point(43, 49);
            this.comboBoxAPC.Name = "comboBoxAPC";
            this.comboBoxAPC.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAPC.TabIndex = 3;
            // 
            // labelAPC
            // 
            this.labelAPC.AutoSize = true;
            this.labelAPC.Location = new System.Drawing.Point(7, 52);
            this.labelAPC.Name = "labelAPC";
            this.labelAPC.Size = new System.Drawing.Size(28, 13);
            this.labelAPC.TabIndex = 2;
            this.labelAPC.Text = "APC";
            // 
            // comboBoxDPD
            // 
            this.comboBoxDPD.FormattingEnabled = true;
            this.comboBoxDPD.Items.AddRange(new object[] {
            "Disable",
            "Legacy",
            "API with Write Back"});
            this.comboBoxDPD.Location = new System.Drawing.Point(43, 17);
            this.comboBoxDPD.Name = "comboBoxDPD";
            this.comboBoxDPD.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDPD.TabIndex = 1;
            // 
            // labelDPD
            // 
            this.labelDPD.AutoSize = true;
            this.labelDPD.Location = new System.Drawing.Point(7, 20);
            this.labelDPD.Name = "labelDPD";
            this.labelDPD.Size = new System.Drawing.Size(30, 13);
            this.labelDPD.TabIndex = 0;
            this.labelDPD.Text = "DPD";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(218, 193);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxDL
            // 
            this.comboBoxDL.FormattingEnabled = true;
            this.comboBoxDL.Items.AddRange(new object[] {
            "Disable",
            "Legacy",
            "API with Write Back"});
            this.comboBoxDL.Location = new System.Drawing.Point(43, 79);
            this.comboBoxDL.Name = "comboBoxDL";
            this.comboBoxDL.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDL.TabIndex = 5;
            // 
            // labelDL
            // 
            this.labelDL.AutoSize = true;
            this.labelDL.Location = new System.Drawing.Point(7, 83);
            this.labelDL.Name = "labelDL";
            this.labelDL.Size = new System.Drawing.Size(21, 13);
            this.labelDL.TabIndex = 4;
            this.labelDL.Text = "DL";
            // 
            // FormPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 230);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxServices);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxSelectPrinter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreferences";
            this.Text = "Preferences";
            this.groupBoxSelectPrinter.ResumeLayout(false);
            this.groupBoxServices.ResumeLayout(false);
            this.groupBoxServices.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSelectPrinter;
        private System.Windows.Forms.ComboBox comboBoxPrinters;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxServices;
        private System.Windows.Forms.Label labelDPD;
        private System.Windows.Forms.ComboBox comboBoxDPD;
        private System.Windows.Forms.Label labelAPC;
        private System.Windows.Forms.ComboBox comboBoxAPC;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxDL;
        private System.Windows.Forms.Label labelDL;
    }
}