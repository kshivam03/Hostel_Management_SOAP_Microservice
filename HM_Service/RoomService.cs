using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace HM_Service
{
    public class RoomService : IRoomService
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public DataSet GetAllRooms()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT room_id, room_type, bed_vacancy FROM Room", connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds, "Room");
            return ds;
        }

        public Room GetRoom(int id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT room_id, room_type, bed_vacancy FROM Room WHERE room_id = @id";
                SqlParameter p = new SqlParameter("@id", id);
                cmd.Parameters.Add(p);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Room room = new Room();
                while (reader.Read())
                {
                    room.RoomId = reader.GetInt32(0);
                    room.RoomType = reader.GetString(1);
                    room.BedVacancy = reader.GetInt32(2);
                }
                reader.Close();
                cn.Close();
                return room;
            }
        }

        public int AddRoom(Room room)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "INSERT INTO Room (room_id, room_type, bed_vacancy) VALUES (@roomId, @roomType, @bedVacancy)";
                cmd.Parameters.AddWithValue("@roomId", room.RoomId);
                cmd.Parameters.AddWithValue("@roomType", room.RoomType);
                cmd.Parameters.AddWithValue("@bedVacancy", room.BedVacancy);
                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rowsAffected;
            }
        }

        public int UpdateRoom(Room room)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE Room SET room_type = @roomType, bed_vacancy = @bedVacancy WHERE room_id = @roomId";
                cmd.Parameters.AddWithValue("@roomType", room.RoomType);
                cmd.Parameters.AddWithValue("@bedVacancy", room.BedVacancy);
                cmd.Parameters.AddWithValue("@roomId", room.RoomId);
                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rowsAffected;
            }
        }

        public int DeleteRoom(int id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "DELETE FROM Room WHERE room_id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rowsAffected;
            }
        }
        public List<int> GetVacantRoomIdsWithAC()
        {
            return GetVacantRoomIdsByRoomType("AC");
        }

        public List<int> GetVacantRoomIdsWithNonAC()
        {
            return GetVacantRoomIdsByRoomType("NON-AC");
        }

        public List<int> GetVacantRoomIds()
        {
            List<int> vacantRoomIdsAC = GetVacantRoomIdsWithAC();
            List<int> vacantRoomIdsNonAC = GetVacantRoomIdsWithNonAC();

            // Combine the lists and get the distinct room IDs
            List<int> combinedVacantRoomIds = vacantRoomIdsAC.Concat(vacantRoomIdsNonAC).Distinct().ToList();

            // Return the count of distinct vacant room IDs
            return combinedVacantRoomIds;
        }

        private List<int> GetVacantRoomIdsByRoomType(string roomType)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT room_id FROM Room WHERE bed_vacancy > 0 AND room_type = @roomType";
                cmd.Parameters.AddWithValue("@roomType", roomType);

                List<int> vacantRoomIds = new List<int>();

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    vacantRoomIds.Add(reader.GetInt32(0));
                }

                reader.Close();
                cn.Close();

                return vacantRoomIds;
            }
        }
        public int GetTotalVacantRooms()
        {
            List<int> vacantRoomIdsAC = GetVacantRoomIdsWithAC();
            List<int> vacantRoomIdsNonAC = GetVacantRoomIdsWithNonAC();

            // Combine the lists and get the distinct room IDs
            List<int> combinedVacantRoomIds = vacantRoomIdsAC.Concat(vacantRoomIdsNonAC).Distinct().ToList();

            // Return the count of distinct vacant room IDs
            return combinedVacantRoomIds.Count;
        }

        int IRoomService.NoOfRooms()
        {
            using (SqlConnection cn = new SqlConnection(
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT COUNT(*) FROM Room";

                cn.Open();
                int rowCount = (int)cmd.ExecuteScalar();
                cn.Close();

                return rowCount;
            }
        }
        public List<string> GetStudentsInRoom(int roomId)
        {
            List<string> studentNames = new List<string>();

            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                // Retrieve the names of students in the specified room
                cmd.CommandText = "SELECT first_name, last_name FROM Student WHERE room_id = @roomId";
                cmd.Parameters.AddWithValue("@roomId", roomId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        string fullName = $"{firstName} {lastName}";
                        studentNames.Add(fullName);
                    }
                }
            }

            return studentNames;
        }

    }

}
