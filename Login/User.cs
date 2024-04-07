using DoAn01;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace DoAn01

{
    public class User
    {
        private string username;
        private string password;
        private string email;
        private string role;
        MY_DB db = new MY_DB();
        public User() { }
        //Khởi tạo
        public User(string username)
        {
           this.username = username;
        }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }
        public User(string username, string password, string email, string role)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Role = role;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        //Thêm người dùng
        public void CreateUser()
        {
            using (SqlConnection connection = db.getConnection)
            {
                string checkUsernameQuery = "SELECT COUNT(*) FROM [user] WHERE username = @username";


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
                string checkEmailQuery = "SELECT COUNT(*) FROM [user] WHERE email = @email";
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
                string insertQuery = "INSERT INTO [user] (username, password, email,role) VALUES (@username, @password, @email,@role)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@role", role);

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
        public User LoginFaceID()
        {
            try
            {
                User user = new User();
                db.openConnection();

                string query = "SELECT * FROM [user] WHERE Username = @Username ";
                SqlCommand cmd = new SqlCommand(query, db.getConnection);

                // Thêm tham số và giá trị cho tham số
                cmd.Parameters.AddWithValue("@Username", username);

                SqlDataReader dataReader = cmd.ExecuteReader();

                // Kiểm tra xem có dữ liệu trong dataReader hay không
                if (dataReader.Read())
                {

                    user.Username = dataReader["username"].ToString();
                    user.Password = dataReader["password"].ToString();
                    user.Email = dataReader["email"].ToString();
                    user.role = dataReader["role"].ToString();
                    db.closeConnection();
                    return user; // Người dùng tồn tại và mật khẩu khớp
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        public User LoginUser()
        {
            try
            {
                User user = new User();
                db.openConnection();

                string query = "SELECT * FROM [user] WHERE Username = @Username AND Password = @Password And Role = @Role";
                SqlCommand cmd = new SqlCommand(query, db.getConnection);

                // Thêm tham số và giá trị cho tham số
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", Role);

                SqlDataReader dataReader = cmd.ExecuteReader();

                // Kiểm tra xem có dữ liệu trong dataReader hay không
                if (dataReader.Read())
                {

                    user.Username = dataReader["username"].ToString();
                    user.Password = dataReader["password"].ToString();
                    user.Email = dataReader["email"].ToString();
                    user.role = dataReader["role"].ToString();
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

        //Lấy lại mật khẩu
        public bool GetPasswordUser()
        {
            try
            {

                db.openConnection();

                string query = "SELECT * FROM [user] WHERE username = @username AND email = @email";

                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        // Sử dụng HasRows để kiểm tra xem có dữ liệu hay không
                        if (dataReader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                db.closeConnection();
            }
        }
        //Cập nhật mật khẩu
        public bool UpdatePassword(string newPassword)
        {
            try
            {
                db.openConnection();
                string updateQuery = "UPDATE [user] SET password = @newPassword WHERE username = @username";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, db.getConnection))
                {
                    updateCommand.Parameters.AddWithValue("@newPassword", newPassword);
                    updateCommand.Parameters.AddWithValue("@username", username);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                db.closeConnection();
            }
        }

        //Kiểm tra dữ liệu người dùng có tồn tại chưa

        //Kiểm tra mật khẩu có đúng không?
        public bool CheckPassword(string inputPassword)
        {
            try
            {
                db.openConnection();

                string query = "SELECT COUNT(*) FROM [user] WHERE username = @username AND password = @inputPassword";

                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@inputPassword", inputPassword);

                    int count = (int)cmd.ExecuteScalar();

                    // Kiểm tra xem có ít nhất một bản ghi khớp hay không
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                db.closeConnection();
            }
        }


    }
}
