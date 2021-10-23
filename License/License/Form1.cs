using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace License
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=MotorCross;User ID=" + textBox2.Text + ";Password=" + textBox3.Text + ";");
            SqlCommand com;
            string expired = toBase64("01 Jun 2025");

            try
            {
                con.Open();
                com = new SqlCommand("", con);
                com.CommandText = "Update KeyPairs set Value='" + expired + "' where KeyPairID='valid_until'";
                com.ExecuteNonQuery();
                MessageBox.Show("Berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message );
            }
            finally
            {
                con.Close();
            }

        }

        public string toBase64(string str)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(str));
        }

        public string fromBase64(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
    }
}
