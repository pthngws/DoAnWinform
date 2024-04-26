using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01.Home.Manage.Dentist
{
    internal class Dentist
    {
        MY_DB mydb = new MY_DB();
        private DataTable table = new DataTable();
        public Dentist(string id,string name)
        {
            this.id = id;
            this.name = name;
            
        }
        public Dentist() { }
        public string id;
        public string name;
        public string address;
        public string phone;
        public DateTime Dob;
        public string Gender;

        public bool insertDentist(string id, string name, string address, string phone, DateTime dob, string gender)
        {
            SqlCommand command = new SqlCommand("INSERT INTO dentist (id,name, address, phone, dob, gender) " +
                                                "VALUES (@id,@name, @address, @phone, @dob, @gender)", mydb.getConnection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@dob", dob);
            command.Parameters.AddWithValue("@gender", gender);

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public bool UpdateDentist(string id, string name, string address, string phone, DateTime dob, string gender)
        {
            SqlCommand command = new SqlCommand("UPDATE dentist " +
                                                "SET name = @name, address = @address, phone = @phone, dob = @dob, gender = @gender " +
                                                "WHERE id = @id", mydb.getConnection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@dob", dob);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@id", id);
            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
      

        public bool DeleteDentist(string id)
        {
            SqlCommand command = new SqlCommand("Delete from dentist where id = @id", mydb.getConnection);
            command.Parameters.AddWithValue("@id", id);
            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
        public DataTable getDentists(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;
        }
    }
}
