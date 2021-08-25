using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;


namespace TCAssociationTool.DAL
{
    public class DBAccess
    {
        public DBAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        object obj = null;

        /// <summary>
        /// Gets data against db based on Stored procedure and parameters
        /// </summary>
        /// <param name="strStoredProcedureName">Procedure to be executed</param>
        /// <param name="sqlParams">Array of Parameters</param>
        /// <returns>Result Set as datatable</returns>

        public DataTable GetDataTable(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            DataTable dt = new DataTable();
            // Query the database and return the rowset.
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            using (cnn)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

                cnn.Open();
                cmd = new SqlCommand(strStoredProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlParams != null)
                {
                    cmd.Parameters.AddRange(sqlParams);
                }
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cnn.Close();
            }
            return dt;
        }

        public DataTable GetDataTableNoParm(string strStoredProcedureName)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            // Query the database and return the rowset.
            using (cnn)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

                cnn.Open();
                cmd = new SqlCommand(strStoredProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cnn.Close();
            }
            return dt;
        }

        public DataSet GetDataSet(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            using (cnn)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

                cnn.Open();
                cmd = new SqlCommand(strStoredProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlParams != null)
                {
                    cmd.Parameters.AddRange(sqlParams);
                }

                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cnn.Close();
            }
            return ds;
        }

        /// <summary>
        /// Sets data into db based on Stored procedure and parameters
        /// </summary>
        /// <param name="strStoredProcedureName">Stored procedure to be executed</param>
        /// <param name="sqlParams">Array of parameters</param>
        /// <returns>Status of operation</returns>

        public Int64 GetValue(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            Int64 i;
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            using (cnn)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

                cnn.Open();
                cmd = new SqlCommand(strStoredProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(sqlParams);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                    obj = dr[0];
                else
                    obj = null;

                i = Convert.ToInt64(obj);
                cnn.Close();
            }
            return i;
        }

        /// <summary>
        /// Gets data against db based on Stored procedure and procedure
        /// </summary>
        /// <param name="strStoredProcedureName">Stored procedure to be executed</param>
        /// <param name="sqlParams">Array of parameters</param>
        /// <returns></returns>

        public object SP_ExecuteScalar(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            using (cnn)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

                cnn.Open();
                cmd = new SqlCommand(strStoredProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 450;

                if (sqlParams != null)
                {
                    cmd.Parameters.AddRange(sqlParams);
                }
              
                return cmd.ExecuteScalar();
              
            }
        }

        public void Sp_GetDataReader(out SqlDataReader sqldr, string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            if (cnn.State == ConnectionState.Open)
                cnn.Close();

            cnn.Open();
            cmd = new SqlCommand(strStoredProcedureName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (sqlParams != null)
            {
                cmd.Parameters.AddRange(sqlParams);
            }

            sqldr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cnn.Close();
        }

        public object GetObject(ref SqlParameter[] sqlParams)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString);
            using (cnn)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

                cnn.Open();
                cmd = new SqlCommand("", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(sqlParams);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                    obj = dr[0];
                else
                    obj = null;
            }
            cnn.Close();
            return obj;
        }
    }
}