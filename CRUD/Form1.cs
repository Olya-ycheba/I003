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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.UseColumnTextForButtonValue = true;
            col.Name = "Delete";
            col.Text = "Удалить";
            dataGridView1.Columns.Add(col);

            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.UseColumnTextForButtonValue = true;
            col2.Text = "Редактировать";
            col2.Name = "Edit";
            dataGridView1.Columns.Add(col2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cRUDDataSet.users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.cRUDDataSet.users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cRUDDataSet.users". При необходимости она может быть перемещена или удалена.

        }

        private void btCreateUser_Click(object sender, EventArgs e)
        {
            Form2 af = new Form2();
            af.Owner = this;
            af.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                int id_user = (int)dataGridView1[0, e.RowIndex].Value;
                string login = (string)dataGridView1[1, e.RowIndex].Value;

                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить пользователя  " + login + "?", "Удаление", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {

                    dataGridView1.Rows.RemoveAt(e.RowIndex);

                    using (SqlConnection connection = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = CRUD; Integrated Security = true"))
                    {   
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "delete users where id_user =" + id_user;
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
            {
                Form3 fm3 = new Form3();
                fm3.Owner = this;
                fm3.tbLogin.Text= dataGridView1[1, e.RowIndex].Value.ToString();
                fm3.tbPassword.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                fm3.tbName.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                fm3.tbID.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                fm3.Show();
            }
        }
    }
}
