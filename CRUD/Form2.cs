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

namespace CRUD
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                if (tbName.Text != "" && tbLogin.Text != "" && tbPassword.Text != "")
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = CRUD; Integrated Security = true"))
                    {
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "select login from users where login = '" + tbLogin.Text + "'";
                        string login = Convert.ToString(cmd.ExecuteScalar());
                        if (login != "")
                        {
                            MessageBox.Show("Данный логин занят");
                        }
                        else
                        {
                            SqlCommand command = new SqlCommand("INSERT INTO users (login,password,name) VALUES (@login,@password,@name)", connection);
                            command.Parameters.AddWithValue("@login", tbLogin.Text);
                            command.Parameters.AddWithValue("@password", tbPassword.Text);
                            command.Parameters.AddWithValue("@name", tbName.Text);
                            command.ExecuteNonQuery();
                            DataRow nRow = main.cRUDDataSet.Tables[0].NewRow();

                            nRow[1] = tbLogin.Text;
                            nRow[2] = tbPassword.Text;
                            nRow[3] = tbName.Text;
                            main.cRUDDataSet.Tables[0].Rows.Add(nRow);
                            main.dataGridView1.Refresh();

                            tbName.Text = "";
                            tbLogin.Text = "";
                            tbPassword.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }
    }
}
