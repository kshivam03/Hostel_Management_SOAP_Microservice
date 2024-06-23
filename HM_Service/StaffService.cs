using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HM_Service
{
    public class StaffService : IStaffService
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public DataSet GetAllStaffs()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT staff_id AS StaffId, first_name AS FirstName, middle_name AS MiddleName, last_name AS LastName, email AS Email, phone_no AS PhoneNo, date_of_joining AS DateOfJoining, work_type AS WorkType, dob AS DateOfBirth, address AS Address, salary AS Salary FROM Staff", connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds, "Staff");
            return ds;
        }

        public Staff GetStaff(int id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT staff_id, first_name, middle_name, last_name, email, phone_no, date_of_joining, work_type, dob, address, salary FROM Staff WHERE staff_id = @id";
                SqlParameter p = new SqlParameter("@id", id);
                cmd.Parameters.Add(p);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Staff staff = new Staff();
                while (reader.Read())
                {
                    staff.StaffId = reader.GetInt32(0);
                    staff.FirstName = reader.GetString(1);
                    staff.MiddleName = reader.GetString(2);
                    staff.LastName = reader.GetString(3);
                    staff.Email = reader.GetString(4);
                    staff.PhoneNo = reader.GetString(5);
                    staff.DateOfJoining = reader.GetDateTime(6);
                    staff.WorkType = reader.GetString(8);
                    staff.DateOfBirth = reader.GetDateTime(9);
                    staff.Address = reader.GetString(10);
                    staff.Salary = reader.GetInt32(11);
                }
                reader.Close();
                cn.Close();
                return staff;
            }
        }

        public int AddStaff(Staff staff)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "INSERT INTO Staff (first_name, middle_name, last_name, email, phone_no, date_of_joining, work_type, dob, address, salary) " +
                                  "VALUES (@firstName, @middleName, @lastName, @email, @phoneNo, @dateOfJoining, @workType, @dob, @address, @salary)";
                cmd.Parameters.AddWithValue("@firstName", staff.FirstName);
                cmd.Parameters.AddWithValue("@middleName", staff.MiddleName);
                cmd.Parameters.AddWithValue("@lastName", staff.LastName);
                cmd.Parameters.AddWithValue("@email", staff.Email);
                cmd.Parameters.AddWithValue("@phoneNo", staff.PhoneNo);
                cmd.Parameters.AddWithValue("@dateOfJoining", staff.DateOfJoining);
                cmd.Parameters.AddWithValue("@workType", staff.WorkType);
                cmd.Parameters.AddWithValue("@dob", staff.DateOfBirth);
                cmd.Parameters.AddWithValue("@address", staff.Address);
                cmd.Parameters.AddWithValue("@salary", staff.Salary);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rowsAffected;
            }
        }

        public int UpdateStaff(Staff staff)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE Staff SET first_name = @firstName, middle_name = @middleName, last_name = @lastName, " +
                                  "email = @email, phone_no = @phoneNo, date_of_joining = @dateOfJoining, " +
                                  "work_type = @workType, dob = @dob, address = @address, salary = @salary WHERE staff_id = @id";
                cmd.Parameters.AddWithValue("@firstName", staff.FirstName);
                cmd.Parameters.AddWithValue("@middleName", staff.MiddleName);
                cmd.Parameters.AddWithValue("@lastName", staff.LastName);
                cmd.Parameters.AddWithValue("@email", staff.Email);
                cmd.Parameters.AddWithValue("@phoneNo", staff.PhoneNo);
                cmd.Parameters.AddWithValue("@dateOfJoining", staff.DateOfJoining);
                cmd.Parameters.AddWithValue("@workType", staff.WorkType);
                cmd.Parameters.AddWithValue("@dob", staff.DateOfBirth);
                cmd.Parameters.AddWithValue("@address", staff.Address);
                cmd.Parameters.AddWithValue("@salary", staff.Salary);
                cmd.Parameters.AddWithValue("@id", staff.StaffId);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rowsAffected;
            }
        }

        public int DeleteStaff(int id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "DELETE FROM Staff WHERE staff_id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rowsAffected;
            }
        }
        int IStaffService.NoOfStaffs()
        {
            using (SqlConnection cn = new SqlConnection(
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT COUNT(*) FROM Staff";

                cn.Open();
                int rowCount = (int)cmd.ExecuteScalar();
                cn.Close();

                return rowCount;
            }
        }

        public DataSet GetStaffsByFirstName(string firstName)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                string query = "SELECT * FROM Staff WHERE first_name LIKE @FirstName";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@FirstName", "%" + firstName + "%");
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Staff");
                return dataSet;
            }
        }
    }

}
