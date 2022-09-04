using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StockManagementSoftware
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxUser.Text = "";
            tbxPass.Clear();
            tbxUser.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=server2;Initial Catalog=TestDB1;Persist Security Info=True;User ID=sa;Password=ammonia"); //connect to DB
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [TestDB1].[dbo].[Login] Where Username='"+tbxUser.Text+"' and Password = '"+tbxPass.Text+"'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
                { 
                    this.Hide(); //hide login

                    StockMain main = new StockMain();
                    main.Show(); //show main page

                }
            else
            {

                MessageBox.Show("Invalid Username or Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);//if input wrong password and username
                btnClear_Click(sender, e);//clear textbox after getting error 
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
