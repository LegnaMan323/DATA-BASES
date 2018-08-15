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
    public partial class TablaPonys : Form
    {
        DBConnection Conectoooo;
        public TablaPonys(DBConnection Connect1)
        {
            Conectoooo = Connect1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Tabla = new Form1();
            Conectoooo.SelectQuery("INSERT INTO entrenadores VALUES(NULL, '" + textBox1.Text + "' )");
            this.Hide();
            Tabla.Show();
        }
    }
}
