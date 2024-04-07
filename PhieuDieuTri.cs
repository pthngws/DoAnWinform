using DoAn01.Home.Manage.Service;
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
    public partial class PhieuDieuTri : Form
    {
        public PhieuDieuTri()
        {
            InitializeComponent();
        }
        Service Service = new Service();
        private void PhieuDieuTri_Load(object sender, EventArgs e)
        {
            comboboxService.DataSource = Service.getAllServices();
            comboboxService.DisplayMember = "name";
            comboboxService.ValueMember = "id";
            comboboxService.SelectedItem = null;
        }
    }
}
