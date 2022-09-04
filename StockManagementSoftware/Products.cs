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

namespace StockManagementSoftware
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=server2;Initial Catalog=TestDB1;Persist Security Info=True;User ID=sa;Password=ammonia"); //connect to DB

            conn.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {

                status = true;


            }
            else 
            { 
                status = false;
            }



            var sqlQuery = "";
            if (IfProductsExists(conn, textBox1.Text))

            {
                sqlQuery = @"UPDATE [Products] SET [ProductNamem] = '" + textBox2.Text + "' ,[Status] ='" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "'";

            }
            else 
            {

                sqlQuery = @"INSERT INTO [TestDB1].[dbo].[Products] ([ProductCode],[ProductName],[Status]) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
            }

            SqlCommand cmd = new SqlCommand(sqlQuery,conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            LoadData();
            //read data


        }

        private bool IfProductsExists(SqlConnection conn, string productCode) 
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [Products] WHERE [ProductCode] = '" + productCode+ "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            return true;
            else 
                return false; 


        }

        public void LoadData() {
            SqlConnection conn = new SqlConnection("Data Source=server2;Initial Catalog=TestDB1;Persist Security Info=True;User ID=sa;Password=ammonia"); //connect to DB
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [TestDB1].[dbo].[Products] ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
            
                if ((bool)item["Status"])
                {

                    dataGridView1.Rows[n].Cells[2].Value = "Active";


                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }
           
            }
        
        }

        private void Products_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {

                comboBox1.SelectedIndex = 0;


            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=server2;Initial Catalog=TestDB1;Persist Security Info=True;User ID=sa;Password=ammonia"); //connect to DB
            var sqlQuery = "";
            if (IfProductsExists(conn, textBox1.Text))

            {
                conn.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Record Not exist");
            }

            


            conn.Close();
            LoadData();
        }
    }////////////////////to be continued done login and product list. 
}
