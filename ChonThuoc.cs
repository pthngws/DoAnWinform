using DoAn01.Home.Manage.Medicine;
using DoAn01.Home.Manage.Service;
using DoAn01.Home.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class ChonThuoc : Form
    {
        public ChonThuoc()
        {
            InitializeComponent();
        }
        string idschedule;
        public ChonThuoc(string id)
        {
            InitializeComponent();
            this.idschedule = id;
        }
        Medicine medicine = new Medicine();
        private void ChonThuoc_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dentalSmileDataSet10.Medicine' table. You can move, or remove it, as needed.
            this.medicineTableAdapter.Fill(this.dentalSmileDataSet10.Medicine);
            SqlCommand cmd = new SqlCommand("select id,name,price from medicine");
            // Thiết lập DataSource cho guna2DataGridView1
            guna2DataGridView1.DataSource = medicine.getMedicines(cmd);

            // Thêm các cột vào guna2DataGridView2 nếu chưa có
            if (guna2DataGridView2.Columns.Count == 0)
            {
                foreach (DataGridViewColumn column in guna2DataGridView1.Columns)
                {
                    guna2DataGridView2.Columns.Add((DataGridViewColumn)column.Clone());
                }
            }
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView2.AllowUserToAddRows = false;

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                }
                guna2DataGridView2.Rows.Add(newRow);
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                // Xác nhận với người dùng trước khi xóa

                // Lấy hàng được chọn
                DataGridViewRow selectedRow = guna2DataGridView2.Rows[e.RowIndex];

                // Xóa hàng từ guna2DataGridView2
                guna2DataGridView2.Rows.Remove(selectedRow);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {

            MY_DB mydb = new MY_DB();
            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                string medicineID = row.Cells[0].Value.ToString();
                // ID của dịch vụ
                // Thêm các giá trị vào bảng LichSuDichVu
                string insertQuery = "INSERT INTO LichSuThuoc (idschedule,idmedicine) VALUES (@idschedule,@medicineid)";
                SqlCommand command = new SqlCommand(insertQuery, mydb.getConnection);
                mydb.openConnection();
                command.Parameters.AddWithValue("@medicineid", medicineID);
                command.Parameters.AddWithValue("@idschedule", idschedule);
                command.ExecuteNonQuery();
                mydb.closeConnection();
            }

            MessageBox.Show("Thêm vào thành công!");



        }
    }
}
