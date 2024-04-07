using DoAn01.Home.Manage.Dentist;
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
using static System.Net.Mime.MediaTypeNames;

namespace DoAn01.Home.Schedule
{
    public partial class UC_Schedule : UserControl
    {
        MY_DB mydb = new MY_DB();
        List<string> listIDBacSiCoLich = new List<string>();
        List<Dentist> listBacSi = new List<Dentist>();
        public UC_Schedule()
        {
            InitializeComponent();
        }

        private void UC_Schedule_Load(object sender, EventArgs e)
        {

        }
        private void addUserControl(UserControl userControl, Panel panel)
        {
            userControl.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(userControl);
            userControl.BringToFront();
        }
        public bool checkStatus(string id)
        {
            foreach(string x in listIDBacSiCoLich) {
                if (id == x)
                    return true;
                
            }
            return false;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

/*            DateTime date = dateTimePickerSearch.Value;*/

            

            
            }


        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            string ca = "";
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    ca = comboBox1.SelectedItem.ToString();
                    panel1.Controls.Clear();
                    listIDBacSiCoLich.Clear();
                    listBacSi.Clear();

                    DateTime date = dateTimePicker1.Value;
                    string formattedDate = date.ToString("yyyy/MM/dd");
                    SqlCommand command = new SqlCommand("select Schedule.dentistId from schedule where NgayKham = @formattedDate and ca = @ca", mydb.getConnection);
                    command.Parameters.Add("@formattedDate", formattedDate);
                    command.Parameters.Add("@ca", ca);

                    try
                    {
                        mydb.openConnection();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string dentistId = reader["DentistId"].ToString();
                            listIDBacSiCoLich.Add(dentistId); // Thêm giá trị vào ListBox
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    SqlCommand command1 = new SqlCommand("select * from dentist", mydb.getConnection);
                    try
                    {
                        mydb.openConnection();
                        SqlDataReader reader = command1.ExecuteReader();
                        Dentist dentist;
                        while (reader.Read())
                        {
                            string dentistId = reader["ID"].ToString();
                            string dentistName = reader["Name"].ToString();
                            double rating = Convert.ToDouble(reader["rating"].ToString());
                            dentist = new Dentist(dentistId, dentistName, rating);
                            listBacSi.Add(dentist); // Thêm giá trị vào ListBox
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }


                    // Xóa các panel cũ trước khi tạo mới


                    int panelWidth = 394; // Đặt chiều rộng của mỗi panel
                    int panelHeight = 162; // Đặt chiều cao của mỗi panel
                    int panelMargin = 10; // Đặt khoảng cách giữa các panel

                    int currentX = 10; // Vị trí ngang ban đầu của panel
                    int currentY = 10; // Vị trí dọc ban đầu của panel

                    foreach (Dentist x in listBacSi)
                    {

                        Panel panel = new Panel();
                        panel.BorderStyle = BorderStyle.FixedSingle;
                        panel.Width = panelWidth;
                        panel.Height = panelHeight;
                        panel.Location = new System.Drawing.Point(currentX, currentY);
                        string status = "Free";
                        if (checkStatus(x.id))
                        {
                            status = "Busy";
                        }
                        Test test = new Test(x.id, x.name, status, ca, formattedDate, x.rating);

                        panel1.Controls.Add(panel);
                        addUserControl(test, panel);

                        // Cập nhật vị trí ngang cho panel tiếp theo
                        currentX += panelWidth + panelMargin;

                        // Kiểm tra xem có cần xuống dòng không
                        if (currentX + panelWidth > panel1.Width)
                        {
                            currentX = 10; // Đặt lại vị trí ngang
                            currentY += panelHeight + panelMargin; // Đi xuống dòng
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một mục trong combobox.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }


        }
    }
    }

