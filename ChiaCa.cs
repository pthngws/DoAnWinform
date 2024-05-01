using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01
{

    public partial class ChiaCa : Form
    {

        static string ConvertValue(int value)
        {
            switch (value)
            {
                case 1:
                    return "Sáng"; // Giá trị 1 chuyển thành 7-12
                case 2:
                    return "Chiều"; // Giá trị 2 chuyển thành 12-17
                case 3:
                    return "Tối"; // Giá trị 3 chuyển thành 17-22
                case 4:
                    return "Khuya";
            case 5:
                    return "Rất Khuya";

                    case 6:
                    return "Rất rất khuya";
                default:
                    return "X"; // Trả về null cho các giá trị khác
            }
        }


        static DataTable ConvertArrayToDataTable(int[,] array)
        {
            DataTable dt = new DataTable();

            // Thêm cột ID nhân viên vào DataTable
            dt.Columns.Add("ID nhân viên", typeof(string));

            // Thêm cột Tên nhân viên vào DataTable sau cột ID nhân viên
            dt.Columns.Add("Tên nhân viên", typeof(string)).SetOrdinal(1);

            // Thêm các cột vào DataTable và đặt tên cho chúng
            dt.Columns.Add("Thứ 2", typeof(string));
            dt.Columns.Add("Thứ 3", typeof(string));
            dt.Columns.Add("Thứ 4", typeof(string));
            dt.Columns.Add("Thứ 5", typeof(string));
            dt.Columns.Add("Thứ 6", typeof(string));
            dt.Columns.Add("Thứ 7", typeof(string));
            dt.Columns.Add("Chủ nhật", typeof(string));

            // Thêm cột số ca vào DataTable
            dt.Columns.Add("Tổng số ca trong tuần", typeof(int));
            Staff staff = new Staff();
            DataTable list = staff.GetStaffData();
            // Thêm dữ liệu từ mảng vào DataTable
            for (int i = 0; i < array.GetLength(0); i++)
            {
                DataRow row = dt.NewRow();
                row["ID nhân viên"] = list.Rows[i]["ID"]; // Thiết lập giá trị cho cột ID
                row["Tên nhân viên"] = list.Rows[i]["Name"]; // Thiết lập giá trị cho cột Tên

                int count = 0; // Biến đếm số lượng giá trị khác "FREE" trong hàng
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row[j + 2] = ConvertValue(array[i, j]); // Ánh xạ giá trị từ mảng a sang giá trị mới, bắt đầu từ cột thứ 3

                    // Đếm số lượng giá trị khác "FREE"
                    if (ConvertValue(array[i, j]) != "X")
                    {
                        count++;
                    }
                }
                // Đặt giá trị cho cột số ca
                row["Tổng số ca trong tuần"] = count;
                dt.Rows.Add(row);
            }



            return dt;
        }



        static List<int> KhoangCach(int start, int kc, int n)
        {
            List<int> list = new List<int>();

            // Thêm phần tử đầu tiên vào danh sách
            list.Add(start);

            // Thêm các phần tử tiếp theo hoặc cập nhật các phần tử đã có
            for (int i = 1; i < kc; i++)
            {
                // Tính toán nextElement sử dụng phép toán module để quay lại phạm vi từ 1 đến n
                int nextElement = (start + i) % n;
                if (nextElement == n) // Nếu nextElement bằng 0, gán nextElement = n
                {
                    nextElement = 0;
                }
                list.Add(nextElement);
            }

            return list;
        }

        static int TangLenBenTrai(int[,] a, int i, int thu,int soca)
        {
            int socantim = 2; // Khởi tạo socantim trước khi sử dụng

            // Tìm giá trị của socantim
            for (int x = thu - 1; x >= 0; x--)
            {
                if (a[i, x] != 0)
                {
                    socantim = a[i, x];
                    break; // Thoát khỏi vòng lặp sau khi tìm thấy giá trị khác không đầu tiên
                }
            }
            // Kiểm tra nếu socantim lớn hơn hoặc bằng 3, trả về 1, ngược lại tăng socantim lên 1 và trả về giá trị mới
            return socantim % soca + 1;
        }

        static int TangLenBenTren(int[,] a, int i, int thu,int soca)
        {
            int socantim = 0; // Khởi tạo socantim trước khi sử dụng

            // Tìm giá trị của socantim
            for (int x = i - 1; x >= 0; x--)
            {
                if (a[x, thu] != 0)
                {
                    socantim = a[x, thu];
                    break; // Thoát khỏi vòng lặp sau khi tìm thấy giá trị khác không đầu tiên
                }
            }

            // Kiểm tra nếu socantim lớn hơn hoặc bằng 3, trả về 1, ngược lại tăng socantim lên 1 và trả về giá trị mới
            return socantim % soca + 1;
        }
        static void taolichchonhanvien(int n, DataGridView dataGridView,int sonhanvien1ca, int soca)
        {
            int tongsoca = sonhanvien1ca * soca;
            int kc = n - tongsoca;
            int start = tongsoca-soca;
            int[,] a = new int[n, 7]; // Đảo ngược kích thước thành [n, 7] để có 7 cột và n hàng
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    a[i, j] = 0;
                }
            }
            for (int i = 0; i < start; i++)
            {
                a[i, 0] = i % soca + 1;
            }
            for (int i = 0; i < soca; i++)
            {
                a[n - 1 - i, 0] = i + 1;
            }

            List<int> khoangcach = new List<int>();
            for (int thu = 1; thu < 7; thu++)
            {
                bool flag = false;
                if (kc <= 0)
                {
                    khoangcach.Add(100);
                }
                else if (kc != 0)
                {
                    start = (start + kc) % n;
                    khoangcach = KhoangCach(start, kc, n);
                }
                
                for (int i = 0; i < n; i++)
                {
                    if (!khoangcach.Contains(i))
                    {
                        if (flag == false)
                        {
                            flag = true;
                            a[i, thu] = TangLenBenTrai(a, i, thu,soca);
                        }
                        else if (flag == true)
                        {
                            a[i, thu] = TangLenBenTren(a, i, thu,soca);
                        }
                    }
                }
            }
            // Các lệnh khởi tạo mảng a ở đây

            // Tạo DataTable từ mảng a
            DataTable dt = ConvertArrayToDataTable(a);
            // Convert array 'a' to DataTable
            if (Global.GlobalRole == "staff")
            {
                // Filter the DataTable to include only rows where the "ID nhân viên" column is equal to "ID"
                DataTable filteredDt = dt.AsEnumerable()
                
                    .Where(row => row.Field<string>("ID nhân viên") == Global.GlobalID.ToUpper())
                    .CopyToDataTable();

                // Bind the filtered DataTable to the DataGridView
                dataGridView.DataSource = filteredDt;
            }
            else
            { dataGridView.DataSource = dt; }

            // Liên kết DataTable với DataGridView
        }
        Staff staff = new Staff();
        public ChiaCa()
        {
            InitializeComponent();

            if (Global.GlobalRole == "staff")
            {

                // Hide additional components if necessary
                label1.Visible = false;
                label2.Visible = false;
                sonhanvien1ca.Visible = false;
                soca.Visible = false;
                btnView.Visible = false;

            }
            btnView_Click(null, null);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            int sonv1ca = (int)sonhanvien1ca.Value;
            int socalam = (int)soca.Value;
            DataTable dt = staff.GetStaffData();

            int n = dt.Rows.Count;
            if (sonv1ca * socalam < n)
            {
                taolichchonhanvien(n, guna2DataGridView1, sonv1ca, socalam);
            }
            else
            {
                MessageBox.Show("Số lượng nhân viên không đủ để phân ca.");
            }
        }

    }
}
