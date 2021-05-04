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
using DBDiplomZernoKolhoz.Forms;

namespace DBDiplomZernoKolhoz.UC
{
    public partial class UC_Voditel : UserControl
    {
        public UC_Voditel()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Voditel_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectVoditel, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[1].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(Voditel f = new Voditel())
            {
                f.ShowDialog();
                this.OnLoad(e);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Items.Dostup = 1;
            Items.listItems.Clear();

            for (int i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                Items.listItems.Add(dataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString());
            }
            using (Voditel f = new Voditel())
            {
                f.ShowDialog();
                this.OnLoad(e);
            }
        }
    }
}
