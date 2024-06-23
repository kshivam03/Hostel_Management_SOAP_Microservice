using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HM_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class StudentService : IStudentService
    {
        DataSet IStudentService.GetAllStudents()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT stu_id AS Id,first_name AS FirstName,middle_name AS MiddleName,last_name AS LastName,email AS Email,phone_no AS PhoneNo,dob AS DateOfBirth,date_of_joining AS DateOfJoining,room_id AS RoomNo,address AS Address from Student", @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            DataSet ds = new DataSet();
            da.Fill(ds, "Student");
            return ds;
        }

        Student IStudentService.GetStudent(int id)
        {
            SqlConnection cn = new SqlConnection(
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "Select stu_id,first_name,middle_name,last_name,email,phone_no,dob,date_of_joining,room_id,address from Student where stu_id=@id";
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Student student = new Student();
            while (reader.Read())
            {
                student.StudentId = reader.GetInt32(0);
                student.FirstName = reader.GetString(1);
                student.MiddleName = reader.GetString(2);
                student.LastName = reader.GetString(3);
                student.Email = reader.GetString(4);
                student.PhoneNo = reader.GetString(5);
                student.DateOfBirth = reader.GetDateTime(6);
                student.DateOfJoining = reader.GetDateTime(7);
                student.RoomID = reader.GetInt32(8);
                student.Address = reader.GetString(9);

            }
            reader.Close();
            cn.Close();
            return student;
        }
        int IStudentService.AddStudent(Student student)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();  // Open the connection before starting the transaction

                // Begin a transaction
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // Insert the student
                    SqlCommand cmdStudent = new SqlCommand();
                    cmdStudent.Connection = cn;
                    cmdStudent.Transaction = transaction;
                    cmdStudent.CommandText = "INSERT INTO Student (first_name, middle_name, last_name, email, phone_no, dob, date_of_joining, room_id, address) " +
                                             "VALUES (@firstName, @middleName, @lastName, @email, @phoneNo, @dob, @dateOfJoining, @roomId, @address);";

                    cmdStudent.Parameters.AddWithValue("@firstName", student.FirstName);
                    cmdStudent.Parameters.AddWithValue("@middleName", student.MiddleName);
                    cmdStudent.Parameters.AddWithValue("@lastName", student.LastName);
                    cmdStudent.Parameters.AddWithValue("@email", student.Email);
                    cmdStudent.Parameters.AddWithValue("@phoneNo", student.PhoneNo);
                    cmdStudent.Parameters.AddWithValue("@dob", student.DateOfBirth);
                    cmdStudent.Parameters.AddWithValue("@dateOfJoining", student.DateOfJoining);
                    cmdStudent.Parameters.AddWithValue("@roomId", student.RoomID);
                    cmdStudent.Parameters.AddWithValue("@address", student.Address);

                    int rowsAffected = cmdStudent.ExecuteNonQuery();

                    // Update the room's bed vacancy
                    SqlCommand cmdUpdateRoom = new SqlCommand();
                    cmdUpdateRoom.Connection = cn;
                    cmdUpdateRoom.Transaction = transaction;
                    cmdUpdateRoom.CommandText = "UPDATE Room SET bed_vacancy = bed_vacancy - 1 WHERE room_id = @roomId";
                    cmdUpdateRoom.Parameters.AddWithValue("@roomId", student.RoomID);

                    // Execute the commands
                    cmdUpdateRoom.ExecuteNonQuery();

                    // Commit the transaction
                    transaction.Commit();

                    return rowsAffected;  // Return the generated student ID if needed
                }
                catch (Exception ex)
                {
                    // An error occurred, rollback the transaction
                    transaction.Rollback();
                    // Handle the exception or log it
                    throw;
                }
            }
        }



        int IStudentService.UpdateStudent(Student student)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE Student SET first_name = @firstName, middle_name = @middleName, last_name = @lastName, " +
                                  "email = @email, phone_no = @phoneNo, dob = @dob, date_of_joining = @dateOfJoining, " +
                                  "room_id = @roomId, address = @address WHERE stu_id = @id";

                cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                cmd.Parameters.AddWithValue("@middleName", student.MiddleName);
                cmd.Parameters.AddWithValue("@lastName", student.LastName);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@phoneNo", student.PhoneNo);
                cmd.Parameters.AddWithValue("@dob", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@dateOfJoining", student.DateOfJoining);
                cmd.Parameters.AddWithValue("@roomId", student.RoomID);
                cmd.Parameters.AddWithValue("@address", student.Address);
                cmd.Parameters.AddWithValue("@id", student.StudentId);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();

                return rowsAffected;
            }
        }

        int IStudentService.DeleteStudent(int id)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();  // Open the connection before starting the transaction

                // Begin a transaction
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // Get the room ID of the student before deleting
                    SqlCommand cmdGetRoomId = new SqlCommand();
                    cmdGetRoomId.Connection = cn;
                    cmdGetRoomId.Transaction = transaction;
                    cmdGetRoomId.CommandText = "SELECT room_id FROM Student WHERE stu_id = @id";
                    cmdGetRoomId.Parameters.AddWithValue("@id", id);

                    int roomId = (int)cmdGetRoomId.ExecuteScalar();

                    // Delete the student
                    SqlCommand cmdDeleteStudent = new SqlCommand();
                    cmdDeleteStudent.Connection = cn;
                    cmdDeleteStudent.Transaction = transaction;
                    cmdDeleteStudent.CommandText = "DELETE FROM Student WHERE stu_id = @id";
                    cmdDeleteStudent.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmdDeleteStudent.ExecuteNonQuery();

                    // Update the room's bed vacancy
                    SqlCommand cmdUpdateRoom = new SqlCommand();
                    cmdUpdateRoom.Connection = cn;
                    cmdUpdateRoom.Transaction = transaction;
                    cmdUpdateRoom.CommandText = "UPDATE Room SET bed_vacancy = bed_vacancy + 1 WHERE room_id = @roomId";
                    cmdUpdateRoom.Parameters.AddWithValue("@roomId", roomId);

                    // Execute the commands
                    cmdUpdateRoom.ExecuteNonQuery();

                    // Commit the transaction
                    transaction.Commit();

                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    // An error occurred, rollback the transaction
                    transaction.Rollback();
                    // Handle the exception or log it
                    throw;
                }
            }
        }
        int IStudentService.NoOfStudents()
        {
            using (SqlConnection cn = new SqlConnection(
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT COUNT(*) FROM Student";

                cn.Open();
                int rowCount = (int)cmd.ExecuteScalar();
                cn.Close();

                return rowCount;
            }
        }

        int IStudentService.PendingFees(int id)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                try
                {
                    // Retrieve student details
                    SqlCommand cmdStudent = new SqlCommand();
                    cmdStudent.Connection = cn;
                    cmdStudent.CommandText = "SELECT stu_id, date_of_joining, room_id FROM Student WHERE stu_id = @studentId";
                    cmdStudent.Parameters.AddWithValue("@studentId", id);

                    SqlDataReader readerStudent = cmdStudent.ExecuteReader();
                    if (!readerStudent.Read())
                    {
                        // Student not found
                        readerStudent.Close();
                        return -1;
                    }

                    int studentId = readerStudent.GetInt32(0);
                    DateTime dateOfJoining = readerStudent.GetDateTime(1);
                    int roomId = readerStudent.GetInt32(2);

                    readerStudent.Close();

                    // Retrieve RoomType from the Room table using the student's RoomID
                    string roomType;
                    using (SqlCommand cmdRoomType = new SqlCommand("SELECT room_type FROM Room WHERE room_id = @roomId", cn))
                    {
                        cmdRoomType.Parameters.AddWithValue("@roomId", roomId);
                        roomType = Convert.ToString(cmdRoomType.ExecuteScalar());
                    }

                    // Set the roomPrice based on the retrieved roomType
                    decimal roomPrice = (roomType == "AC") ? 9000 : 7000;

                    // Calculate fees details
                    DateTime currentDate = DateTime.Now;
                    int monthsSinceJoining = (currentDate.Year - dateOfJoining.Year) * 12 + currentDate.Month - dateOfJoining.Month;

                    // Retrieve Total Paid Amount from Fees table
                    SqlCommand cmdTotalPaidAmount = new SqlCommand("SELECT ISNULL(SUM(paid_amount), 0) FROM Fees WHERE stu_id = @studentId", cn);
                    cmdTotalPaidAmount.Parameters.AddWithValue("@studentId", studentId);

                    decimal totalPaidAmount = Convert.ToDecimal(cmdTotalPaidAmount.ExecuteScalar());

                    // Calculate pending fees
                    decimal pendingFees = (monthsSinceJoining * roomPrice) - totalPaidAmount;

                    return (int)pendingFees;
                }
                catch (Exception ex)
                {
                    // Handle the exception or log it
                    throw;
                }
                finally
                {
                    cn.Close();
                }
            }
        }
        private int[] GetAllStudentIds()
        {
            List<int> studentIds = new List<int>();

            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                try
                {
                    SqlCommand cmdGetStudentIds = new SqlCommand("SELECT stu_id FROM Student", cn);

                    using (SqlDataReader reader = cmdGetStudentIds.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int studentId = reader.GetInt32(0);
                            studentIds.Add(studentId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception or log it
                    throw;
                }
                finally
                {
                    cn.Close();
                }
            }

            return studentIds.ToArray();
        }

        int IStudentService.CountStudentsWithPendingFees()
        {
            IStudentService studentService = new StudentService();
            int total_students = studentService.NoOfStudents();
            int[] ids = GetAllStudentIds();
            int count = 0;
            for(int i = 0; i < total_students; i++)
            {
                if (studentService.PendingFees(ids[i]) > 0)
                {
                    count++;
                }
            }
            return count; ;
        }

        public DataSet GetStudentsByFirstName(string firstName)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                string query = "SELECT * FROM Student WHERE first_name LIKE @FirstName";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@FirstName", "%" + firstName + "%");
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Student");
                return dataSet;
            }
        }

    }


}
