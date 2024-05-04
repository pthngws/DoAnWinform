using DoAn01;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace DoAn01
{
    public class Staff
    {
        public Staff() { }
        public string password;
        public string email;
        public string id;
        public string name;
        public string address;
        public string phone;
        public DateTime Dob;
        public string Gender;
        public MemoryStream picture;
        public Staff(string id,string password) {
            this.id = id;
            this.password = password;
        }
        public Staff(string id, string password, string email)
        {
            this.id = id;
            this.email = email;
            this.password = password;
        }

        MY_DB db = new MY_DB();
        MY_DB mydb = new MY_DB();
        public void CreateStaff()
        {
            using (SqlConnection connection = db.getConnection)
            {
                string checkUsernameQuery = "SELECT COUNT(*) FROM [staff] WHERE id = @id";


                using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection))
                {
                    checkUsernameCommand.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int usernameCount = (int)checkUsernameCommand.ExecuteScalar();


                        if (usernameCount > 0)
                        {
                            MessageBox.Show("id already exists. Please choose a different id.");
                            return;
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking id: " + ex.Message);
                        return;
                    }
                }
                string checkEmailQuery = "SELECT COUNT(*) FROM [staff] WHERE email = @email";
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
                string insertQuery = "INSERT INTO [staff] (id, password, email) VALUES (@id, @password, @email)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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
        public Staff LoginStaff()
        {
            try
            {
                Staff staff = new Staff();
                db.openConnection();

                string query = "SELECT * FROM [Staff] WHERE id = @id AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, db.getConnection);

                // Thêm tham số và giá trị cho tham số
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader dataReader = cmd.ExecuteReader();

                // Kiểm tra xem có dữ liệu trong dataReader hay không
                if (dataReader.Read())
                {

                    staff.id = dataReader["id"].ToString();
                    staff.password = dataReader["password"].ToString();
                    staff.email = dataReader["email"].ToString();
                    db.closeConnection();
                    return staff; // Người dùng tồn tại và mật khẩu khớp
                }
                else
                {
                    db.closeConnection();
                    MessageBox.Show("An account failed to log on. Please check your id and password.");
                    return null; // Người dùng không tồn tại hoặc mật khẩu không khớp
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return null;
            }
        }
        public DataTable GetStaffData()
        {
            DataTable dataTable = new DataTable(); // Tạo một DataTable để lưu trữ dữ liệu

            MY_DB mydb = new MY_DB();
            string query = "SELECT id, name FROM staff"; // Câu lệnh SQL để lấy ID và tên từ bảng nhân viên

            using (SqlConnection connection = mydb.getConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Tạo cột ID và Tên cho DataTable
                    dataTable.Columns.Add("ID", typeof(string));
                    dataTable.Columns.Add("Name", typeof(string));

                    // Đọc dữ liệu từ kết quả truy vấn và thêm vào DataTable
                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();

                        // Thêm dữ liệu vào mỗi dòng của DataTable
                        dataTable.Rows.Add(id, name);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dataTable;
        }


        public bool UpdateStaff(string id, string name, string address, string phone, DateTime dob, string gender, MemoryStream picture)
        {
            SqlCommand command = new SqlCommand("UPDATE Staff " +
                                                "SET name = @name, address = @address, phone = @phone, dob = @dob, gender = @gender,picture = @pic " +
                                                "WHERE id = @id", mydb.getConnection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@dob", dob);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
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

        public Staff GetStaffById(string id)
        {
            try
            {
                Staff dentist = null;
                string query = "SELECT * FROM [Staff] WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, mydb.getConnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    mydb.openConnection();

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            dentist = new Staff();
                            dentist.id = dataReader["id"].ToString();
                            dentist.name = dataReader["name"].ToString();
                            dentist.address = dataReader["address"].ToString();
                            dentist.phone = dataReader["phone"].ToString();
                            dentist.Dob = dataReader.IsDBNull(dataReader.GetOrdinal("dob")) ? DateTime.MinValue : Convert.ToDateTime(dataReader["dob"]);
                            dentist.Gender = dataReader["gender"].ToString();
                            if (!dataReader.IsDBNull(dataReader.GetOrdinal("picture")))
                            {
                                // Đọc dữ liệu hình ảnh từ cột "picture" và tạo MemoryStream
                                byte[] pic = (byte[])dataReader["picture"];
                                MemoryStream picture = new MemoryStream(pic);

                                // Gán MemoryStream cho thuộc tính picture trong lớp Dentist
                                dentist.picture = picture;
                            }
                            else
                            {
                                // Nếu không có dữ liệu ảnh, gán thuộc tính picture là null
                                dentist.picture = null;
                            }
                        }
                    }
                }
                mydb.closeConnection();
                return dentist;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while querying information: " + ex.Message);
                return null;
            }
        }

        public bool DeleteStaff(string id)
        {
            SqlCommand command = new SqlCommand("Delete from Staff where id = @id", mydb.getConnection);
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
        public DataTable getStaffs(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;
        }

    }
}
