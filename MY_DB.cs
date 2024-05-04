using System;
using System.Data;
using System.Data.SqlClient;

namespace DoAn01
{
    class MY_DB : IDisposable
    {
        private SqlConnection con;

        public MY_DB()
        {
            con = new SqlConnection(@"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=DentalSmile;Integrated Security=True");
        }

        // get the connection
        public SqlConnection getConnection
        {
            get
            {
                return con;
            }
        }

        // open the connection
        public void openConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening connection: " + ex.Message);
                // Handle the exception as needed
            }
        }

        // close the connection
        public void closeConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error closing connection: " + ex.Message);
                // Handle the exception as needed
            }
        }

        // Implement IDisposable to properly release resources
        public void Dispose()
        {
            closeConnection();
            con.Dispose();
        }
    }
}
