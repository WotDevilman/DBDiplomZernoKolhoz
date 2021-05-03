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
using DBDiplomZernoKolhoz.Forms.More;
using DBDiplomZernoKolhoz.Scripts;


namespace DBDiplomZernoKolhoz.Forms
{
    public partial class Syshka : Form
    {
        public Syshka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Items Items = new Items();
        private void button2_Click(object sender, EventArgs e)
        {
            using (MoreZerno f = new MoreZerno())
            {
                f.ShowDialog();
            }
            More.Text = Items.MoveZernoID.ToString();
        }
        database db = new database();
        private void button3_Click(object sender, EventArgs e)
        {
            if (Items.Dostup != 1)
            {
                string quest = "INSERT INTO Сушка (Дата, КодЗернопродукции, Масса, МассаПослеСушки, Отходы) VALUES('" + Дата.Value.ToShortDateString() + "','" + More.Text + "','" + Масса.Text + "','" + Мааспослесушки.Text + "','" + Отходы.Text + "')";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
            else
            {
                string quest = $"UPDATE Сушка SET Дата = '{Дата.Value.ToShortDateString()}', КодЗернопродукции = '{More.Text}', Масса = '{Масса.Text}',МассаПослеСушки = '{Мааспослесушки.Text}', Отходы = '{Отходы.Text}' where КодСушки = {Items.listItems[0]}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
        }

        private void Syshka_Load(object sender, EventArgs e)
        {
            if (Items.Dostup != 0)
            {
                Дата.Value = Convert.ToDateTime(Items.listItems[1]);
                More.Text = Items.listItems[2];
                Масса.Text = Items.listItems[3];
                Мааспослесушки.Text = Items.listItems[4];
                Отходы.Text = Items.listItems[5];
            }
        }
    }
}
