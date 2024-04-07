using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01.Home.Manage.Service
{
    internal class Service
    {
            MY_DB mydb = new MY_DB();
            public string id;
            public string name;
            public double price;

            public Service() { }
            public DataTable getServices(SqlCommand command)
            {
                command.Connection = mydb.getConnection;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable TABLE = new DataTable();
                adapter.Fill(TABLE);
                return TABLE;
            }
            public bool addService(string id, string name, double price)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Service (id,name,price) " +
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
            public bool updateService(string id, string name, double price)
            {
                SqlCommand command = new SqlCommand("UPDATE Service " +
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
            public bool DeleteService(string id)
            {
                SqlCommand command = new SqlCommand("Delete from Service where id = @id", mydb.getConnection);
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
        public DataTable getAllServices()
        {

                DataTable servicesDataTable = new DataTable();

                string query = "SELECT * FROM service"; // Thay thế 'Courses' bằng tên bảng thực tế trong cơ sở dữ liệu của bạn

                using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
                {
                    mydb.getConnection.Close();
                    mydb.getConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                // Tạo các cột trong DataTable tương ứng với cột trong kết quả truy vấn
                servicesDataTable.Columns.Add("Id", typeof(string));
                servicesDataTable.Columns.Add("name", typeof(string));
                servicesDataTable.Columns.Add("Price", typeof(int));


                    while (reader.Read())
                    {
                        // Tạo một hàng mới cho mỗi bản ghi và thêm vào DataTable
                        DataRow row = servicesDataTable.NewRow();
                        row["Id"] = reader["Id"].ToString();
                        row["Name"] = reader["name"].ToString();
                        row["Price"] = Convert.ToInt32(reader["Price"]);
                    servicesDataTable.Rows.Add(row);
                    }

                    reader.Close();
                }

                return servicesDataTable;
            }

        }
   
    }

