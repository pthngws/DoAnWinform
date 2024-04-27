using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;

namespace DoAn01.Home.Report
{
    public partial class UC_Report : UserControl
    {
        public UC_Report()
        {
            InitializeComponent();

            List<string> yearDataSource = new List<string> { "2024", "2025" };
            guna2ComboBox2.DataSource = yearDataSource;

        }
        MY_DB mydb = new MY_DB();
        private void PanelService_Load(object sender, EventArgs e, string time)
        {
            // Kết nối đến cơ sở dữ liệu và thực hiện truy vấn
            string query = "SELECT TOP 3 Service.Name AS 'Tên dịch vụ', COUNT(LichSuDichVu.idService) AS 'Số lượng' " +
                           "FROM Schedule " +
                           "JOIN LichSuDichVu ON Schedule.Id = LichSuDichVu.idSchedule " +
                           "JOIN Service ON LichSuDichVu.idService = Service.Id " +
                           time +
                           " GROUP BY Service.Name " +
                           "ORDER BY COUNT(LichSuDichVu.idService) DESC";

            mydb.openConnection(); // Mở kết nối tại đây
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            // Lặp qua các hàng của DataTable
            if (dataTable.Rows.Count > 0)
            {
                // Lặp qua các hàng của DataTable
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    // Lấy giá trị của cột 'Tên dịch vụ'
                    string serviceName = dataTable.Rows[i]["Tên dịch vụ"].ToString();
                    // Lấy giá trị của cột 'Số lượng'
                    int serviceCount = Convert.ToInt32(dataTable.Rows[i]["Số lượng"]);

                    // Gán giá trị vào các Label tương ứng
                    if (i == 0)
                    {
                        NS1.Text = serviceName;
                        Q1.Text = serviceCount.ToString();
                    }
                    else if (i == 1)
                    {
                        NS2.Text = serviceName;
                        Q2.Text = serviceCount.ToString();
                    }
                    else if (i == 2)
                    {
                        NS3.Text = serviceName;
                        Q3.Text = serviceCount.ToString();
                    }
                }
            }

            else
            {
                NS1.Text = "";
                Q1.Text = "";
                NS2.Text = "";
                Q3.Text = "";
                NS3.Text = "";
                Q2.Text = "";
            }

            mydb.closeConnection(); // Đóng kết nối tại đây
        }
        private void LoadChart(string query)
        {
            // Xóa dữ liệu hiện có trên biểu đồ trước khi tải dữ liệu mới
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Tạo mới một Series và ChartArea cho biểu đồ
            Series series = new Series("Total Revenue");
            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.LabelStyle.Enabled = true; // Bật hiển thị nhãn trục X
            chartArea.AxisX.LabelStyle.Interval = 1; // Đặt khoảng cách giữa các nhãn
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true; // Cho phép hiển thị nhãn cuối cùng
            // Đặt kiểu đồ thị của Series thành Line
            series.ChartType = SeriesChartType.Column;

            // Thêm series và chartArea vào biểu đồ
            chart1.Series.Add(series);
            chart1.ChartAreas.Add(chartArea);
            // Đặt độ dày của đường line
            series.BorderWidth = 10; // 3 là một giá trị ví dụ, bạn có thể thay đổi nó tùy ý
            series.Color = System.Drawing.Color.FromArgb(53, 26, 150);

            // Tạo SqlDataAdapter và DataTable để lưu trữ kết quả truy vấn
            SqlDataAdapter adapter;
            DataTable dataTable = new DataTable();

            // Trường hợp QUARTER
            if (query.StartsWith("WHERE MONTH(Schedule.NgayKham) B"))
            {
                // Lấy phần cuối của query, đó là số tháng cuối của quý
                int endMonth = int.Parse(query.Substring(query.Length - 1));

                // Tính toán tháng bắt đầu và kết thúc dựa trên tháng cuối của quý
                int startMonth = endMonth - 2;
                if(startMonth<1)
                {
                    startMonth += 10;
                }    
                for (int month = startMonth; month <= startMonth + 2; month++)
                {
                    adapter = new SqlDataAdapter("SELECT SUM(price) AS totalrev FROM PhieuDIeuTri PDT JOIN Schedule ON PDT.scheduleid = Schedule.Id WHERE MONTH(Schedule.NgayKham) = " + month, mydb.getConnection);
                    mydb.openConnection();
                    dataTable.Clear();
                    adapter.Fill(dataTable);

                    // Thêm dữ liệu vào series
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Kiểm tra xem cột có giá trị DBNull không
                        if (row["totalrev"] != DBNull.Value)
                        {
                            // Nếu không phải DBNull, chuyển đổi giá trị thành double và thêm vào biểu đồ
                            double totalRevenue = Convert.ToDouble(row["totalrev"]);
                            series.Points.AddXY(month, totalRevenue);
                        }
                        else
                        {
                            // Nếu là DBNull, xử lý tùy thuộc vào yêu cầu của bạn
                            // Ví dụ: bỏ qua giá trị này, gán giá trị mặc định, hoặc thực hiện hành động khác
                        }
                    }
                    mydb.closeConnection();
                }
            }
            // Trường hợp MONTH
            // Trường hợp MONTH
            // Trường hợp MONTH
            else if (query.StartsWith("WHERE MONTH(Schedule.NgayKham)"))
            {
                adapter = new SqlDataAdapter("SELECT Day(Schedule.NgayKham) AS Day, SUM(price) AS totalrev FROM PhieuDIeuTri PDT JOIN Schedule ON PDT.scheduleid = Schedule.Id " + query + " GROUP BY Day(Schedule.NgayKham)", mydb.getConnection);
                mydb.openConnection();
                adapter.Fill(dataTable);
                mydb.closeConnection();

                // Thêm dữ liệu vào series
                foreach (DataRow row in dataTable.Rows)
                {
                    // Kiểm tra xem cột có giá trị DBNull không
                    if (row["totalrev"] != DBNull.Value)
                    {
                        // Nếu không phải DBNull, chuyển đổi giá trị thành double và thêm vào biểu đồ
                        double totalRevenue = Convert.ToDouble(row["totalrev"]);
                        int day = Convert.ToInt32(row["Day"]);
                        series.Points.AddXY(day, totalRevenue);
                    }
                    else
                    {
                        // Nếu là DBNull, xử lý tùy thuộc vào yêu cầu của bạn
                        // Ví dụ: bỏ qua giá trị này, gán giá trị mặc định, hoặc thực hiện hành động khác
                    }
                }

                // Đặt dạng hiển thị cho nhãn của trục X là ngày
              
            }


