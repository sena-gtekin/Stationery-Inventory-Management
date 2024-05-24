using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _20200305013
{
    public partial class Form2 : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=homework;Uid=root;Pwd=senadmin");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;

        void product()
        {
            dt = new DataTable();
            con.Open();
            adapter = new MySqlDataAdapter("SELECT *FROM products", con);
            dataGridView1.DataSource = dt;
            adapter.Fill(dt);
            con.Close();
        }
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            product();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            {
                throw;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO products(name , brand , stock , barcode ) " + "VALUES(@name , @brand , @stock , @barcode)";
            cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("name", textBox1.Text);
            cmd.Parameters.AddWithValue("brand", textBox2.Text);
            cmd.Parameters.AddWithValue("stock", textBox3.Text);
            cmd.Parameters.AddWithValue("barcode", textBox4.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            product();
            MessageBox.Show("New Product addition succesful", "succesful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = ("DELETE FROM products WHERE name=@name");
            cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("name", textBox1.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            product();
            MessageBox.Show("Product deletion succesful", "succesful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE products SET name=@name , brand=@brand , stock=@stock , barcode=@barcode " + "WHERE name=@name ";
            cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("name", textBox1.Text);
            cmd.Parameters.AddWithValue("brand", textBox2.Text);
            cmd.Parameters.AddWithValue("stock", textBox3.Text);
            cmd.Parameters.AddWithValue("barcode", textBox4.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            product();
            MessageBox.Show("Product update succesful", "succesful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string filterText = textBox5.Text.Trim();
            if (!string.IsNullOrEmpty(filterText))
            {
                DataView dv = dt.DefaultView;

                if (radioButton1.Checked)
                {
                    dv.RowFilter = "name LIKE '%" + filterText + "%'"; // Filter by product name
                }
                else if (radioButton2.Checked)
                {
                    dv.RowFilter = "brand LIKE '%" + filterText + "%'"; // Filter by brand
                }
                else if (radioButton3.Checked)
                {
                    int stockValue;
                    if (int.TryParse(filterText, out stockValue))
                    {
                        dv.RowFilter = "stock >= " + stockValue; // Filter by stock (assuming numeric)
                    }
                    else
                    {
                        // Handle invalid input for stock filtering
                        MessageBox.Show("Invalid stock value. Please enter a numeric value.");
                    }
                }

                dataGridView1.DataSource = dv;
            }
            else
            {
                dataGridView1.DataSource = dt; // Clear filter when text is empty
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            {

                Form3 form3 = new Form3();
                this.Hide();
                form3.Show();
            }
        }
    }
}



