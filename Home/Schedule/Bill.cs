using DoAn01.Home.Schedule;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class Bill : Form
    {
        string patientID;
        string patientName;
        string patientAddress;
        string patientGender;
        string patientPhone;
        string dentistID;
        string idSchedule;
        double rating;
        MY_DB connection = new MY_DB();
        public Bill(string patientID, string dentistID, string idSchedule)
        {
            InitializeComponent();

            this.patientID = patientID;
            this.dentistID = dentistID;
            this.idSchedule = idSchedule;

            // Thực hiện truy vấn để lấy thông tin của bệnh nhân dựa trên patientID
            string query = "SELECT name, address, phone FROM patient WHERE id = @PatientID";
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
        public Bill()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            labelName.Text = " " + patientName;
            labelAddress.Text = " " + patientAddress;
            labelPhone.Text = " " + patientPhone;
            labelID.Text = " " + idSchedule;

            SqlCommand cmd = new SqlCommand("select S.Name as 'Tên dịch vụ', count(L.idService) as 'Số lượng',sum(S.price) as 'Thành tiền' from Service as S, LichSuDichVu as L where S.Id = L.idService and L.idSchedule =@idSchedule group by S.name", mydb.getConnection);
            cmd.Parameters.AddWithValue("idSchedule", idSchedule);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            // Tạo một DataTable để lưu trữ kết quả
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Thiết lập DataSource cho DataGridView
            guna2DataGridView1.DataSource = dataTable;
            decimal totalAmount = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["Thành tiền"]);
            }

            // Định dạng số tiền với dấu chấm phân tách hàng nghìn
            string formattedAmount = totalAmount.ToString("#,##0");

            // Hiển thị số tiền đã định dạng trong Label
            label2.Text = ": " + formattedAmount;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

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
            toolTip1.SetToolTip(guna2ImageButton1, "Print");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill(patientID, dentistID, idSchedule);
            bill.Show();
        }
    }
}

