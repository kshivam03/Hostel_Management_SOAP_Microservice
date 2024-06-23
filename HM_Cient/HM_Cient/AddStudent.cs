using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HM_Cient
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HM_Cient.StudentServiceReference.StudentServiceClient proxy = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
            HM_Cient.StudentServiceReference.Student stu = new HM_Cient.StudentServiceReference.Student();
            stu.FirstName = textBox1.Text;
            stu.MiddleName = textBox2.Text;
            stu.LastName = textBox3.Text;
            stu.Email = textBox4.Text;
            stu.PhoneNo = textBox5.Text;
            stu.DateOfBirth = dateTimePicker1.Value.Date;
            stu.DateOfJoining = dateTimePicker2.Value.Date;
            stu.RoomID = (int)comboBox1.SelectedItem; ;
            stu.Address = textBox6.Text;
            int n = proxy.AddStudent(stu);
            if (n == 1)
            {
                panel1.Visible = true;
            }
            proxy.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;

            HM_Cient.RoomServiceReference.RoomServiceClient proxy = new HM_Cient.RoomServiceReference.RoomServiceClient("NetTcpBinding_IRoomService");

            try
            {
                int[] roomIds = proxy.GetVacantRoomIds();

                // Sort the room ID list
                Array.Sort(roomIds);

                foreach (int roomId in roomIds)
                {
                    comboBox1.Items.Add(roomId);
                }

                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the process
                MessageBox.Show("An error occurred while fetching room IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the service client when done
                if (proxy.State == CommunicationState.Opened)
                {
                    proxy.Close();
                }
            }
        }
    }
}
