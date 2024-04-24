using DoAn01.Home.Schedule;
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

namespace DoAn01
{
    public partial class LichSuDieuTri : Form
    {
        public LichSuDieuTri()
        {
            InitializeComponent();
        }
        string patientid;
        public LichSuDieuTri(string id)
        {
            InitializeComponent();
            this.patientid = id;
        }
        Schedule schedule = new Schedule();
        private void LichSuDieuTri_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select id,dentistid,ngaykham from schedule where patientid= @id and tinhtrang = 'true'");
            cmd.Parameters.Add("@id", patientid);
            listBox1.DataSource = schedule.getSchedule(cmd);
            listBox1.DisplayMember = "ngaykham";
            listBox1.ValueMember ="id";

        }

        private void LichSuDieuTri_DoubleClick(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBox1.SelectedItem;
            string id = drv.Row[0].ToString();
            string dentistid = drv.Row[1].ToString();
            PhieuDieuTri phieu = new PhieuDieuTri(patientid,dentistid,id);
            phieu.Show();
        }
    }
}
