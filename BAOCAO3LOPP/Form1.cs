using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using NPOI.SS.UserModel;
using System.Configuration;
using System.Data.OleDb;

namespace BAOCAO3LOPP
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dt;
        
        public Form1()
        {
            InitializeComponent();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dgvHocSinh.DataSource = B_HocSinh.GetAllHocSinh();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            //    {
            //        if (cn.State == ConnectionState.Closed)
            //            cn.Open();
            //        using (DataTable dt = new DataTable("HocSinh"))
            //        {

            //            using (SqlCommand cmd = new SqlCommand("SELECT * FROM HocSinh WHERE MaHS LIKE '%' + MaHS +'%'", cn))//select *from HocSinh where MaHS=@MaHS or TenHS like @TenHS
            //            {
            //                cmd.Parameters.AddWithValue("MaHS", txtSearch.Text);
            //                cmd.Parameters.AddWithValue("TenHS", string.Format("%{0}%", txtSearch.Text));
            //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //                adapter.Fill(dt);
            //                dgvHocSinh.DataSource = dt;

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //string rowFilter = string.Format("{0} like '{1}'", "TenHS", "*" + txtSearch.Text + "*");

            //string rowFilter1 = string.Format("{0} like '{1}'", "MaHS", "*" + txtSearch.Text + "*");

            //string rowFilter2 = string.Format("{0} like '{1}'", "DiaChi", "*" + txtSearch.Text + "*");

            string rowFilter = string.Format("{0} like '{1}' or {2} like '{3}' or {4} like '{5}'", "TenHS", "*" + txtSearch.Text + "*", "MaHS", "*" + txtSearch.Text + "*", "DiaChi", "*" + txtSearch.Text + "*");



            //(dgvHocSinh.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            //(dgvHocSinh.DataSource as DataTable).DefaultView.RowFilter = rowFilter1;
            (dgvHocSinh.DataSource as DataTable).DefaultView.RowFilter = rowFilter;

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)//enter
                btnSearch.PerformClick();
            if (txtSearch.Text=="")
            {
                dgvHocSinh.DataSource = B_HocSinh.GetAllHocSinh();
            }
        }
        private Form currentFormChild;
        
        private void OpenChildForm(Form childForm)
        {

            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.None;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            panel1.BringToFront();
            childForm.Show();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormA());
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //dgvHocSinh.DataSource = B_HocSinh.GetAllHocSinh();
           
        }

        private void dgvHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = new DataGridViewRow();
                row = dgvHocSinh.Rows[e.RowIndex];
                txtMa.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                dtpNs.Text = row.Cells[2].Value.ToString();
                txtDiachi.Text = row.Cells[3].Value.ToString();
                txtDiem.Text = row.Cells[4].Value.ToString();
                txtXeploai.Text = row.Cells[5].Value.ToString();
            }
            catch { }

        }

      

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(ktdiem(float.Parse(txtDiem.Text)))
                {
                try
                {
                    string mahs = txtMa.Text;
                    string tenhs = txtTen.Text;
                    DateTime ngays = dtpNs.Value;
                    string diachi = txtDiachi.Text;
                    float diem = float.Parse(txtDiem.Text);
                    string xeploai = txtXeploai.Text;
                    tblHocSinh hocsinh = new tblHocSinh(mahs, tenhs, ngays, diachi, diem, xeploai);
                    B_HocSinh.InsertHocSinh(hocsinh);
                    MessageBox.Show("Ban da them " + tenhs + "thanh cong");
                    dgvHocSinh.DataSource = B_HocSinh.GetAllHocSinh();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Điểm chỉ nhâp từ 0 đến 10");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ktdiem(float.Parse(txtDiem.Text)))
            {
                try
                {
                    string mahs = txtMa.Text;
                    string tenhs = txtTen.Text;
                    DateTime ngays = dtpNs.Value;
                    string diachi = txtDiachi.Text;
                    float diem = float.Parse(txtDiem.Text);
                    string xeploai = txtXeploai.Text;
                    tblHocSinh hocsinh = new tblHocSinh(mahs, tenhs, ngays, diachi, diem, xeploai);
                    B_HocSinh.UpdateHocSinh(hocsinh);
                    MessageBox.Show("Ban da sua " + tenhs + "thanh cong");
                    dgvHocSinh.DataSource = B_HocSinh.GetAllHocSinh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Điểm chỉ nhâp từ 0 đến 10");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            try
            {
                string mahs = txtMa.Text;
                string tenhs = txtTen.Text;
                DateTime ngays = dtpNs.Value;
                string diachi = txtDiachi.Text;
                float diem = float.Parse(txtDiem.Text);
                string xeploai = txtXeploai.Text;
                tblHocSinh hocsinh = new tblHocSinh(mahs, tenhs, ngays, diachi, diem, xeploai);
                B_HocSinh.DeleteHocSinh(mahs);
                MessageBox.Show("Ban da xoa " + tenhs +" "+ "thanh cong");
                dgvHocSinh.DataSource = B_HocSinh.GetAllHocSinh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void ExportExcel(string path)
        {
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            for(int i=0; i<dgvHocSinh.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dgvHocSinh.Columns[i].HeaderText;
            }
            for (int i = 0; i < dgvHocSinh.Rows.Count; i++)
            {
                for(int j =0;j<dgvHocSinh.Columns.Count;j++)
                {
                    application.Cells[i + 2, j + 1] = dgvHocSinh.Rows[i].Cells[j].Value;
                }
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(path);
            application.ActiveWorkbook.Saved = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            

            ////Xuất Excel chạy được ,import vô được nhma không khớp bảng với code import Excel 
            //dgvHocSinh.SelectAll();
            //DataObject copydata = dgvHocSinh.GetClipboardContent();
            //if (copydata != null) Clipboard.SetDataObject(copydata);
            //Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            //xlapp.Visible = true;
            //Microsoft.Office.Interop.Excel.Workbook xlWbook;
            //Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            //object miseddata = System.Reflection.Missing.Value;
            //xlWbook = xlapp.Workbooks.Add(miseddata);

            //xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWbook.Worksheets.get_Item(1);
            //Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            //xlr.Select();
            //xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            ////----------------------------------------------------------------------------------------------------------
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Excel";
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcel(saveFileDialog.FileName);
                    MessageBox.Show("Xuat file thanh cong!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xuat file khong thanh cong!\n" + ex.Message);
                }
            }

        }
        private void ImportExcel(String path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dataTable = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dataTable.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRows = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        listRows.Add(excelWorksheet.Cells[i, j].Value.ToString());
                    }
                    dataTable.Rows.Add(listRows.ToArray());
                }
                dgvHocSinh.DataSource = dataTable;
            }
        }

        private void btn_importExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Excel";
            openFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportExcel(openFileDialog.FileName);
                    MessageBox.Show("Nhap file thanh cong!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nhap file khong thanh cong!\n" + ex.Message);
                }
            }
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            FormReport formReport = new FormReport();
            formReport.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

     

        private void txtMa_TextChanged(object sender, EventArgs e)
        {

        }

      public bool ktdiem(float a)
        {
            bool flag=false;
            if (a >= 0 && a <= 10)
            {
                flag = true;
            }
            return flag;
        }
        public bool ktso(string a)
        {bool flag=true;

            if (!a.All(Char.IsDigit))
            {
                MessageBox.Show("Tên chỉ được nhập số.");
                txtDiem.Text = null;
                flag=false;
                return flag;
            }
            return flag;
        }

        private void txtDiem_KeyDown(object sender, KeyEventArgs e)
        {
            
          
        }

        private void txtDiem_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void txtDiem_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void txtDiem_TextChanged(object sender, EventArgs e)
        {
            if (ktso(txtDiem.Text) == false)
            {
                txtDiem.Text = null;
            }
        }

        private void txtTen_TextChanged(object sender, EventArgs e)
        {
            if (!txtTen.Text.All(Char.IsLetter))
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show("Tên chỉ được nhập chữ.");
                // Đặt focus vào ô tên
                txtTen.Focus();
            }
        }


        //private bool KiemTraThongTin()
        //{
        //    if (txtMa.Text == "")
        //    {
        //        MessageBox.Show("Vui lòng điền mã của học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        txtMa.Focus();
        //        return false;
        //    }
        //    if (txtTen.Text == "")
        //    {
        //        MessageBox.Show("Vui lòng điền tên của học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        txtTen.Focus();
        //        return false;
        //    }
        //    if (dtpNs.Text == "")
        //    {
        //        MessageBox.Show("Vui lòng điền ngày sinh của học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        dtpNs.Focus();
        //        return false;
        //    }

        //    if (txtDiachi.Text == "")
        //    {
        //        MessageBox.Show("Vui lòng điền địa chỉ của học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        txtDiachi.Focus();
        //        return false;
        //    }
        //    if (txtDiem.Text == "")
        //    {
        //        MessageBox.Show("Vui lòng điền điểm của học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        txtDiem.Focus();
        //        return false;
        //    }
        //    return true;
        //}

    }
}


