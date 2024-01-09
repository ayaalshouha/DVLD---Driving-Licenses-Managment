using System;
using System.Drawing;
using DVLD_Buissness;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace DVLD___Driving_Licenses_Managment
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblNotes.Visible = true;
                lblNotes.Text = "* Enter Username/password please..";
                return; 
            }

            lblNotes.Visible = false;

            //check if exist 
            if (clsUser.isExist(txtUsername.Text, txtPassword.Text))
            {
                //check if active 
                if(clsUser.Authintication(txtUsername.Text, txtPassword.Text))
                {
                    clsGlobal.CurrentUser = clsUser.Find(txtUsername.Text);

                    if(clsGlobal.CurrentUser != null)
                    {
                        if (checkBox1.Checked)
                        {
                            clsGlobal.RememberUsernameAndPassword(txtUsername.Text, txtPassword.Text);
                        }
                        else
                        {
                            txtUsername.Text = "";
                            txtPassword.Text = "";
                            clsGlobal.RememberUsernameAndPassword("", "");
                        }

                        Logger DatabaseLogger = new Logger(clsUser.SaveLogin);

                        if (DatabaseLogger.Log(clsGlobal.CurrentUser.ID))
                        {
                            this.Hide();
                            FrmMainScreen Form = new FrmMainScreen(this);
                            Form.ShowDialog();
                        }
                    }
                }
                else
                {
                    lblNotes.Visible = true;
                    lblNotes.Text = "* User account is NOT active!";
                }
            }
            else
            {
                lblNotes.Visible = true;
                lblNotes.Text = "* Invalid username/password!";
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            string username ="", password = ""; 

            if(clsGlobal.getStoredUsernameAndPassword(ref username, ref password))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                checkBox1.Checked = true; 
            }
            else
                checkBox1.Checked = false;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
            {
            pnlUsername.BackColor = Color.DodgerBlue;
            pnlPassword.BackColor = Color.DimGray;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
              txtUsername.ForeColor = Color.Black;
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.ForeColor = Color.Black;
            txtPassword.UseSystemPasswordChar = false;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            pnlPassword.BackColor = Color.DodgerBlue;
            pnlUsername.BackColor = Color.DimGray;
        }
    }
}
