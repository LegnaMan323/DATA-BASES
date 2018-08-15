using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_bases_p1
{
    public partial class Form1 : Form
    {

        DBConnection Connect1;
        bool ponys;

        public Form1()
        {
            ponys = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            Connect1 = new DBConnection("138.68.20.16", "arodriguez_pfinal", "arodriguez", "1234567890");

            if (Connect1.Connect(ref errorMsg))
            {

            }
            else
            {
                MessageBox.Show("Conection Failure: " + errorMsg);
                Close();
            }
        }


        private void UpdateTable()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            if (ponys)
            {
                DataTable PONYS = Connect1.SelectQuery("Select * from PONYS");
                dataGridView1.DataSource = PONYS;

            }
       
        }

        private void btn_ADD_Click(object sender, EventArgs e)
        {
            if (ponys)
            {
                TablaPonys addEntrenadores = new TablaPonys(Connect1);
                this.Hide();
                addEntrenadores.Show();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable PONYS = dataGridView1.DataSource as DataTable;

            string query = "UPDATE " + "entrenadores" + " SET "
               + PONYS.Columns[e.ColumnIndex].ColumnName + " = '"
               + PONYS.Rows[e.RowIndex][e.ColumnIndex]
               + "' WHERE id = " + PONYS.Rows[e.RowIndex][0];
            bool success = Connect1.ExecuteQuery(query);
            if (!success) MessageBox.Show("Error desconocido en el query");
            UpdateTable();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var senderGrid = sender as DataGridView;
            if (ponys)
            {
                Connect1.ExecuteQuery("Delete from entrenadores where id =" + e.Row.Cells[0].Value.ToString());
                UpdateTable();
            }


           e.Cancel = true;
        }

        private void btn_PONYS_Click(object sender, EventArgs e)
        {
            ponys = true;
            UpdateTable();
        }
    }
}
