using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DACN_IS385BB_TruongTienDat_6725
{
    public class dbClass
    {
        public DataTable GetData(string sql, SqlParameter[] parameters)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Thêm tham số vào lệnh SQL
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }

        public DataTable GetData(string sql) // Overload cho ObjectDataSource
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

        public void RunQuery(string sql, params SqlParameter[] parameters)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand myCom = new SqlCommand(sql, connection))
                    {
                        myCom.CommandType = CommandType.Text;
                        if (parameters != null)
                        {
                            myCom.Parameters.AddRange(parameters);
                        }
                        myCom.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error in RunQuery: " + ex.Message);
                    throw;
                }
            }
        }

        public SqlDataReader GetRecord(string sql, params SqlParameter[] parameters)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(conStr);
            try
            {
                connection.Open();
                SqlCommand myCom = new SqlCommand(sql, connection);
                myCom.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    myCom.Parameters.AddRange(parameters);
                }
                return myCom.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetRecord: " + ex.Message);
                connection.Close();
                throw;
            }
        }

        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}