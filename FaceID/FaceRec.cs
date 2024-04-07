#region Assembly FaceRecognition, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\thang\source\repos\DoAn01\bin\Debug\FaceRecognition.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;

namespace DoAn01
{
    public class FaceRec
    {
        private double distance = 1E+19;

        private CascadeClassifier CascadeClassifier = new CascadeClassifier(Environment.CurrentDirectory + "/Haarcascade/haarcascade_frontalface_alt.xml");

        private Image<Bgr, byte> Frame = null;

        private Capture camera;

        private Mat mat = new Mat();

        private List<Image<Gray, byte>> trainedFaces = new List<Image<Gray, byte>>();

        private List<int> PersonLabs = new List<int>();

        private bool isEnable_SaveImage = false;

        private string ImageName;

        private PictureBox PictureBox_Frame;

        private PictureBox PictureBox_smallFrame;

        public string setPersonName;

        public bool isTrained = false;

        private List<string> Names = new List<string>();

        private EigenFaceRecognizer eigenFaceRecognizer;

        private IContainer components = null;
        public string NamePerson;

        public FaceRec()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Image"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Image");
            }
        }
        static string GetLetters(string input)
        {
            // Khởi tạo một chuỗi rỗng để lưu kết quả
            string letters = "";

            // Duyệt qua từng ký tự trong chuỗi đầu vào
            foreach (char c in input)
            {
                // Nếu gặp dấu `_`, trả về chuỗi letters hiện tại
                if (c == '_')
                    return letters;

                // Thêm ký tự vào chuỗi letters
                letters += c;
            }

            // Nếu không gặp dấu `_`, trả về chuỗi letters hiện tại
            return letters;
        }

        public void getPersonName(Control control)
        {
            Timer timer = new Timer();
            timer.Tick += timer_getPersonName_Tick;
            timer.Interval = 1000;
            timer.Start();

            void timer_getPersonName_Tick(object sender, EventArgs e)
            {
                // Sử dụng phương thức Invoke để đảm bảo việc cập nhật Text được thực hiện trên luồng giao diện người dùng
                control.Invoke((MethodInvoker)delegate {
                    control.Text = setPersonName;
                });
            }
        }


        public void openCamera(PictureBox pictureBox_Camera)
        {
            PictureBox_Frame = pictureBox_Camera;
            camera = new Capture();
            camera.ImageGrabbed += Camera_ImageGrabbed;
            camera.Start();
        }
        public void closeCamera()
        {
            if (camera != null)
            {
                camera.Dispose(); // Tắt camera và giải phóng tài nguyên
                Application.Idle -= Camera_ImageGrabbed;
            }
        }


        public void Save_IMAGE(string imageName)
        {
            ImageName = imageName;
            isEnable_SaveImage = true;
        }
        private void Camera_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                camera.Retrieve(mat);

                // Kiểm tra kích thước ma trận
                if (mat != null && mat.Rows > 0 && mat.Cols > 0)
                {
                    Frame = mat.ToImage<Bgr, byte>().Resize(PictureBox_Frame.Width, PictureBox_Frame.Height, Inter.Cubic);
                    detectFace();
                    PictureBox_Frame.Image = Frame.Bitmap;
                }
                else
                {
                    // Xử lý tình huống kích thước ma trận không hợp lệ
                    MessageBox.Show("Lỗi camera vui lòng thử lại!");
 
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khác
                MessageBox.Show("Lỗi xảy ra: " + ex.Message);
            }
        }



        private void detectFace()
        {
            Image<Bgr, byte> image = Frame.Convert<Bgr, byte>();
            Mat mat = new Mat();
            CvInvoke.CvtColor(Frame, mat, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(mat, mat);
            Rectangle[] array = CascadeClassifier.DetectMultiScale(mat, 1.1, 4);
            if (array.Length != 0)
            {
                Rectangle[] array2 = array;
                foreach (Rectangle rectangle in array2)
                {
                    CvInvoke.Rectangle(Frame, rectangle, new Bgr(Color.LimeGreen).MCvScalar, 2);
                    SaveImage(rectangle);
                    image.ROI = rectangle;
                    trainedIamge();
                    checkName(image, rectangle);
                }
            }
            else
            {
                setPersonName = "";
            }
        }

        private void SaveImage(Rectangle face)
        {
            if (isEnable_SaveImage)
            {
                string imageName = ImageName+"_";
                int count = 0;
                // Kiểm tra xem tên file đã tồn tại trong thư mục Image chưa
                while (File.Exists(Environment.CurrentDirectory + "\\Image\\" + imageName + ".jpg"))
                {
                    count++;
                    imageName = $"{ImageName+"_"}{count}"; // Cộng thêm số sau tên file
                }

                Image<Bgr, byte> image = Frame.Convert<Bgr, byte>();
                image.ROI = face;
                image.Resize(100, 100, Inter.Cubic).Save(Environment.CurrentDirectory + "\\Image\\" + imageName + ".jpg");
                isEnable_SaveImage = false;
                trainedIamge();
            }
        }

        private void trainedIamge()
        {
            try
            {
                int num = 0;
                trainedFaces.Clear();
                PersonLabs.Clear();
                Names.Clear();
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Image", "*.jpg", SearchOption.AllDirectories);
                string[] array = files;
                foreach (string text in array)
                {
                    Image<Gray, byte> item = new Image<Gray, byte>(text);
                    trainedFaces.Add(item);
                    PersonLabs.Add(num);
                    Names.Add(text);
                    num++;
                }

                eigenFaceRecognizer = new EigenFaceRecognizer(num, distance);
                eigenFaceRecognizer.Train(trainedFaces.ToArray(), PersonLabs.ToArray());
            }
            catch
            {
            }
        }

        private void checkName(Image<Bgr, byte> resultImage, Rectangle face)
        {
            try
            {
                if (isTrained)
                {
                    Image<Gray, byte> image = resultImage.Convert<Gray, byte>().Resize(100, 100, Inter.Cubic);
                    CvInvoke.EqualizeHist(image, image);
                    FaceRecognizer.PredictionResult predictionResult = eigenFaceRecognizer.Predict(image);
                    if (predictionResult.Label != -1 && predictionResult.Distance < distance)
                    {
                        setPersonName = Names[predictionResult.Label].Replace(Environment.CurrentDirectory + "\\Image\\", "").Replace(".jpg", "");
                        CvInvoke.PutText(Frame, GetLetters(setPersonName), new Point(face.X - 2, face.Y - 2), FontFace.HersheyPlain, 1.0, new Bgr(Color.LimeGreen).MCvScalar);
                        NamePerson = GetLetters(setPersonName);
                    }
                    else
                    {
                        CvInvoke.PutText(Frame, "Unknown", new Point(face.X - 2, face.Y - 2), FontFace.HersheyPlain, 1.0, new Bgr(Color.OrangeRed).MCvScalar);
                    }
                }
            }
            catch
            {
            }
        }



    }
#if false // Decompilation log
    '62' items in cache
    ------------------
    Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll'
    ------------------
    Resolve: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    Found single assembly: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Windows.Forms.dll'
    ------------------
    Resolve: 'Emgu.CV.World, Version=3.1.0.2282, Culture=neutral, PublicKeyToken=7281126722ab4438'
    Found single assembly: 'Emgu.CV.World, Version=3.1.0.2282, Culture=neutral, PublicKeyToken=7281126722ab4438'
    Load from: 'C:\Users\thang\source\repos\DoAn01\packages\EmguCV.3.1.0.1\lib\net30\Emgu.CV.World.dll'
    ------------------
    Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll'
    ------------------
    Resolve: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
    Found single assembly: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
    Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll'
#endif
}
