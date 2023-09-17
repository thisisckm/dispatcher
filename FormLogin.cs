using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dispatcher
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.errorProvider1.GetError(this) == "closing")
            {
                Application.Exit();
            }
            
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.errorProvider1.SetError(this, "closing");
        }        

        private void FormLogin_Shown(object sender, EventArgs e)
        {
            
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            var environment = comboBoxEnvironment.SelectedIndex == 0 ? Environment.Production : Environment.Test;
            try
            {
                BusinessProcess.Instance.DoLogin(environment, textBoxUsername.Text, textBoxPassword.Text);
                this.errorProvider1.SetError(buttonSignIn, "");
                this.errorProvider1.SetError(this, "");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.errorProvider1.SetError(buttonSignIn, ex.Message);
            }
        }

        private void comboBoxEnvironment_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider1.SetError(comboBoxEnvironment,
                comboBoxEnvironment.SelectedIndex == -1 ? "Test or Production Environment" : "");
        }


        private void textBoxUsername_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider1.SetError(textBoxUsername,
                textBoxUsername.Text == String.Empty ? "Username is required" : "");
        }
    

        private void textBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider1.SetError(textBoxPassword,
                textBoxPassword.Text == String.Empty ? "Password is required" : "");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBoxEnvironment.SelectedIndex == -1 || textBoxUsername.Text == string.Empty || textBoxPassword.Text == string.Empty)
            {
                buttonSignIn.Enabled = false;
            }
            else
            {
                buttonSignIn.Enabled = true;
                this.errorProvider1.SetError(comboBoxEnvironment, "");
                this.errorProvider1.SetError(textBoxPassword, "");
                this.errorProvider1.SetError(textBoxUsername, "");
            }
        }
    }
}
