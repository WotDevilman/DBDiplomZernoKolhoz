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

namespace DBDiplomZernoKolhoz.Forms
{
    public partial class Kombain : Form
    {
        public Kombain()
        {
            InitializeComponent();
        }
        database db = new database();
        Items Items = new Items();
        private void button3_Click(object sender, EventArgs e)
        {
            if (Items.Dostup != 1)
            {
                string quest = "INSERT INTO Комбайнер (Фамилия, Имя, Отчество, Марка, Номер) VALUES('" + Фамилия.Text + "','" + Имя.Text + "','" + Отчество.Text + "','" + Марка.Text + "','" + Номер.Text + "')";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
            else
            {
                string quest = $"UPDATE Комбайнер SET Фамилия = '{Фамилия.Text}', Имя = '{Имя.Text}', Отчество = '{Отчество.Text}',Марка = '{Марка.Text}', Номер = '{Номер.Text}' where КодКомбайнера = {Items.listItems[0]}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Kombain_Load(object sender, EventArgs e)
        {
            if (Items.Dostup != 0)
            {
                label1.Text = "Запись комбайнера";

                Фамилия.Text = Items.listItems[1].Split(' ')[0];
                Имя.Text = Items.listItems[1].Split(' ')[1];
                Отчество.Text = Items.listItems[1].Split(' ')[2];
                Марка.Text = Items.listItems[2];
                Номер.Text = Items.listItems[3];
            }
            else label1.Text = "Изменения комбайнера";
        }
    }
}
