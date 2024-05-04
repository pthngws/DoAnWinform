using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn01;

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

        string username;
        string password;
        string email;
        public Dentist(string username, string password,string x)
        {
            this.username = username;
            this.password = password;
            this.email = x;
        }

        MY_DB db = new MY_DB();

        public Dentist LoginDentist()
        {
            try
            {
                Dentist user = new Dentist();
                db.openConnection();

                string query = "SELECT * FROM [Dentist] WHERE id = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, db.getConnection);

                // Thêm tham số và giá trị cho tham số
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader dataReader = cmd.ExecuteReader();

                // Kiểm tra xem có dữ liệu trong dataReader hay không
                if (dataReader.Read())
                {

                    user.username = dataReader["id"].ToString();
                    user.password = dataReader["password"].ToString();
                    user.email = dataReader["email"].ToString();
                    db.closeConnection();
                    return user; // Người dùng tồn tại và mật khẩu khớp
                }
                else
                {
                    db.closeConnection();
                    MessageBox.Show("An account failed to log on. Please check your username and password.");
                    return null; // Người dùng không tồn tại hoặc mật khẩu không khớp
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return null;
            }
        }
        public void CreateDentist()
        {
            using (SqlConnection connection = db.getConnection)
            {
                string checkUsernameQuery = "SELECT COUNT(*) FROM [dentist] WHERE id = @username";


                using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection))
                {
                    checkUsernameCommand.Parameters.AddWithValue("@username", username);

                    try
                    {
                        connection.Open();
                        int usernameCount = (int)checkUsernameCommand.ExecuteScalar();


                        if (usernameCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.");
                            return;
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking username: " + ex.Message);
                        return;
                    }
                }
                string checkEmailQuery = "SELECT COUNT(*) FROM [dentist] WHERE email = @email";
                using (SqlCommand checkEmailCommand = new SqlCommand(checkEmailQuery, connection))
                {
                    checkEmailCommand.Parameters.AddWithValue("@email", email);

                    try
                    {
                        connection.Open();
                        int emailCount = (int)checkEmailCommand.ExecuteScalar();


                        if (emailCount > 0)
                        {
                            MessageBox.Show("Email already exists. Please choose a different email.");
                            return;
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking email: " + ex.Message);
                        return;
                    }
                }
                string insertQuery = "INSERT INTO [dentist] (id, password, email) VALUES (@username, @password, @email)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Successful");
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting data: " + ex.Message);
                    }
                }
            }
        }
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

        public Dentist GetDentistById(string id)
        {
            try
            {
                Dentist dentist = null;
                string query = "SELECT * FROM [dentist] WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, mydb.getConnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    mydb.openConnection();

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            dentist = new Dentist();
                            dentist.id = dataReader["id"].ToString();
                            dentist.name = dataReader["name"].ToString();
                            dentist.address = dataReader["address"].ToString();
                            dentist.phone = dataReader["phone"].ToString();
                            dentist.Dob = dataReader.IsDBNull(dataReader.GetOrdinal("dob")) ? DateTime.MinValue : Convert.ToDateTime(dataReader["dob"]);
                            dentist.Gender = dataReader["gender"].ToString();
                        }
                    }
                }
                mydb.closeConnection();
                return dentist;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when querying dentist information: " + ex.Message);
                return null;
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
