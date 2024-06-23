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
    public partial class HomePage : Form
    {
        FormHome home;
        AddStudent student;
        AddStaff staff;
        AddRoom room;
        AddFees fee;
        StudentList studentList;
        StaffList staffList;
        RoomList roomList;
        FeeList feeList;

        public HomePage()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
        bool student_expand = false;
        private void studentTransition_Tick(object sender, EventArgs e)
        {
            if (student_expand == false)
            {
                StudentContainer.Height += 10;
                if (StudentContainer.Height >= 168)
                {
                    studentTransition.Stop();
                    student_expand = true;
                }
            }
            else
            {
                StudentContainer.Height -= 10;
                if (StudentContainer.Height <= 51)
                {
                    studentTransition.Stop();
                    student_expand = false;

                }
            }
        }

        private void student_btn_Click(object sender, EventArgs e)
        {
            studentTransition.Start();
        }

        private void room_btn_Click(object sender, EventArgs e)
        {
            roomTransition.Start();
        }
        bool room_expand = false;
        private void roomTransition_Tick(object sender, EventArgs e)
        {
            if (room_expand == false)
            {
                RoomContainer.Height += 10;
                if (RoomContainer.Height >= 168)
                {
                    roomTransition.Stop();
                    room_expand = true;
                }
            }
            else
            {
                RoomContainer.Height -= 10;
                if (RoomContainer.Height <= 51)
                {
                    roomTransition.Stop();
                    room_expand = false;

                }
            }
        }

        private void staff_btn_Click(object sender, EventArgs e)
        {
            staffTransition.Start();
        }
        bool staff_expand = false;
        private void staffTransition_Tick(object sender, EventArgs e)
        {
            if (staff_expand == false)
            {
                StaffContainer.Height += 10;
                if (StaffContainer.Height >= 168)
                {
                    staffTransition.Stop();
                    staff_expand = true;
                }
            }
            else
            {
                StaffContainer.Height -= 10;
                if (StaffContainer.Height <= 51)
                {
                    staffTransition.Stop();
                    staff_expand = false;
                }
            }
        }
        bool sidebar_expand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebar_expand)
            {
                sidebar.Width -= 5;
                if (sidebar.Width <= 57)
                {
                    sidebar.FlowDirection = FlowDirection.TopDown;
                    sidebar_expand = false;
                    sidebarTransition.Stop();

                    pnHome.Width = sidebar.Width;
                    StudentContainer.Width = sidebar.Width;
                    RoomContainer.Width = sidebar.Width;
                    StaffContainer.Width = sidebar.Width;
                    FeeContainer.Width = sidebar.Width;
                }

            }
            else
            {
                sidebar.Width += 5;
                if (sidebar.Width >= 224)
                {
                    sidebar.FlowDirection = FlowDirection.LeftToRight;
                    sidebar_expand = true;
                    sidebarTransition.Stop();
                    pnHome.Width = sidebar.Width;
                    StudentContainer.Width = sidebar.Width;
                    RoomContainer.Width = sidebar.Width;
                    StaffContainer.Width = sidebar.Width;
                    FeeContainer.Width = sidebar.Width;
                }
            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (home == null)
            {
                home = new FormHome();
                home.FormClosed += Home_FormClosed;
                home.MdiParent = this;
                home.Dock = DockStyle.Fill;
                home.Show();
            }
            else
            {
                home.Activate();
            }
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            home = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (student == null)
            {
                student = new AddStudent();
                student.FormClosed += Student_FormClosed;
                student.MdiParent = this;
                student.Dock = DockStyle.Fill;
                student.Show();
            }
            else
            {
                student.Activate();
            }
        }

        private void Student_FormClosed(object sender, FormClosedEventArgs e)
        {
            student = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;

            if (room == null)
            {
                room = new AddRoom();
                room.FormClosed += Room_FormClosed;
                room.MdiParent = this;
                room.Dock = DockStyle.Fill;
                room.Show();
            }
            else
            {
                room.Activate();
            }

        }

        private void Room_FormClosed(object sender, FormClosedEventArgs e)
        {
            room = null;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (staff == null)
            {
                staff = new AddStaff();
                staff.FormClosed += Staff_FormClosed;
                staff.MdiParent = this;
                staff.Dock = DockStyle.Fill;
                staff.Show();
            }
            else
            {
                staff.Activate();
            }
        }

        private void Staff_FormClosed(object sender, FormClosedEventArgs e)
        {
            staff = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (studentList == null)
            {
                studentList = new StudentList();
                studentList.FormClosed += StudentList_FormClosed;
                studentList.MdiParent = this;
                studentList.Dock = DockStyle.Fill;
                studentList.Show();
            }
            else
            {
                studentList.Activate();
            }
        }

        private void StudentList_FormClosed(object sender, FormClosedEventArgs e)
        {
            studentList = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (roomList == null)
            {
                roomList = new RoomList();
                roomList.FormClosed += RoomList_FormClosed;
                roomList.MdiParent = this;
                roomList.Dock = DockStyle.Fill;
                roomList.Show();
            }
            else
            {
                roomList.Activate();
            }
        }
        private void RoomList_FormClosed(object sender, FormClosedEventArgs e)
        {
            roomList = null;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (staffList == null)
            {
                staffList = new StaffList();
                staffList.FormClosed += StaffList_FormClosed;
                staffList.MdiParent = this;
                staffList.Dock = DockStyle.Fill;
                staffList.Show();
            }
            else
            {
                staffList.Activate();
            }
        }
        private void StaffList_FormClosed(object sender, FormClosedEventArgs e)
        {
            staffList = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (fee == null)
            {
                fee = new AddFees();
                fee.FormClosed += Fee_FormClosed;
                fee.MdiParent = this;
                fee.Dock = DockStyle.Fill;
                fee.Show();
            }
            else
            {
                fee.Activate();
            }
        }

        private void Fee_FormClosed(object sender, FormClosedEventArgs e)
        {
            fee = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            home = null;
            student = null;
            staff = null;
            room = null;
            fee = null;
            studentList = null;
            staffList = null;
            roomList = null;
            feeList = null;
            if (feeList == null)
            {
                feeList = new FeeList();
                feeList.FormClosed += FeeList_FormClosed;
                feeList.MdiParent = this;
                feeList.Dock = DockStyle.Fill;
                feeList.Show();
            }
            else
            {
                feeList.Activate();
            }
        }

        private void FeeList_FormClosed(object sender, FormClosedEventArgs e)
        {
            feeList = null;
        }
        bool fee_expand = false;
        private void feeTransition_Tick(object sender, EventArgs e)
        {
            if (fee_expand == false)
            {
                FeeContainer.Height += 10;
                if (FeeContainer.Height >= 168)
                {
                    feeTransition.Stop();
                    fee_expand = true;
                }
            }
            else
            {
                FeeContainer.Height -= 10;
                if (FeeContainer.Height <= 51)
                {
                    feeTransition.Stop();
                    fee_expand = false;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            feeTransition.Start();
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}