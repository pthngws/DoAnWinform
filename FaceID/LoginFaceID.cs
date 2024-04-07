using DoAn01.Home;
using System;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace DoAn01
{
    public partial class LoginFaceID : Form
    {

        private string personName;
        private FaceRec faceRec = new FaceRec();
        private Timer timer;
        private bool loginSuccess = false;

        public LoginFaceID()
        {
            InitializeComponent();
            label1.Text = "Khách Hàng";
            faceRec.openCamera(guna2CirclePictureBox1);
            faceRec.isTrained = true;
            // Khởi tạo timer và cấu hình thời gian cập nhật
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 100; // Cập nhật mỗi 500ms
            timer.Start();
        }


        // Hàm xử lý sự kiện cập nhật tên người dùng
        private int timerTickCount = 0;
        private const int TimerDelayInSeconds = 2; // Delay for 3 seconds


        private void Timer_Tick(object sender, EventArgs e)
        {
            timerTickCount++;
            personName = faceRec.NamePerson;
            label1.Text = personName;

            if (timerTickCount >= TimerDelayInSeconds * 10 && !loginSuccess && timer.Enabled)
            {
                if (!string.IsNullOrEmpty(personName) && personName != "Khách hàng")
                {
                    Patient patient = new Patient();
                    patient = patient.checkPatient(personName);
                    if (patient != null)
                    {
                        // Đóng camera trước khi đóng form
                        // Tạm dừng timer trước khi đóng form
                        faceRec.closeCamera();
                        timer.Stop();
                        loginSuccess = true;
                        faceRec.isTrained = false;

                        InfoPatient infoPatientForm = new InfoPatient(patient)
                        {
                            StartPosition = FormStartPosition.CenterScreen
                        };
                        infoPatientForm.Show();
                        // Đóng form hiện tại
                        this.Dispose();
                    }
                }
            }
        }

        private void LoginFaceID_Load(object sender, EventArgs e)
        {

        }

        private void LoginFaceID_FormClosed(object sender, FormClosedEventArgs e)
        {
            faceRec.closeCamera();
        }

        private void LoginFaceID_FormClosing(object sender, FormClosingEventArgs e)
        {
            faceRec.closeCamera();
        }
    }
}