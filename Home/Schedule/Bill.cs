using DoAn01.Home.Schedule;
using Guna.UI2.WinForms;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class Bill : Form
    {
        MY_DB mydb = new MY_DB();

        string scheduleID;
        public Bill()
        {
            InitializeComponent();
        }
        public Bill( string scheduleID)
        {
            InitializeComponent();
            this.scheduleID = scheduleID;
        }
        // Code này chỉ là một phần của class Bill, hãy bổ sung vào phần còn lại của class

        private void Bill_Load(object sender, EventArgs e)
        {
            string sql1 = @"
        SELECT 
            P.Name as patientName, 
            P.Address as patientAddress, 
            P.Phone as patientPhone, 
            S.NgayKham as Date
        FROM 
            Patient AS P, Schedule AS S
        WHERE 
            S.Id = @Sid 
            AND P.id = S.PatientId";

            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, mydb.getConnection);
            adapter1.SelectCommand.Parameters.AddWithValue("@Sid", scheduleID);

            DataSet dataSet1 = new DataSet();
            adapter1.Fill(dataSet1, "DataTable1");

            ReportDataSource rds1 = new ReportDataSource();
            rds1.Name = "DataSetPatient";
            rds1.Value = dataSet1.Tables["DataTable1"];

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "DoAn01.Home.Schedule.ReportBill.rdlc"; // chỉ cần thiết lập một lần

            this.reportViewer1.LocalReport.DataSources.Add(rds1);

            string sql2 = @"
                    SELECT Medicine.Name AS 'ServiceName', 
                           COUNT(*) AS 'Quantity', 
                           Medicine.Price * COUNT(*) AS 'Price' 
                    FROM LichSuThuoc
                    INNER JOIN Medicine ON LichSuThuoc.idMedicine = Medicine.Id
                    WHERE LichSuThuoc.idSchedule = @Sid
                    GROUP BY Medicine.Name, Medicine.Price

                    UNION

                    SELECT Service.Name AS 'ServiceName', 
                           COUNT(*) AS 'Quantity', 
                           Service.Price * COUNT(*) AS 'Price'
                    FROM LichSuDichVu
                    INNER JOIN Service ON LichSuDichVu.idService = Service.Id
                    WHERE LichSuDichVu.idSchedule = @Sid
                    GROUP BY Service.Name, Service.Price;
            ";

            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, mydb.getConnection);
            adapter2.SelectCommand.Parameters.AddWithValue("@Sid", scheduleID);

            DataSet dataSet2 = new DataSet();
            adapter2.Fill(dataSet2, "DataTable2");

            ReportDataSource rds2 = new ReportDataSource();
            rds2.Name = "DataSetService";
            rds2.Value = dataSet2.Tables["DataTable2"];

            this.reportViewer1.LocalReport.DataSources.Add(rds2);

            this.reportViewer1.RefreshReport();
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

