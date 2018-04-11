using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CS129_ProjVer2._0
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "admin" && txtPass.Text == "admin")
            {
                MainForm mForm = new MainForm();
                MessageBox.Show("Log in successful!");
                mForm.Show();
                
                this.Hide();
                
            }
            else 
            {
                MessageBox.Show("Incorrect username or password. Please try again.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '*';
        }
    }
}
