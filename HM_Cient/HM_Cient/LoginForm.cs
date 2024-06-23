using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HM_Cient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.ForeColor = Color.White;
                panel5.Visible = false;
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox2.ForeColor = Color.White;
                textBox2.PasswordChar = '*';
                panel7.Visible = false;
            }
            catch { }
        }

        private void textbox2_click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textbox1_click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor= Color.Lime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "Admin")
            {
                if (textBox1.Text == "")
                {
                    textBox1.Text = "Enter UserName";
                    return;
                }
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            if(textBox2.Text != "Password@123")
            {
                if (textBox2.Text == "")
                {
                    textBox2.Text = "Password";
                    return;
                }
                    panel7.Visible = true;
                textBox2.Focus();
                return;
            }
            this.Hide();
            HomePage homePage = new HomePage();
            homePage.Show();
        }
    }
}
