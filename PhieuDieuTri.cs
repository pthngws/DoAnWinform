﻿using DoAn01.Home.Manage.Service;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class PhieuDieuTri : Form
    {
        public PhieuDieuTri()
        {
            InitializeComponent();
        }
        string patientID;
        string patientName;
        string patientAddress;
        string patientGender;
        string patientPhone;
        string dentistID;
        string idSchedule;
        MY_DB connection = new MY_DB();
        public PhieuDieuTri(string patientID, string dentistID,string idSchedule)
        {
            InitializeComponent();

            this.patientID = patientID;
            this.dentistID = dentistID;
            this.idSchedule = idSchedule;

            // Thực hiện truy vấn để lấy thông tin của bệnh nhân dựa trên patientID
            string query = "SELECT name, address, gender, phone FROM patient WHERE id = @PatientID";
            try
            {

                    connection.openConnection();
                    SqlCommand command = new SqlCommand(query, connection.getConnection);
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        patientName = reader["name"].ToString();
                        patientAddress = reader["address"].ToString();
                        patientGender = reader["gender"].ToString();
                        patientPhone = reader["phone"].ToString();
                    }
                    reader.Close();
                
                }
            
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

            // In thông tin của bệnh nhân để kiểm tra
            
        }


        Service Service = new Service();
        MY_DB mydb = new MY_DB();

        // Dictionary để lưu trữ trạng thái của các CheckBox controls
        Dictionary<string, bool> checkboxStates = new Dictionary<string, bool>();

        private void PhieuDieuTri_Load(object sender, EventArgs e)
        {
            if (patientGender == "Male")
                guna2RadioButton1.Checked = true;
            else
                guna2RadioButton1.Checked = false;
            label7.Text += " " + idSchedule;
            txtName.Text = patientName;
            txtAdd.Text = patientAddress;
            MessageBox.Show(idSchedule);
            SqlCommand cmd = new SqlCommand("select S.Name as 'Tên dịch vụ', count(L.idService) as 'Số lượng',sum(S.price) as 'Thành tiền' from Service as S, LichSuDichVu as L where S.Id = L.idService and L.idSchedule =@idSchedule group by S.name", mydb.getConnection);
            cmd.Parameters.AddWithValue("idSchedule", idSchedule);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            // Tạo một DataTable để lưu trữ kết quả
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Thiết lập DataSource cho DataGridView
            guna2DataGridView1.DataSource = dataTable;
            SqlCommand cmd2 = new SqlCommand("select advice,lotrinh from PhieuDieuTri where scheduleid=@idSchedule", mydb.getConnection);
            cmd2.Parameters.AddWithValue("idSchedule", idSchedule);
            SqlDataAdapter  adapter1 = new SqlDataAdapter(cmd2);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            if(dataTable1.Rows.Count > 0)
{
                // Lấy dữ liệu từ hàng đầu tiên của DataTable
                string advice = dataTable1.Rows[0]["advice"].ToString();
                string lotrinh = dataTable1.Rows[0]["lotrinh"].ToString();

                // Gán dữ liệu vào các TextBox
                txtAdvise.Text = advice;
                txtLLoTrinh.Text = lotrinh;
            }
else
            {
                // Xử lý trường hợp không tìm thấy bản ghi
                // Ví dụ: Hiển thị một thông báo lỗi
                MessageBox.Show("Không tìm thấy thông tin cho scheduleid này.");
            }

            /* try
             {
                 // Mở kết nối
                 mydb.openConnection();

                 // Truy vấn để lấy dữ liệu từ bảng service
                 *//*string query = "SELECT * FROM service";
                 SqlCommand command = new SqlCommand(query, mydb.getConnection);
                 SqlDataReader reader = command.ExecuteReader();

                 // Kiểm tra nếu có dữ liệu
                 if (reader.HasRows)
                 {
                     // Đọc từng dòng dữ liệu
                     while (reader.Read())
                     {
                         // Lấy thông tin dịch vụ từ cột trong bảng service
                         string serviceName = reader["name"].ToString();

                         // Tạo một CheckBox control
                         CheckBox checkBox = new CheckBox();
                         checkBox.Text = serviceName;

                         // Gán một giá trị duy nhất cho Name hoặc Tag của CheckBox control
                         checkBox.Name = "checkbox_" + serviceName.Replace(" ", "_");

                         // Thêm giá trị mặc định của CheckBox control vào Dictionary
                         checkboxStates.Add(checkBox.Name, checkBox.Checked);

                         // Gắn sự kiện CheckedChanged cho CheckBox control
                         checkBox.CheckedChanged += CheckBox_CheckedChanged;

                         // Thêm CheckBox control vào FlowLayoutPanel
                         flowLayoutPanel1.Controls.Add(checkBox);
                     }
                 }*//*

                 // Đóng kết nối
                 reader.Close();
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Lỗi: " + ex.Message);
             }
             finally
             {
                 // Đảm bảo kết nối được đóng sau khi sử dụng
                 mydb.closeConnection();
             }*/
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Lấy tên của CheckBox control được thay đổi trạng thái
            string checkboxName = ((CheckBox)sender).Name;

            // Lấy trạng thái mới của CheckBox control
            bool newState = ((CheckBox)sender).Checked;

            // Cập nhật trạng thái mới vào Dictionary
            checkboxStates[checkboxName] = newState;

            // Đây là nơi để xử lý khi trạng thái của CheckBox control thay đổi
            // Ví dụ: bạn có thể hiển thị thông báo hoặc thực hiện tính toán khác tùy thuộc vào nhu cầu của bạn
        }
        PDT pDT = new PDT();

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string adv = txtAdvise.Text;
            string lotrinh = txtLLoTrinh.Text;
            if (pDT.InsertData(adv, lotrinh, idSchedule))
                MessageBox.Show("Thành công");



        }
    }
}
