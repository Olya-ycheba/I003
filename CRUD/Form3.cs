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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btEdit_Click(object sender, EventArgs e)
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
                            SqlCommand command = new SqlCommand("update users set login='" + tbLogin.Text + "', password = '" + tbPassword.Text + "', name = '" + tbName.Text+ "' where id_user =" + tbID.Text , connection);
                            command.ExecuteNonQuery();

                            main.usersTableAdapter.Fill(main.cRUDDataSet.users);
                            main.dataGridView1.Refresh();
                            Close();
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
