using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01.Home.Schedule
{
    public partial class Test : UserControl
    {
        public Test()
        {
            InitializeComponent();
        }
        string ca;
        string id;
        string name;
        string status;
        string date;
        double rating;
        public Test(string id, string name, string status,string ca,string date,double rating)
        {
            InitializeComponent();
            this.id = id;
            this.status = status;
            this.name = name;
            this.ca = ca;
            this.date = date;
            this.rating = rating;
        }   

        private void Test_Load(object sender, EventArgs e)
        {
            if(status == "Busy")
                BackColor = Color.DarkRed;
           
            label1.Text =  name;
            // Tính toán vị trí của Label
            int labelX = (this.ClientSize.Width - label1.Width) / 2;

            // Đặt vị trí cho Label
            label1.Location = new Point(labelX, 35);

            label2.Text = "Status: " + status;
            guna2RatingStar1.Value =Convert.ToInt32(rating);
        }

        private void Test_Click(object sender, EventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule = schedule.getSchedule(id,ca,date);
            ScheduleForm schedulef = new ScheduleForm(schedule,ca,id,date);
            schedulef.Show();
        }
    }
}
