using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HM_Service
{
    public class FeesService : IFeesService
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HostelManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public DataSet GetAllFees()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT fee_id, stu_id, paid_amount, payment_date FROM Fees", connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds, "Fees");
            return ds;
        }

        public DataSet StudentAllFees(int s_id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT fee_id, stu_id, paid_amount, payment_date FROM Fees WHERE stu_id = @studentId";
                cmd.Parameters.AddWithValue("@studentId", s_id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentFees");

                return ds;
            }
        }
        public int AddFees(Fees fee)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "INSERT INTO Fees (stu_id, paid_amount, payment_date) " +
                                  "VALUES (@studentId, @paidAmount, @paymentDate);";

                cmd.Parameters.AddWithValue("@studentId", fee.StudentId);
                cmd.Parameters.AddWithValue("@paidAmount", fee.PaidAmount);
                cmd.Parameters.AddWithValue("@paymentDate", fee.PaymentDate);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();

                return rowsAffected;
            }
        }
    }

}
