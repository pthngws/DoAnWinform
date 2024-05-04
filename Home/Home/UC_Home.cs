using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class UC_Home : UserControl
    {
        private string[] imagePaths = {
            @"C:\Users\thang\source\repos\DoAnWinform\Home_Load\z5409174444330_5b6c43afbf0a4d96d396e9d7ce6ae853.jpg",
            @"C:\Users\thang\source\repos\DoAnWinform\Home_Load\z5409146973037_b1b2f71e1df6ffc071913d491a34d215.jpg",
            @"C:\Users\thang\source\repos\DoAnWinform\Home_Load\istockphoto-1222376322-612x612.jpg"
        };

        private int currentImageIndex = 0;
        private Timer timer = new Timer();

        public UC_Home()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(imagePaths[0]);

            // Gán sự kiện CheckedChanged cho các RadioButton
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;

            // Bắt đầu Timer
            timer.Interval = 1000; // 5 giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Di chuyển đến RadioButton tiếp theo
            currentImageIndex = (currentImageIndex + 1) % 3;

            // Dựa vào chỉ số, chọn RadioButton tương ứng
            switch (currentImageIndex)
            {
                case 0:
                    radioButton1.Checked = true;
                    break;
                case 1:
                    radioButton2.Checked = true;
                    break;
                case 2:
                    radioButton3.Checked = true;
                    break;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pictureBox1.Image = Image.FromFile(imagePaths[0]);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                pictureBox1.Image = Image.FromFile(imagePaths[1]);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                pictureBox1.Image = Image.FromFile(imagePaths[2]);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2RatingStar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
