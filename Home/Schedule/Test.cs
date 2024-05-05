using DoAn01.Home.Manage.Dentist;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DoAn01.Home.Schedule
{
    public partial class Test : UserControl
    {
        private string ca;
        private string id;
        private string name;
        private string status;
        private string date;

        public Test()
        {
            InitializeComponent();
        }

        public Test(string id, string name, string status, string ca, string date) : this()
        {
            this.id = id;
            this.status = status;
            this.name = name;
            this.ca = ca;
            this.date = date;
            if (Global.GlobalRole == "dentist")
            {
                label1.Visible = false;
                guna2RatingStar1.Visible = false;
                label3.Visible = true;
                label3.Text =  ca;
            }
                using (SqlConnection connection = mydb.getConnection)
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT AVG(rating) AS rating FROM Schedule INNER JOIN PhieuDIeuTri ON Schedule.Id = PhieuDIeuTri.scheduleid WHERE Schedule.DentistId = @did", connection))
                {
                    cmd.Parameters.AddWithValue("@did", id);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["rating"] != DBNull.Value)
                        {
                            double averageRating;
                            if (double.TryParse(dataTable.Rows[0]["rating"].ToString(), out averageRating))
                            {
                                // Chỉ đặt giá trị cho guna2RatingStar1 nếu giá trị trung bình rating hợp lệ (trong khoảng từ 0 đến 5)
                                if (averageRating >= 0 && averageRating <= 5)
                                {
                                    guna2RatingStar1.Value = Convert.ToUInt32(averageRating);

                                }
                                else
                                {
                                    guna2RatingStar1.Value = 0; // hoặc giá trị mặc định khác tùy thuộc vào yêu cầu của bạn
                                }
                            }
                            else
                            {
                                guna2RatingStar1.Value = 0;
                            }
                        }
                        else
                        {
                            guna2RatingStar1.Value = 0;
                        }

                    }
                }

            }
        }

        MY_DB mydb = new MY_DB();

        private void Test_Load(object sender, EventArgs e)
        {
            if (status == "Busy")
                BackColor = Color.DarkRed;

            label1.Text = name;
            // Tính toán vị trí của Label
            int labelX = (this.ClientSize.Width - label1.Width) / 2;

            // Đặt vị trí cho Label
            label1.Location = new Point(labelX, 35);

            label2.Text = "Status: " + status;
        }

        private void Test_Click(object sender, EventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule = schedule.getSchedule(id, ca, date);
            ScheduleForm schedulef = new ScheduleForm(schedule, ca, id, date) { StartPosition = FormStartPosition.CenterScreen };
            schedulef.Show();
        }
    }
}

