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
    public partial class StaffList : Form
    {
        public StaffList()
        {
            InitializeComponent();
        }

        private void StaffList_Load(object sender, EventArgs e)
        {
            HM_Cient.StaffServiceReference.StaffServiceClient proxy = new HM_Cient.StaffServiceReference.StaffServiceClient("NetTcpBinding_IStaffService");
            DataSet ds=proxy.GetAllStaffs();
            dataGridView1.DataSource = ds.Tables["Staff"];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0) // Check if the clicked cell is in the Delete button column and not a header
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this staff?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the student ID from the selected row
                    int staffId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["staffId"].Value);

                    // Call the service to delete the student
                    HM_Cient.StaffServiceReference.StaffServiceClient proxy = new HM_Cient.StaffServiceReference.StaffServiceClient("NetTcpBinding_IStaffService");
                    int rowsAffected = proxy.DeleteStaff(staffId);

                    if (rowsAffected > 0)
                    {
                        // Reload the data after successful deletion
                        DataSet ds = proxy.GetAllStaffs();
                        dataGridView1.DataSource = ds.Tables["Staff"];
                        MessageBox.Show("Staff deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the staff.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (e.RowIndex >= 0) // Check if the clicked cell is not a header
            {
                if (e.ColumnIndex == 0) // Check if the clicked cell is in the Update button column
                {
                    // Get the data from the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    int staffId = Convert.ToInt32(selectedRow.Cells["staffId"].Value);
                    string firstName = selectedRow.Cells["firstName"].Value.ToString();
                    string middleName = selectedRow.Cells["middleName"].Value.ToString();
                    string lastName = selectedRow.Cells["lastName"].Value.ToString();
                    string email = selectedRow.Cells["email"].Value.ToString();
                    string phoneNo = selectedRow.Cells["phoneNo"].Value.ToString();
                    DateTime dob = Convert.ToDateTime(selectedRow.Cells["DateOfBirth"].Value);
                    DateTime dateOfJoining = Convert.ToDateTime(selectedRow.Cells["dateOfJoining"].Value);
                    string workType = selectedRow.Cells["WorkType"].Value.ToString();
                    int salary = Convert.ToInt32(selectedRow.Cells["salary"].Value);
                    string address = selectedRow.Cells["address"].Value.ToString();
                    HM_Cient.StaffServiceReference.Staff updatedStaff = new HM_Cient.StaffServiceReference.Staff
                    {
                        StaffId = staffId,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Email = email,
                        PhoneNo = phoneNo,
                        DateOfBirth = dob,
                        DateOfJoining = dateOfJoining,
                        Salary = salary,
                        Address = address,
                        WorkType = workType,
                    };

                    // Call the service to update the student
                    HM_Cient.StaffServiceReference.StaffServiceClient proxy = new HM_Cient.StaffServiceReference.StaffServiceClient("NetTcpBinding_IStaffService");
                    int rowsAffected = proxy.UpdateStaff(updatedStaff);

                    if (rowsAffected > 0)
                    {
                        // Reload the data after successful update
                        DataSet ds = proxy.GetAllStaffs();
                        dataGridView1.DataSource = ds.Tables["Staff"];
                        MessageBox.Show("Staff updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the staff.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HM_Cient.StaffServiceReference.StaffServiceClient proxy = new HM_Cient.StaffServiceReference.StaffServiceClient("NetTcpBinding_IStaffService");
            string searchKeyword = textBox1.Text.Trim();
            DataSet ds = proxy.GetStaffsByFirstName(searchKeyword);
            dataGridView1.DataSource = ds.Tables["Staff"];
        }
    }
}
