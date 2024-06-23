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
    public partial class StudentList : Form
    {
        AddFees addFeesForm;

        public StudentList()
        {
            InitializeComponent();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            HM_Cient.StudentServiceReference.StudentServiceClient proxy = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
            DataSet ds = proxy.GetAllStudents();
            dataGridView1.DataSource = ds.Tables["Student"];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0) // Check if the clicked cell is in the Delete button column and not a header
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the student ID from the selected row
                    int studentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stu_id"].Value);

                    // Call the service to delete the student
                    HM_Cient.StudentServiceReference.StudentServiceClient proxy = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
                    int rowsAffected = proxy.DeleteStudent(studentId);

                    if (rowsAffected > 0)
                    {
                        // Reload the data after successful deletion
                        DataSet ds = proxy.GetAllStudents();
                        dataGridView1.DataSource = ds.Tables["Student"];
                        MessageBox.Show("Student deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (e.RowIndex >= 0) // Check if the clicked cell is not a header
            {
                if (e.ColumnIndex == 1) // Check if the clicked cell is in the Update button column
                {
                    // Get the data from the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    int studentId = Convert.ToInt32(selectedRow.Cells["id"].Value);
                    string firstName = selectedRow.Cells["firstName"].Value.ToString();
                    string middleName = selectedRow.Cells["middleName"].Value.ToString();
                    string lastName = selectedRow.Cells["lastName"].Value.ToString();
                    string email = selectedRow.Cells["email"].Value.ToString();
                    string phoneNo = selectedRow.Cells["phoneNo"].Value.ToString();
                    DateTime dob = Convert.ToDateTime(selectedRow.Cells["dateOfBirth"].Value);
                    DateTime dateOfJoining = Convert.ToDateTime(selectedRow.Cells["dateOfJoining"].Value);
                    int roomId = Convert.ToInt32(selectedRow.Cells["roomNo"].Value);
                    string address = selectedRow.Cells["address"].Value.ToString();
                    HM_Cient.StudentServiceReference.Student updatedStudent = new HM_Cient.StudentServiceReference.Student
                    {
                        StudentId = studentId,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Email = email,
                        PhoneNo = phoneNo,
                        DateOfBirth = dob,
                        DateOfJoining = dateOfJoining,
                        RoomID = roomId,
                        Address = address
                    };

                    // Call the service to update the student
                    HM_Cient.StudentServiceReference.StudentServiceClient proxy = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
                    int rowsAffected = proxy.UpdateStudent(updatedStudent);

                    if (rowsAffected > 0)
                    {
                        // Reload the data after successful update
                        DataSet ds = proxy.GetAllStudents();
                        dataGridView1.DataSource = ds.Tables["Student"];
                        MessageBox.Show("Student updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.ColumnIndex == dataGridView1.Columns["PayFeeButton"].Index && e.RowIndex >= 0)
                {
                    int studentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                    if (addFeesForm == null)
                    {
                        addFeesForm = new AddFees();
                        addFeesForm.FormClosed += AddFeesFormClosed;
                        addFeesForm.StudentId = studentId;
                        addFeesForm.MdiParent = HomePage.ActiveForm;
                        addFeesForm.Dock = DockStyle.Fill;
                        addFeesForm.Show();
                    }
                    else
                    {
                        addFeesForm.Activate();
                    }
                }
            }
        }
        private void AddFeesFormClosed(object sender, FormClosedEventArgs e)
        {
            addFeesForm = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HM_Cient.StudentServiceReference.StudentServiceClient proxy = new HM_Cient.StudentServiceReference.StudentServiceClient("NetTcpBinding_IStudentService");
            string searchKeyword = textBox1.Text.Trim();
            DataSet ds = proxy.GetStudentsByFirstName(searchKeyword);
            dataGridView1.DataSource = ds.Tables["Student"];
        }
    }
}