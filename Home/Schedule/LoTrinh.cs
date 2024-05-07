using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class LoTrinh : Form
    {
        string id;
        public LoTrinh(string id)
        {
            InitializeComponent();
            this.id = id;
        }
        MY_DB mydb = new MY_DB();
        private bool statusColumnAdded = false;

        private void LoadData()
        {
            // Tạo kết nối và truy vấn SQL
            try
            {
                // Phần code hiện tại của phương thức LoadData
                // ...
           
            string query = "SELECT task as Task, status FROM LoTrinh WHERE patientid = @id";
            SqlDataAdapter adapter = new SqlDataAdapter(query, mydb.getConnection);
            adapter.SelectCommand.Parameters.AddWithValue("@id", id);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Nếu cột "Status" chưa được thêm vào, thêm nó vào
            if (!statusColumnAdded)
            {
                // Tạo cột mới kiểu DataGridViewCheckBoxColumn
                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.HeaderText = "Status"; // Tiêu đề của cột
                checkboxColumn.Name = "Status"; // Tên cột
                checkboxColumn.DataPropertyName = "status"; // Tên cột dữ liệu từ DataTable
                checkboxColumn.ReadOnly = true; // Đặt cột là chỉ đọc (không cho phép chỉnh sửa)

                // Thêm cột mới vào DataGridView
                guna2DataGridView1.Columns.Add(checkboxColumn);

                statusColumnAdded = true; // Đánh dấu rằng cột đã được thêm vào
            }

            // Gán dữ liệu vào DataGridView
            guna2DataGridView1.DataSource = dataTable;
            guna2DataGridView1.Columns["status"].DisplayIndex = guna2DataGridView1.ColumnCount - 1;
            guna2DataGridView1.Columns["delete"].DisplayIndex = guna2DataGridView1.ColumnCount - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LoTrinh_Load(object sender, EventArgs e)
        {
            // Gọi hàm LoadData khi Form được load
            LoadData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 

            if (e.RowIndex != -1 && e.ColumnIndex == guna2DataGridView1.Columns["status"].Index)
            {
                DataGridViewCheckBoxCell cell = guna2DataGridView1.Rows[e.RowIndex].Cells["status"] as DataGridViewCheckBoxCell;
                if (cell != null && !Convert.ToBoolean(cell.Value)) // Ô checkbox không được check
                {
                    // Lấy giá trị của cột "task" tại hàng được nhấp
                    string task = guna2DataGridView1.Rows[e.RowIndex].Cells["task"].Value.ToString();

                    // Cập nhật status trong cơ sở dữ liệu
                    UpdateStatusInDatabase(task, "true");

                    // Cập nhật trạng thái checkbox
                    cell.Value = true;
                }
            }



            if (e.RowIndex != -1 && e.ColumnIndex == guna2DataGridView1.Columns["Delete"].Index)
            {
                // Xác nhận việc xóa hàng
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Lấy giá trị của cột "task" tại hàng được nhấp
                    string task = guna2DataGridView1.Rows[e.RowIndex].Cells["task"].Value.ToString();

                    // Thực hiện truy vấn SQL để xóa hàng từ cơ sở dữ liệu
                    string query = "DELETE FROM LoTrinh WHERE task = @task and patientid = @id";
                    SqlCommand command = new SqlCommand(query, mydb.getConnection);
                    mydb.openConnection();
                    command.Parameters.AddWithValue("@task", task);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    mydb.closeConnection();
                }

                // Xóa hàng trong DataGridView
                guna2DataGridView1.Rows.RemoveAt(e.RowIndex);
            }
             }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateStatusInDatabase(string task, string status)
        {
            try
            {
                // Thực hiện truy vấn SQL để cập nhật status trong cơ sở dữ liệu
                string query = "UPDATE LoTrinh SET status = @status WHERE task = @task";

                using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
                {
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@task", task);

                    mydb.openConnection();
                    command.ExecuteNonQuery();
                    mydb.closeConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}


    private void btnAdd_Click(object sender, EventArgs e)
        {
            try { 
            // Lấy nhiệm vụ từ điều khiển người dùng (ví dụ: TextBox)
            string task = txtBoxTask.Text;

            // Kiểm tra xem nhiệm vụ có được cung cấp không
            if (!string.IsNullOrEmpty(task))
            {
                // Thực hiện truy vấn SQL để chèn nhiệm vụ mới vào cơ sở dữ liệu và thiết lập status
                string query = "INSERT INTO LoTrinh (task, status, patientid) VALUES (@task, @status, @patientid)";

                    SqlCommand command = new SqlCommand(query, mydb.getConnection);
                    command.Parameters.AddWithValue("@task", task);
                    command.Parameters.AddWithValue("@status", "false");
                command.Parameters.AddWithValue("@patientid", id);
                    mydb.openConnection();
                    command.ExecuteNonQuery();
                    mydb.closeConnection();


                // Cập nhật giao diện người dùng để hiển thị nhiệm vụ mới
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập nhiệm vụ trước khi thêm!");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


