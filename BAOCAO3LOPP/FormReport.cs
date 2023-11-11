using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using BUS;

namespace BAOCAO3LOPP
{
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }
        B_HocSinh b = new B_HocSinh();
        private void FormReport_Load(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "BAOCAO3LOPP.Report1.rdlc";
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = b.GetAllHocSinhh();
                //reportDataSource.Value = B_HocSinh.GetAllHocSinh();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                this.reportViewer1.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
