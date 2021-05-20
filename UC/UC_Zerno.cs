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
    public partial class UC_Zerno : UserControl
    {
        public UC_Zerno()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Zerno_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectZerno, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[1].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Zerno f = new Zerno())
            {
                f.ShowDialog();
                this.OnLoad(e);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Items.Dostup = 1;
            Items.listItems.Clear();

            for (int i = 1; i <= dataGridView1.ColumnCount-1; i++)
            {
                Items.listItems.Add(dataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString());
            }
            using (Zerno f = new Zerno())
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
                    string quest = $"DELETE FROM Зернопродукция WHERE КодЗернопродукции = {deleteID[i]}";
                    db.connect.Open();
                    OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                    dataCommander.ExecuteNonQuery();
                db.connect.Close();
            }
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectZerno, db.connect);
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

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (MoreReproductia f = new MoreReproductia()) {
                f.ShowDialog();
            }
            if(Items.MoveReproductia.Length != 0)
            {
                reprod = $"Зернопродукция.Репродукция = {Items.MoveReproductiaID}";
                More.Text = Items.MoveReproductia;
            }
            else
            {

            }
        }
        string kylt = "";
        string sort = "";
        string reprod = "";
        string vremaposev = "";
        string cena = "";
        string sklad = "";

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> tex = new List<string>();
            List<string> te = new List<string>();
            string quest = "WHERE ";

            tex.Clear();
            te.Clear();

            if (kylt.Length != 0) tex.Add(kylt);
            if (sort.Length != 0) tex.Add(sort);
            if (reprod.Length != 0) tex.Add(reprod);
            if (vremaposev.Length != 0) tex.Add(vremaposev);
            if (cena.Length != 0) tex.Add(cena);
            if (sklad.Length != 0) tex.Add(sklad);

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
                else if (tex.Count == 5)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                    te.Add(" and ");
                    te.Add(tex[2]);
                    te.Add(" and ");
                    te.Add(tex[3]);
                    te.Add(" and ");
                    te.Add(tex[4]);
                }
                else if (tex.Count == 6)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                    te.Add(" and ");
                    te.Add(tex[2]);
                    te.Add(" and ");
                    te.Add(tex[3]);
                    te.Add(" and ");
                    te.Add(tex[4]);
                    te.Add(" and ");
                    te.Add(tex[5]);
                }
                for (int i = 0; i < te.Count; i++)
                {
                    quest += te[i];
                }
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectZerno + quest, db.connect);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }

        private void Культура_TextChanged(object sender, EventArgs e)
        {
            if (Культура.Text != " ")
            {
                kylt = $"Зернопродукция.Культура = '{Культура.Text}'";
            }
            else
            {
                kylt = "";
            }
        }

        private void Сорт_TextChanged(object sender, EventArgs e)
        {
            if (Сорт.Text != " ")
            {
                sort = $"Зернопродукция.Сорт = '{Сорт.Text}'";
            }
            else
            {
                sort = "";
            }
        }

        private void Дата_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Дата.Text != " ")
            {
                vremaposev = $"Зернопродукция.ВремяПосева = '{Дата.Text}'";
            }
            else
            {
                vremaposev = "";
            }
        }

        private void Цена_TextChanged(object sender, EventArgs e)
        {
            if (Цена.Text != " ")
            {
                cena = $"Зернопродукция.Цена = {Цена.Text}";
            }
            else
            {
                cena = "";
            }
        }

        private void Всего_TextChanged(object sender, EventArgs e)
        {
            if (Всего.Text != " ")
            {
                sklad = $"Зернопродукция.[Всего на складе] = {Всего.Text}";
            }
            else
            {
                sklad = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

             kylt = "";
             sort = "";
             reprod = "";
             vremaposev = "";
             cena = "";
             sklad = "";

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectZerno, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
    }
}