            // Trường hợp YEAR
            else if (query.StartsWith("WHERE YEAR(Schedule.NgayKham)"))
            {
                adapter = new SqlDataAdapter("SELECT MONTH(Schedule.NgayKham) AS Month, SUM(price) AS totalrev FROM PhieuDIeuTri PDT JOIN Schedule ON PDT.scheduleid = Schedule.Id " + query + " GROUP BY MONTH(Schedule.NgayKham)", mydb.getConnection);
                mydb.openConnection();
                adapter.Fill(dataTable);
                mydb.closeConnection();

                // Thêm dữ liệu vào series
                foreach (DataRow row in dataTable.Rows)
                {
                    // Kiểm tra xem cột có giá trị DBNull không
                    if (row["totalrev"] != DBNull.Value)
                    {
                        // Nếu không phải DBNull, chuyển đổi giá trị thành double và thêm vào biểu đồ
                        double totalRevenue = Convert.ToDouble(row["totalrev"]);
                        int month = Convert.ToInt32(row["Month"]);
                        series.Points.AddXY(month, totalRevenue);
                    }
                    else
                    {
                        // Nếu là DBNull, xử lý tùy thuộc vào yêu cầu của bạn
                        // Ví dụ: bỏ qua giá trị này, gán giá trị mặc định, hoặc thực hiện hành động khác
                    }
                }
            }
        }



        private void PanelDentist_Load(object sender, EventArgs e, string time)
        {
            // Kết nối đến cơ sở dữ liệu và thực hiện truy vấn
            string query = "SELECT TOP 3 Dentist.Name, AVG(PhieuDieuTri.rating) AS Rating\r\nFROM Dentist\r\nJOIN Schedule ON Schedule.DentistId = Dentist.Id\r\nJOIN PhieuDieuTri ON PhieuDieuTri.scheduleid = Schedule.Id " + time + " GROUP BY Dentist.Name\r\nORDER BY rating DESC";

            mydb.openConnection(); // Mở kết nối tại đây
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            // Lặp qua các hàng của DataTable
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string serviceName = dataTable.Rows[i]["Name"].ToString();
                    int serviceCount = Convert.ToInt32(dataTable.Rows[i]["Rating"]);

                    // Gán giá trị vào các Label tương ứng
                    if (i == 0)
                    {
                        D1.Text = serviceName;
                        QD1.Text = serviceCount.ToString();
                    }
                    else if (i == 1)
                    {
                        D2.Text = serviceName;
                        QD2.Text = serviceCount.ToString();
                    }
                    else if (i == 2)
                    {
                        D3.Text = serviceName;
                        QD3.Text = serviceCount.ToString();
                    }
                }
            }
            else
            {
                D1.Text = "";
                QD1.Text = "";
                D3.Text = "";
                QD3.Text = "";
                D2.Text = "";
                QD2.Text = "";
            }
            mydb.closeConnection();

        }
            private void UC_Report_Load(object sender, EventArgs e, string time)
        {
            mydb.openConnection(); // Mở kết nối tại đây
            SqlCommand cmd = new SqlCommand("SELECT COALESCE(SUM(price), 0) AS totalrev,\r\n       COALESCE(COUNT(PDT.scheduleid), 0) AS totalcase,\r\n       COALESCE(COUNT(DISTINCT Schedule.Patientid), 0) AS totalpatient\r\nFROM PhieuDIeuTri PDT\r\nJOIN Schedule ON PDT.scheduleid = Schedule.Id " + time, mydb.getConnection);

            // Thực thi truy vấn và lấy giá trị trả về
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Lấy giá trị từ các cột và gán vào các biến
                totalrev.Text = Convert.ToDouble(reader["totalrev"]).ToString("#,##0");

                totalcase.Text = reader["totalcase"].ToString();
                totalpatient.Text = reader["totalpatient"].ToString();
            }

            // Đóng kết nối
            mydb.closeConnection();
        }
        private string Query(string time)
        {
            if (time == "1st")
                return "WHERE MONTH(Schedule.NgayKham) BETWEEN 1 AND 3";
            else if (time == "2nd")
                return "WHERE MONTH(Schedule.NgayKham) BETWEEN 4 AND 6";
            else if (time == "3rd")
                return "WHERE MONTH(Schedule.NgayKham) BETWEEN 7 AND 9";
            else if (time == "4th")
                return "WHERE MONTH(Schedule.NgayKham) BETWEEN 10 AND 12";
            else if (time == "January")
                return "WHERE MONTH(Schedule.NgayKham) = 1";
            else if (time == "February")
                return "WHERE MONTH(Schedule.NgayKham) = 2";
            else if (time == "March")
                return "WHERE MONTH(Schedule.NgayKham) = 3";
            else if (time == "April")
                return "WHERE MONTH(Schedule.NgayKham) = 4";
            else if (time == "May")
                return "WHERE MONTH(Schedule.NgayKham) = 5";
            else if (time == "June")
                return "WHERE MONTH(Schedule.NgayKham) = 6";
            else if (time == "July")
                return "WHERE MONTH(Schedule.NgayKham) = 7";
            else if (time == "August")
                return "WHERE MONTH(Schedule.NgayKham) = 8";
            else if (time == "September")
                return "WHERE MONTH(Schedule.NgayKham) = 9";
            else if (time == "October")
                return "WHERE MONTH(Schedule.NgayKham) = 10";
            else if (time == "November")
                return "WHERE MONTH(Schedule.NgayKham) = 11";
            else if (time == "December")
                return "WHERE MONTH(Schedule.NgayKham) = 12";
            else if (time == "2024")
                return "WHERE YEAR(Schedule.NgayKham) = 2024";
            else if (time == "2025")
                return "WHERE YEAR(Schedule.NgayKham) = 2025";
            return ""; // hoặc xử lý trường hợp không hợp lệ khác tùy theo yêu cầu
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> quarterDataSource = new List<string> { "1st", "2nd", "3rd", "4th" };
            List<string> yearDataSource = new List<string> { "2024", "2025" };

            // Khởi tạo nguồn dữ liệu cho MONTH
            List<string> monthDataSource = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            // Lấy giá trị của mục được chọn trong guna2ComboBox1
            string selectedValue = guna2ComboBox1.Text;

            if (selectedValue == "QUARTER")
            {
                guna2ComboBox2.DataSource = quarterDataSource;
            }
            else if (selectedValue == "MONTH")
            {
                guna2ComboBox2.DataSource = monthDataSource;
            }
            else if (selectedValue == "YEAR")
            {
                guna2ComboBox2.DataSource = yearDataSource;
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = guna2ComboBox2.Text;
            string query = Query(time);
            PanelService_Load(sender, e, query); // Sử dụng biến query thay cho time
            PanelDentist_Load(sender, e, query);
            UC_Report_Load(sender, e, query);
            LoadChart(query);
        }
    }
}
