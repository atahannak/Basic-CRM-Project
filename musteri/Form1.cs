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

namespace musteri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

   


        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=customerdb;Integrated Security=True;");

            try
            {
                con.Open(); 

                
                SqlCommand cnn = new SqlCommand("insert into customer (musterinumarası, musteriadı, odeme1, odeme2, odeme3, tarih) values (@musterinumarası, @musteriadı, @odeme1, @odeme2, @odeme3, @tarih)", con);

                cnn.Parameters.AddWithValue("@musterinumarası", int.Parse(textBox1.Text));
                cnn.Parameters.AddWithValue("@musteriadı", textBox2.Text);
                cnn.Parameters.AddWithValue("@odeme1", textBox3.Text);
                cnn.Parameters.AddWithValue("@odeme2", textBox4.Text);
                cnn.Parameters.AddWithValue("@odeme3", textBox5.Text);
                cnn.Parameters.AddWithValue("@tarih", textBox6.Text);

                cnn.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarıyla Eklendi", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while connecting to the SQL Server: " + ex.Message);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }

        void BindData()
        {
            string connectionString = @"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=customerdb;Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Bağlantıyı aç

                    SqlCommand bindCommand = new SqlCommand("SELECT * FROM customer", con);
                    SqlDataAdapter da = new SqlDataAdapter(bindCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Server'a bağlanırken bir hata oluştu: " + ex.Message);
                }
            }
        }

     

    private void btnNew_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=customerdb;Integrated Security=True;");

            try
            {
                con.Open(); // Open the connection to ensure it works

                // Corrected SQL command syntax
                SqlCommand cnn = new SqlCommand("DELETE FROM customer WHERE musterinumarası = @musterinumarası", con);

                cnn.Parameters.AddWithValue("@musterinumarası", int.Parse(textBox1.Text));

                cnn.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarıyla Silindi", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while connecting to the SQL Server: " + ex.Message);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }

        private void btnSearchByNumber_Click_1(object sender, EventArgs e)
        {

                SearchCustomerByNumber();

            void SearchCustomerByNumber()
            {
                string connectionString = @"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=customerdb;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open(); // Bağlantıyı aç

                        // SQL SELECT sorgusu
                        SqlCommand searchCommand = new SqlCommand("SELECT * FROM customer WHERE musterinumarası = @musterinumarası", con);

                        // Parametre ekleme
                        searchCommand.Parameters.AddWithValue("@musterinumarası",(textBox7.Text));

                        // Sonuçları almak için SqlDataAdapter ve DataTable kullanma
                        SqlDataAdapter da = new SqlDataAdapter(searchCommand);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Veri tablosunun dolu olup olmadığını kontrol et
                        if (dt.Rows.Count > 0)
                        {
                            // Sonuçları dataGridView1'e bağlama
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Müşteri bulunamadı.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL Server'a bağlanırken bir hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            {
                BindData();
            }

        }
    }
}
