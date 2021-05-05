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

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> deleteID = new List<int>();
            deleteID.Clear();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                    deleteID.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value));
            }
            for (int i = 0; i < deleteID.Count; i++)
            {
                string quest = $"DELETE FROM Водитель WHERE КодВодителя = {deleteID[i]}";
                db.connect.Open();
                OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                dataCommander.ExecuteNonQuery();
                db.connect.Close();
            }
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectVoditel, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    bool TFalse = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = !TFalse;
                }
            }
        }
    }
}
