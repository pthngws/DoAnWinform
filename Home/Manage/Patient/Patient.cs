using DoAn01;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace DoAn01 // Quản Lý Bệnh Nhân
{

    class Patient
    {
        MY_DB mydb = new MY_DB();
        private DataTable table = new DataTable();

        public string id;
        public string name;
        public string address;
        public string phone;
        public DateTime Dob;
        public string Gender;

        public Patient checkPatient(string id)
        {
            Patient patient = new Patient();
            SqlCommand cmd = new SqlCommand("select * from patient where id = @id",mydb.getConnection);
            mydb.openConnection();
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if(sqlDataReader.Read())
            {
                patient.id = sqlDataReader["id"].ToString();
                patient.name = sqlDataReader["name"].ToString();
                patient.address = sqlDataReader["address"].ToString();
                patient.phone =sqlDataReader["phone"].ToString();
                patient.Gender = sqlDataReader["gender"].ToString();
                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("dob")))
                {
                    patient.Dob = Convert.ToDateTime(sqlDataReader["dob"]);
                }
                else
                {
                    patient.Dob = DateTime.Now;
                }

                return patient;
            }
            return null;
        }
        public bool insertPatient(string id, string name, string address, string phone, DateTime dob, string gender)
        {

            SqlCommand command = new SqlCommand("INSERT INTO patient (id,name, address, phone, dob, gender) " +
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
            return false;
        }


        public bool UpdatePatient(string id, string name, string address, string phone, DateTime dob, string gender)
        {
           

            SqlCommand command = new SqlCommand("UPDATE patient " +
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
        private void XoaAnh(string id)
        {
            string folderPath = @"C:\Users\thang\source\repos\DoAn01\bin\Debug\Image";

            try
            {
                // Kiểm tra xem thư mục tồn tại không
                if (!Directory.Exists(folderPath))
                {
                    return;
                }

                // Lấy danh sách các tập tin trong thư mục
                string[] fileNames = Directory.GetFiles(folderPath);

                // Duyệt qua từng tập tin
                foreach (string fileName in fileNames)
                {
                    // Kiểm tra xem tên tập tin có chứa chuỗi "BN01" không
                    if (Path.GetFileName(fileName).Contains(id+"_"))
                    {
                        // Xóa tập tin
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the file: " + ex.Message);
            }
        }

/*        public bool DeletePatient(string id)
        {
            SqlCommand command = new SqlCommand("Delete from patient where id = @id", mydb.getConnection);
            command.Parameters.AddWithValue("@id", id);
            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                XoaAnh(id);
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
            return false;
        }*/
        public DataTable getPatients(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;
        }

      


        /*        public DataTable GetTreatmentHistory(int patientID)
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM treatment_history WHERE patient_id = @patientID", mydb.getConnection);
                    command.Parameters.AddWithValue("@patientID", patientID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable treatmentHistory = new DataTable();
                    adapter.Fill(treatmentHistory);
                    return treatmentHistory;
                }*/
    }
}