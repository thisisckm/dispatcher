
namespace Dispatcher
{
    partial class FormAPCPreferences
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
            this.labelAPIEndpoint = new System.Windows.Forms.Label();
            this.textBoxAPIEndpoint = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelLabelPrinterName = new System.Windows.Forms.Label();
            this.comboBoxLabelPrinterName = new System.Windows.Forms.ComboBox();
            this.labelLabelFormat = new System.Windows.Forms.Label();
            this.comboBoxLabelFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelAPIEndpoint
            // 
            this.labelAPIEndpoint.AutoSize = true;
            this.labelAPIEndpoint.Location = new System.Drawing.Point(13, 13);
            this.labelAPIEndpoint.Name = "labelAPIEndpoint";
            this.labelAPIEndpoint.Size = new System.Drawing.Size(69, 13);
            this.labelAPIEndpoint.TabIndex = 0;
            this.labelAPIEndpoint.Text = "API Endpoint";
            // 
            // textBoxAPIEndpoint
            // 
            this.textBoxAPIEndpoint.Location = new System.Drawing.Point(16, 30);
            this.textBoxAPIEndpoint.Name = "textBoxAPIEndpoint";
            this.textBoxAPIEndpoint.Size = new System.Drawing.Size(361, 20);
            this.textBoxAPIEndpoint.TabIndex = 1;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(16, 81);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(361, 20);
            this.textBoxUsername.TabIndex = 3;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(13, 64);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 2;
            this.labelUsername.Text = "Username";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(16, 129);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(361, 20);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(13, 112);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(293, 268);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(212, 268);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelLabelPrinterName
            // 
            this.labelLabelPrinterName.AutoSize = true;
            this.labelLabelPrinterName.Location = new System.Drawing.Point(13, 162);
            this.labelLabelPrinterName.Name = "labelLabelPrinterName";
            this.labelLabelPrinterName.Size = new System.Drawing.Size(97, 13);
            this.labelLabelPrinterName.TabIndex = 8;
            this.labelLabelPrinterName.Text = "Label Printer Name";
            // 
            // comboBoxLabelPrinterName
            // 
            this.comboBoxLabelPrinterName.FormattingEnabled = true;
            this.comboBoxLabelPrinterName.Location = new System.Drawing.Point(16, 179);
            this.comboBoxLabelPrinterName.Name = "comboBoxLabelPrinterName";
            this.comboBoxLabelPrinterName.Size = new System.Drawing.Size(205, 21);
            this.comboBoxLabelPrinterName.TabIndex = 9;
            // 
            // labelLabelFormat
            // 
            this.labelLabelFormat.AutoSize = true;
            this.labelLabelFormat.Location = new System.Drawing.Point(13, 215);
            this.labelLabelFormat.Name = "labelLabelFormat";
            this.labelLabelFormat.Size = new System.Drawing.Size(68, 13);
            this.labelLabelFormat.TabIndex = 10;
            this.labelLabelFormat.Text = "Label Format";
            // 
            // comboBoxLabelFormat
            // 
            this.comboBoxLabelFormat.FormattingEnabled = true;
            this.comboBoxLabelFormat.Items.AddRange(new object[] {
            "ZPL",
            "PDF"});
            this.comboBoxLabelFormat.Location = new System.Drawing.Point(16, 232);
            this.comboBoxLabelFormat.Name = "comboBoxLabelFormat";
            this.comboBoxLabelFormat.Size = new System.Drawing.Size(205, 21);
            this.comboBoxLabelFormat.TabIndex = 11;
            // 
            // FormAPCPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 314);
            this.Controls.Add(this.comboBoxLabelFormat);
            this.Controls.Add(this.labelLabelFormat);
            this.Controls.Add(this.comboBoxLabelPrinterName);
            this.Controls.Add(this.labelLabelPrinterName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxAPIEndpoint);
            this.Controls.Add(this.labelAPIEndpoint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAPCPreferences";
            this.Text = "APC Preferences";
            this.Load += new System.EventHandler(this.FormAPCPreferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAPIEndpoint;
        private System.Windows.Forms.TextBox textBoxAPIEndpoint;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelLabelPrinterName;
        private System.Windows.Forms.ComboBox comboBoxLabelPrinterName;
        private System.Windows.Forms.Label labelLabelFormat;
        private System.Windows.Forms.ComboBox comboBoxLabelFormat;
    }
}