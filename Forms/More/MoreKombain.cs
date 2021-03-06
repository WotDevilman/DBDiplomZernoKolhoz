using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBDiplomZernoKolhoz.Scripts;


namespace DBDiplomZernoKolhoz.Forms.More
{
    public partial class MoreKombain : Form
    {
        public MoreKombain()
        {
            InitializeComponent();
        }
        database db = new database();
        private void MoreKombain_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectKombain, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Items.MoveKombainID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            Items.MoveKombain = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
