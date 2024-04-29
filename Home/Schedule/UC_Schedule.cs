using DoAn01.Home.Manage.Dentist;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            if(Global.GlobalRole =="dentist")
            {
                comboBox1.Visible = false;
                guna2ImageButton1.Visible = false;
            }
        }

        private void UC_Schedule_Load(object sender, EventArgs e)
        {
            guna2ImageButton1_Click(sender, e);
        }
        private void addUserControl(UserControl userControl, Panel panel)
        {
            userControl.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(userControl);
            userControl.BringToFront();
        }
        public bool checkStatus(string id, string ca, string date)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM schedule WHERE dentistid = @id AND ca = @ca AND ngaykham = @date";
                using (SqlCommand cmd = new SqlCommand(query, mydb.getConnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ca", ca);
                    cmd.Parameters.AddWithValue("@date", date);
                    mydb.openConnection();

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0; // Trả về true nếu có ít nhất một hàng chứa cặp giá trị id, ca và ngày đã cho
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra trạng thái: " + ex.Message);
                return false;
            }
            finally
            {
                mydb.closeConnection();
            }
        }


        public bool checkStatus(string id)

            {
                foreach (string x in listIDBacSiCoLich)
                {
                    if (id == x)
                        return true;

                }
            return false;
        }


        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            string ca = "";
            try
            {
                DateTime date = dateTimePicker1.Value;
                string formattedDate = date.ToString("yyyy/MM/dd");


                // Xóa các panel cũ trước khi tạo mới


                int panelWidth = 394; // Đặt chiều rộng của mỗi panel
                int panelHeight = 162; // Đặt chiều cao của mỗi panel
                int panelMargin = 10; // Đặt khoảng cách giữa các panel

                int currentX = 10; // Vị trí ngang ban đầu của panel
                int currentY = 10; // Vị trí dọc ban đầu của panel
                if (Global.GlobalRole == "dentist")
                {
                    Dentist dentist = new Dentist();
                    dentist = dentist.GetDentistById(Global.GlobalID);
                    List<string> Calam = new List<string>() { "7-9", "9-11", "11-13", "13-15", "15-17", "17-21" };

                    foreach (string x in Calam)
                    {
                        Panel panel = new Panel();


                        panel.Width = panelWidth;
                        panel.Height = panelHeight;
                        panel.Location = new System.Drawing.Point(currentX, currentY);

                        // Độ cong của góc (tùy chỉnh để điều chỉnh vẻ ngoài mềm mại)
                        int borderRadius = 10;

                        GraphicsPath boGocPath = new GraphicsPath();
                        boGocPath.AddArc(new Rectangle(0, 0, 2 * borderRadius, 2 * borderRadius), 180, 90); // Góc trên bên trái
                        boGocPath.AddArc(new Rectangle(panel.Width - 2 * borderRadius, 0, 2 * borderRadius, 2 * borderRadius), -90, 90); // Góc trên bên phải
                        boGocPath.AddArc(new Rectangle(panel.Width - 2 * borderRadius, panel.Height - 2 * borderRadius, 2 * borderRadius, 2 * borderRadius), 0, 90); // Góc dưới bên phải
                        boGocPath.AddArc(new Rectangle(0, panel.Height - 2 * borderRadius, 2 * borderRadius, 2 * borderRadius), 90, 90); // Góc dưới bên trái
                        panel.Region = new Region(boGocPath);


                        string status = "Free";
                        if (checkStatus(dentist.id,x,formattedDate))
                        {
                            status = "Busy";
                        }
                        Test test = new Test(dentist.id, dentist.name, status, x, formattedDate);

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
                    if (comboBox1.SelectedItem != null)
                    {
                        ca = comboBox1.SelectedItem.ToString();
                        panel1.Controls.Clear();
                        listIDBacSiCoLich.Clear();
                        listBacSi.Clear();


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

                                dentist = new Dentist(dentistId, dentistName);
                                listBacSi.Add(dentist); // Thêm giá trị vào ListBox
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        foreach (Dentist x in listBacSi)
                        {

                            Panel panel = new Panel();


                            panel.Width = panelWidth;
                            panel.Height = panelHeight;
                            panel.Location = new System.Drawing.Point(currentX, currentY);

                            // Độ cong của góc (tùy chỉnh để điều chỉnh vẻ ngoài mềm mại)
                            int borderRadius = 10;

                            GraphicsPath boGocPath = new GraphicsPath();
                            boGocPath.AddArc(new Rectangle(0, 0, 2 * borderRadius, 2 * borderRadius), 180, 90); // Góc trên bên trái
                            boGocPath.AddArc(new Rectangle(panel.Width - 2 * borderRadius, 0, 2 * borderRadius, 2 * borderRadius), -90, 90); // Góc trên bên phải
                            boGocPath.AddArc(new Rectangle(panel.Width - 2 * borderRadius, panel.Height - 2 * borderRadius, 2 * borderRadius, 2 * borderRadius), 0, 90); // Góc dưới bên phải
                            boGocPath.AddArc(new Rectangle(0, panel.Height - 2 * borderRadius, 2 * borderRadius, 2 * borderRadius), 90, 90); // Góc dưới bên trái
                            panel.Region = new Region(boGocPath);


                            string status = "Free";
                            if (checkStatus(x.id))
                            {
                               
                                status = "Busy";
                            }
                            Test test = new Test(x.id, x.name, status, ca, formattedDate);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (Global.GlobalRole == "dentist")
            {
                panel1.Controls.Clear(); // Xóa các Panel cũ trước khi tạo mới


                DateTime date = dateTimePicker1.Value;
                string formattedDate = date.ToString("yyyy/MM/dd");

                int panelWidth = 394; // Đặt chiều rộng của mỗi panel
                int panelHeight = 162; // Đặt chiều cao của mỗi panel
                int panelMargin = 10; // Đặt khoảng cách giữa các panel

                int currentX = 10; // Vị trí ngang ban đầu của panel
                int currentY = 10; // Vị trí dọc ban đầu của panel
                if (Global.GlobalRole == "dentist")
                {
                    Dentist dentist = new Dentist();
                    dentist = dentist.GetDentistById(Global.GlobalID);
                    List<string> Calam = new List<string>() { "7-9", "9-11", "11-13", "13-15", "15-17", "17-21" };

                    foreach (string x in Calam)
                    {
                        Panel panel = new Panel();


                        panel.Width = panelWidth;
                        panel.Height = panelHeight;
                        panel.Location = new System.Drawing.Point(currentX, currentY);

                        // Độ cong của góc (tùy chỉnh để điều chỉnh vẻ ngoài mềm mại)
                        int borderRadius = 10;

                        GraphicsPath boGocPath = new GraphicsPath();
                        boGocPath.AddArc(new Rectangle(0, 0, 2 * borderRadius, 2 * borderRadius), 180, 90); // Góc trên bên trái
                        boGocPath.AddArc(new Rectangle(panel.Width - 2 * borderRadius, 0, 2 * borderRadius, 2 * borderRadius), -90, 90); // Góc trên bên phải
                        boGocPath.AddArc(new Rectangle(panel.Width - 2 * borderRadius, panel.Height - 2 * borderRadius, 2 * borderRadius, 2 * borderRadius), 0, 90); // Góc dưới bên phải
                        boGocPath.AddArc(new Rectangle(0, panel.Height - 2 * borderRadius, 2 * borderRadius, 2 * borderRadius), 90, 90); // Góc dưới bên trái
                        panel.Region = new Region(boGocPath);


                        string status = "Free";
                        if (checkStatus(dentist.id, x, formattedDate))
                        {
                            status = "Busy";
                        }
                        Test test = new Test(dentist.id, dentist.name, status, x, formattedDate);

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
            }
        }
    }
    }

