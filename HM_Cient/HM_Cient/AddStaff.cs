using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HM_Cient
{
    public partial class AddStaff : Form
    {
        public AddStaff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HM_Cient.StaffServiceReference.StaffServiceClient proxy = new HM_Cient.StaffServiceReference.StaffServiceClient("NetTcpBinding_IStaffService");
            HM_Cient.StaffServiceReference.Staff staff = new HM_Cient.StaffServiceReference.Staff();

            staff.FirstName = textBox1.Text;
            staff.MiddleName = textBox2.Text;
            staff.LastName = textBox3.Text;
            staff.Email = textBox6.Text;
            staff.PhoneNo = textBox7.Text;
            staff.DateOfBirth = dateTimePicker3.Value.Date;
            staff.DateOfJoining = dateTimePicker1.Value.Date;
            staff.WorkType = textBox11.Text;
            staff.Address = textBox9.Text;
            staff.Salary = int.Parse(textBox10.Text);

            int n = proxy.AddStaff(staff);

            if (n == 1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;

                panel1.Visible = true;
            }

            proxy.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
