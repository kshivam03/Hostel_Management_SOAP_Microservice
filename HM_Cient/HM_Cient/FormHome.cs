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
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            HM_Cient.StudentServiceReference.StudentServiceClient proxy1 = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
            label4.Text = proxy1.NoOfStudents().ToString();
            HM_Cient.RoomServiceReference.RoomServiceClient proxy2 = new HM_Cient.RoomServiceReference.RoomServiceClient("NetTcpBinding_IRoomService");
            label5.Text = proxy2.NoOfRooms().ToString();
            HM_Cient.StaffServiceReference.StaffServiceClient proxy3 = new HM_Cient.StaffServiceReference.StaffServiceClient("NetTcpBinding_IStaffService");
            label6.Text = proxy3.NoOfStaffs().ToString();
            int x=proxy1.CountStudentsWithPendingFees();
            int y = proxy1.NoOfStudents();
            circularProgressBar1.Value = (x * 100/ y);
            circularProgressBar1.Text = (x *100/ y).ToString()+"%";
            int a = proxy2.GetTotalVacantRooms();
            int b = proxy2.NoOfRooms();
            circularProgressBar2.Value = (a * 100 / b);
            circularProgressBar2.Text = (a * 100 / b).ToString() + "%";

        }
    }
}
