﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThuVienHam;

namespace App
{
    public partial class XemSuadethi : Form
    {
        Thuvien tv = new Thuvien();
        private DataTable dataTable;
        private int currentIndex;
        string logonname;
        public XemSuadethi(string uname)
        {
            logonname = uname;
            InitializeComponent();
        }

        private void XemSuadethi_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = App.Properties.Settings.Default.connectionstring;
            con.Open();
            string query = "SELECT * FROM Dethi where groups like '%" + logonname + "%'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            currentIndex = 0;
            ShowRecord(currentIndex);
        }
        private void ShowRecord(int index)
        {
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[index];
                macauhoi.Text = tv.giaima(row["macauhoi"].ToString());
                cauhoi.Text = tv.giaima(row["cauhoi"].ToString());
                string a = tv.giaima(row["dapan1"].ToString());
                string b = tv.giaima(row["dapan2"].ToString());
                string c = tv.giaima(row["dapan3"].ToString());
                string d = tv.giaima(row["dapan4"].ToString());
                textBoxA.Text = a;
                textBoxB.Text = b;
                textBoxC.Text = c;
                textBoxD.Text = d;

                // Hiển thị thông tin các trường dữ liệu khác tương ứng với bảng
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string cauHoi = cauhoi.Text;
            string dapAn1 = "";
            string dapAn2 = "";
            string dapAn3 = "";
            string dapAn4 = "";
            string dapAnDung = "-1";
            string maCauHoi = macauhoi.Text;
            if (checkBoxA.Checked)
            {
                dapAnDung = "1";
                dapAn1 = textBoxA.Text;
                dapAn2 = textBoxB.Text;
                dapAn3 = textBoxC.Text;
                dapAn4 = textBoxD.Text;
            }
            else
            if (checkBoxB.Checked)
            {
                dapAnDung = "2";
                dapAn1 = textBoxA.Text;
                dapAn2 = textBoxB.Text;
                dapAn3 = textBoxC.Text;
                dapAn4 = textBoxD.Text;
            }
            else
            if (checkBoxC.Checked)
            {
                dapAnDung = "3";
                dapAn1 = textBoxA.Text;
                dapAn2 = textBoxB.Text;
                dapAn3 = textBoxC.Text;
                dapAn4 = textBoxD.Text;
            }
            else
            if (checkBoxD.Checked)
            {
                dapAnDung = "4";
                dapAn1 = textBoxA.Text;
                dapAn2 = textBoxB.Text;
                dapAn3 = textBoxC.Text;
                dapAn4 = textBoxD.Text;
            }


            if (string.IsNullOrEmpty(cauHoi) || string.IsNullOrEmpty(dapAn1) || string.IsNullOrEmpty(dapAn2) || string.IsNullOrEmpty(dapAn3) || string.IsNullOrEmpty(dapAnDung) || string.IsNullOrEmpty(maCauHoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            SqlConnection con = new SqlConnection();
            con.ConnectionString = App.Properties.Settings.Default.connectionstring;
            con.Open();
            SqlCommand command = new SqlCommand("select * from Dethi where macauhoi = @Macauhoi and groups like @Logname%", con);
            command.Parameters.Add("@Macauhoi", SqlDbType.NVarChar, 200).Value = tv.mahoa(maCauHoi);
            command.Parameters.Add("@Logname", SqlDbType.NVarChar, 200).Value = "%" + logonname + "%";
            command.Prepare();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                con.Close();
                con.Open();
                
                SqlCommand cmand = new SqlCommand("UPDATE Dethi SET macauhoi = @MaCauHoi, groups = @Groups, cauhoi = @CauHoi, dapan1 = @DapAn1, dapan2 = @DapAn2, dapan3 = @DapAn3, dapan4 = @DapAn4, dapandung = @DapAnDung WHERE macauhoi = @MaCauHoi" , con);
                cmand.Parameters.AddWithValue("@CauHoi", tv.mahoa(cauHoi));
                cmand.Parameters.AddWithValue("@DapAn1", tv.mahoa(dapAn1));
                cmand.Parameters.AddWithValue("@DapAn2", tv.mahoa(dapAn2));
                cmand.Parameters.AddWithValue("@DapAn3", tv.mahoa(dapAn3));
                cmand.Parameters.AddWithValue("@DapAn4", tv.mahoa(dapAn4));
                cmand.Parameters.AddWithValue("@Groups", logonname);
                cmand.Parameters.AddWithValue("@DapAnDung", tv.mahoa(dapAnDung));
                cmand.Parameters.AddWithValue("@MaCauHoi", tv.mahoa(maCauHoi));
                cmand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Lưu câu hỏi thành công.");
            }
            else
            {
                MessageBox.Show("Lưu câu hỏi thất bại.");
            }
        }

        private void checkBoxA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxA.Checked == true)
            {
                checkBoxB.Checked = false;
                checkBoxC.Checked = false;
                checkBoxD.Checked = false;
            }
        }

        private void checkBoxB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxB.Checked == true)
            {
                checkBoxA.Checked = false;
                checkBoxC.Checked = false;
                checkBoxD.Checked = false;
            }
        }

        private void checkBoxC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxC.Checked == true)
            {
                checkBoxB.Checked = false;
                checkBoxA.Checked = false;
                checkBoxD.Checked = false;
            }
        }

        private void checkBoxD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxD.Checked == true)
            {
                checkBoxB.Checked = false;
                checkBoxC.Checked = false;
                checkBoxA.Checked = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = App.Properties.Settings.Default.connectionstring;
            con.Open();
            string mach = macauhoi.Text;
            SqlCommand command = new SqlCommand("select * from Dethi where macauhoi = @macauhoi and groups like @Logname", con);
            command.Parameters.Add("@macauhoi", SqlDbType.NVarChar, 200).Value = tv.mahoa(mach);
            command.Parameters.Add("@Logname", SqlDbType.NVarChar, 200).Value = "%" + logonname + "%";
            command.Prepare();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {               
                reader.Read();              
                macauhoi.Text = tv.giaima(reader["macauhoi"].ToString());
                cauhoi.Text = tv.giaima(reader["cauhoi"].ToString());
                textBoxA.Text = tv.giaima(reader["dapan1"].ToString());
                textBoxB.Text = tv.giaima(reader["dapan2"].ToString());
                textBoxC.Text = tv.giaima(reader["dapan3"].ToString());
                textBoxD.Text = tv.giaima(reader["dapan4"].ToString());               
            }
            else
            {
                MessageBox.Show("Không tìm thấy câu hỏi, hoặc bạn không có quyền hạn", "Thông báo");
            }                
           

        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowRecord(currentIndex);
            }
        }

        private void btnnext_Click_1(object sender, EventArgs e)
        {
            if (currentIndex < dataTable.Rows.Count - 1)
            {
                currentIndex++;
                ShowRecord(currentIndex);
            }
        }
    }
}
