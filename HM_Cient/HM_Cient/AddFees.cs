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
    public partial class AddFees : Form
    {
        public int StudentId { get; set; }
        public AddFees()
        {
            InitializeComponent();
        }

        private void AddFees_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            textBox1.Text = StudentId.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HM_Cient.StudentServiceReference.StudentServiceClient proxy = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
            HM_Cient.FeesServiceReference.FeesServiceClient proxy1 = new HM_Cient.FeesServiceReference.FeesServiceClient("NetTcpBinding_IFeesService");
            if (textBox1.Text != "")
            {
                dataGridView1.Visible = true;
                textBox2.Text = proxy.PendingFees(int.Parse(textBox1.Text)).ToString();
                DataSet ds = proxy1.StudentAllFees(int.Parse(textBox1.Text));
                dataGridView1.DataSource = ds.Tables["StudentFees"];
            }
            else
            {
                textBox2.Text = "";
                dataGridView1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HM_Cient.FeesServiceReference.FeesServiceClient proxy = new HM_Cient.FeesServiceReference.FeesServiceClient("NetTcpBinding_IFeesService");
            HM_Cient.FeesServiceReference.Fees fee = new HM_Cient.FeesServiceReference.Fees();
            fee.StudentId = int.Parse(textBox1.Text);
            fee.PaidAmount = int.Parse(textBox3.Text);
            fee.PaymentDate= DateTime.Now;
            int n = proxy.AddFees(fee);
            if (n == 1)
            {
                textBox1.Text = "";
                textBox3.Text = "";

                MessageBox.Show("Fee Added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add Fee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
