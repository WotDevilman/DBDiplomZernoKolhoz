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

using DBDiplomZernoKolhoz.Forms.More;

namespace DBDiplomZernoKolhoz.UC
{
    public partial class UC_Syshka : UserControl
    {
        public UC_Syshka()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Syshka_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectSyshka, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[1].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Syshka f = new Syshka())
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
            using (Syshka f = new Syshka())
            {
                f.ShowDialog();
                this.OnLoad(e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
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
                string quest = $"DELETE FROM Сушка WHERE КодСушки = {deleteID[i]}";
                db.connect.Open();
                OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                dataCommander.ExecuteNonQuery();
                db.connect.Close();
            }
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectSyshka, db.connect);
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

        private void button4_Click(object sender, EventArgs e)
        {
            using (PrintSyshka f = new PrintSyshka())
            {
                f.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        string zerno = "";
        string massa = "";
        string massaposle = "";
        string othod = "";
        private void button5_Click(object sender, EventArgs e)
        {
            List<string> tex = new List<string>();
            List<string> te = new List<string>();
            string quest = "WHERE ";

            tex.Clear();
            te.Clear();

            if (zerno.Length != 0) tex.Add(zerno);
            if (massa.Length != 0) tex.Add(massa);
            if (massaposle.Length != 0) tex.Add(massaposle);
            if (othod.Length != 0) tex.Add(othod);
            if (tex.Count() != 0)
            {
                if (tex.Count == 1)
                {
                    te.Add(tex[0]);
                }
                if (tex.Count == 2)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                }
                else if (tex.Count == 3)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                    te.Add(" and ");
                    te.Add(tex[2]);
                }
                else if (tex.Count == 4)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                    te.Add(" and ");
                    te.Add(tex[2]);
                    te.Add(" and ");
                    te.Add(tex[3]);
                }
                for (int i = 0; i < te.Count; i++)
                {
                    quest += te[i];
                }
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectSyshka + quest, db.connect);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }

        private void Масса_TextChanged(object sender, EventArgs e)
        {
            if (Масса.Text != " ")
            {
                massa = $"Сушка.Масса = {Масса.Text}";
            }
            else
            {
                massa = "";
            }
        }

        private void Мааспослесушки_TextChanged(object sender, EventArgs e)
        {
            if (Мааспослесушки.Text != " ")
            {
                massaposle = $"Сушка.МассаПослеСушки = {Мааспослесушки.Text}";
            }
            else
            {
                massaposle = "";
            }
        }

        private void Отходы_TextChanged(object sender, EventArgs e)
        {
            if (Отходы.Text != " ")
            {
                othod = $"Сушка.Отходы = {Отходы.Text}";
            }
            else
            {
                othod = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (MoreZerno f = new MoreZerno()) {
                f.ShowDialog();
            }
            if(Items.MoveZerno.Length != 0)
            {
                zerno = $"Сушка.КодЗернопродукции = {Items.MoveZernoID}";
                More.Text = Items.MoveZerno;
            }
            else
            {
                zerno = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            zerno = "";
            othod = "";
            massa = "";
            massaposle = "";
        }
    }
}
