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
    public partial class MoreVoditel : Form
    {
        public MoreVoditel()
        {
            InitializeComponent();
        }
        database db = new database();
        private void MoreVoditel_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectVoditel, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Items.MoveVoditelID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            Items.MoveVoditel = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " "+ dataGridView1.Rows[e.RowIndex].Cells[3].Value);

            this.Close();
        }
    }
}
