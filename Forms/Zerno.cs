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
            if (Items.Dostup != 1)
            {
                string quest = "INSERT INTO Зернопродукция (Культура, Сорт, Репродукция, ВремяПосева, Цена, [Всего на складе]) VALUES('" + Культура.Text + "','" + Сорт.Text + "','" + IDReprodyctia + "','" + Дата.Value.ToShortDateString() + "','" + Цена.Text + "'," + Всего.Text + ")";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
            else
            {
                string quest = $"UPDATE ОсновныеПоказатели SET Предприятия = '{idpredpriatia}', ОсновнойВидПродукции = '{idvidproducta}', ЧислоРаботников = '{TRabot.Text}', Прибыль = '{TPribil.Text}' where КодОсновныеПоказатели = {ID}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
        }
    }
}
