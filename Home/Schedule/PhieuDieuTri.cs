using DoAn01.Home.Manage.Service;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using DoAn01.Home.Schedule;

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
        double rating;
        double price;
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
            label16.Text = patientGender.ToString();
            label7.Text += " " + idSchedule;
            labelName.Text = patientName;
            labelAdd.Text = patientAddress;
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
            decimal totalAmount = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["Thành tiền"]);
            }
            price = Convert.ToDouble(totalAmount);
            // Định dạng số tiền với dấu chấm phân tách hàng nghìn
            string formattedAmount = totalAmount.ToString("#,##0");

            // Hiển thị số tiền đã định dạng trong Label
            label17.Text += " " + formattedAmount;
            using (SqlCommand cmd1 = new SqlCommand("SELECT rating FROM PhieuDIeuTri WHERE scheduleid = @id ", mydb.getConnection))
            {
                cmd1.Parameters.AddWithValue("@id", idSchedule); // Sử dụng cmd1 thay vì cmd
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd1))
                {
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);

                    if (dataTable2.Rows.Count > 0 && dataTable2.Rows[0]["rating"] != DBNull.Value)
                    {
                        double averageRating;
                        if (double.TryParse(dataTable2.Rows[0]["rating"].ToString(), out averageRating))
                        {
                            // Chỉ đặt giá trị cho ratingStar nếu giá trị trung bình rating hợp lệ (trong khoảng từ 0 đến 5)
                            if (averageRating >= 0 && averageRating <= 5)
                            {
                                uint ratingValue = (uint)Math.Round(averageRating, MidpointRounding.AwayFromZero);
                                ratingStar.Value = ratingValue;
                            }
                            else
                            {
                                ratingStar.Value = 0; // hoặc giá trị mặc định khác tùy thuộc vào yêu cầu của bạn
                            }
                        }
                        else
                        {
                            ratingStar.Value = 0;
                        }
                    }
                    else
                    {
                        ratingStar.Value = 0;
                    }
                }
            }

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
        Schedule schedule = new Schedule();
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string adv = txtAdvise.Text;
            string lotrinh = txtLLoTrinh.Text;
            rating = ratingStar.Value;
            // Kiểm tra xem scheduleid đã tồn tại trong cơ sở dữ liệu hay chưa
            bool isExisting = IsScheduleIdExisting(idSchedule);

            if (isExisting)
            {
                // Nếu scheduleid đã tồn tại, thực hiện cập nhật
                if (pDT.UpdateData(adv, lotrinh, idSchedule,rating,price))
                    MessageBox.Show("Cập nhật thành công");
                else
                    MessageBox.Show("Cập nhật thất bại");
            }
            else
            {
                // Nếu scheduleid chưa tồn tại, thực hiện chèn mới
                if (pDT.InsertData(adv, lotrinh, idSchedule,rating, price))
                {
                    schedule.updateStatusSchedule(idSchedule);
                    MessageBox.Show("Thêm mới thành công");
                }
                else
                    MessageBox.Show("Thêm mới thất bại");
            }
        }

        private bool IsScheduleIdExisting(string scheduleId)
        {
            // Tạo câu lệnh SQL để kiểm tra xem scheduleid có tồn tại trong cơ sở dữ liệu hay không
            string query = "SELECT COUNT(*) FROM PhieuDieuTri WHERE scheduleid = @id";

            using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
            {
                mydb.openConnection();
                command.Parameters.AddWithValue("@id", scheduleId);

                // Thực thi câu lệnh SQL và kiểm tra số lượng bản ghi trả về
                int count = (int)command.ExecuteScalar();
                mydb.closeConnection();

                // Nếu số lượng bản ghi là 0, tức là scheduleid không tồn tại
                // Ngược lại, tồn tại ít nhất một bản ghi với scheduleid này
                return count > 0;
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Sao chép nội dung từ TextBox vào Label
            label12.Text = txtAdvise.Text;

            // Ẩn TextBox và hiển thị Label
            txtAdvise.Visible = false;
            label12.Visible = true;
            label13.Text = txtLLoTrinh.Text;

            label15.Text = guna2DateTimePicker1.Value.ToString("dd/mm/yyyy");
            guna2DateTimePicker1.Visible = false;
            label15.Visible = true;
            // Ẩn TextBox và hiển thị Label
            txtLLoTrinh.Visible = false;
            label13.Visible = true;

            Print(this.panel1);
        }
        private void Print(Panel panel)
        {
            PrinterSettings ps = new PrinterSettings();
            panel1 = panel;
            getPrintArea(panel);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument2_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private Bitmap memorying;

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memorying, (pagearea.Width / 2) - (this.panel1.Width / 2), this.panel1.Location.Y);
        }
        private void getPrintArea(Panel panel)
        {
            memorying = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(memorying, new System.Drawing.Rectangle(0, 0, panel.Width, panel.Height));
        }


        private void pictureBoxPrint_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnPrint, "Print");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill(patientID,dentistID,idSchedule);
            bill.Show();
        }
    }
}

