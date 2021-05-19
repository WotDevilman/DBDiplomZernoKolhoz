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
    public partial class Zerno : Form
    {
        public Zerno()
        {
            InitializeComponent();
        }
        int IDReprodyctia = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            Items Items = new Items();
            using(MoreReproductia f = new MoreReproductia())
            {
                f.ShowDialog();
            }
            if(Items.MoveReproductia.Length != 0)
            {
                IDReprodyctia = Items.MoveReproductiaID;
                More.Text = Items.MoveReproductia;
            }
        }
        database db = new database();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Items.Dostup == 0)
            {
                string quest = "INSERT INTO Зернопродукция (Культура, Сорт, Репродукция, ВремяПосева, Цена, [Всего на складе]) VALUES('" + Культура.Text + "','" + Сорт.Text + "','" + IDReprodyctia + "','" + Дата.Text + "','" + Цена.Text + "'," + Всего.Text + ")";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
            else
            {
                string quest = $"UPDATE Зернопродукция SET Культура = '{Культура.Text}', Сорт = '{Сорт.Text}', Репродукция = '{IDReprodyctia}',ВремяПосева = '{Дата.Text}', Цена = '{Цена.Text}', [Всего на складе] = '{Всего.Text}' where КодЗернопродукции = {Items.listItems[0]}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
        }

        private void Zerno_Load(object sender, EventArgs e)
        {
            if (Items.Dostup != 0)
            {
                label1.Text = "Запись зерна";

                Культура.Text = Items.listItems[1];
                Сорт.Text = Items.listItems[2];
                More.Text = Items.listItems[3];
                Дата.Text = Items.listItems[4];
                Цена.Text = Items.listItems[5];
                Всего.Text = Items.listItems[6];

                db.connect.Open();
                OleDbCommand cmd = db.connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT КодРепродукции FROM Репродукция where Значение = '" + Items.listItems[3] + "'";
                cmd.ExecuteNonQuery();
                IDReprodyctia = Convert.ToInt32(cmd.ExecuteScalar());
                db.connect.Close();
            }
            else label1.Text = "Изменения зерна";
        }
    }
}
