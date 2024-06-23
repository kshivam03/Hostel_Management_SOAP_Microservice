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
    public partial class AddRoom : Form
    {
        public AddRoom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HM_Cient.RoomServiceReference.RoomServiceClient proxy = new HM_Cient.RoomServiceReference.RoomServiceClient("NetTcpBinding_IRoomService");
            HM_Cient.RoomServiceReference.Room room = new HM_Cient.RoomServiceReference.Room();
            room.RoomId = int.Parse(textBox1.Text);
            room.BedVacancy = int.Parse(textBox2.Text);
            room.RoomType = comboBox1.SelectedItem.ToString();
            
            int n = proxy.AddRoom(room);

            if (n == 1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedItem = null;

                panel1.Visible = true;
            }

            proxy.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void AddRoom_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("AC");
            comboBox1.Items.Add("NON-AC");
        }
    }
}
