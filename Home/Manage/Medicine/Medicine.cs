using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01.Home.Manage.Medicine
{
    internal class Medicine
    {
        MY_DB mydb = new MY_DB();
        public string id;
        public string name;
        public double price;

        public Medicine() { }
        public DataTable getMedicines(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;
        }
        public bool addMedicine(string id, string name, double price)
        {
            SqlCommand command = new SqlCommand("INSERT INTO medicine (id,name,price) " +
                                                "VALUES (@id,@name, @price)", mydb.getConnection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@price", price);

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
        public bool updateMedicine(string id, string name, double price)
        {
            SqlCommand command = new SqlCommand("UPDATE medicine " +
                                                "SET name = @name, price=@price " +
                                                "WHERE id = @id", mydb.getConnection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@price", price);
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
        public bool DeleteMedicine(string id)
        {
            SqlCommand command = new SqlCommand("Delete from medicine where id = @id", mydb.getConnection);
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
    }
}
