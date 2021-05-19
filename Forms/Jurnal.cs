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
    public partial class Jurnal : Form
    {
        public Jurnal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int IDVoditel = 0;
        int IDKombain = 0;
        int IDZerno = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            using (MoreZerno f = new MoreZerno())
            {
                f.ShowDialog();
            }
            Зерно.Text = Items.MoveZerno;
            IDZerno = Items.MoveZernoID;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (MoreSclad f = new MoreSclad())
            {
                f.ShowDialog();
            }
            Склад.Text = Items.MoveSclad;
            IDSclad = Items.MoveScladID;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (MoreVoditel f = new MoreVoditel())
            {
                f.ShowDialog();
            }
            Водитель.Text = Items.MoveVoditel;
            IDVoditel = Items.MoveVoditelID;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (MoreKombain f = new MoreKombain())
            {
                f.ShowDialog();
            }
            Комбайнер.Text = Items.MoveKombain;
            IDKombain = Items.MoveKombainID;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (MorePole f = new MorePole())
            {
                f.ShowDialog();
            }
            Поле.Text = Items.MovePole;
            IDPole = Items.MovePoleID;
        }
        int IDPole = 0;
        int IDSclad = 0;
        database db = new database();
        private void button3_Click(object sender, EventArgs e)
        {
            if (Items.Dostup != 1)
            {
                string quest = "INSERT INTO Журнал (Дата, КодЗернопродукции, КодСклада, Брутто, Тара,Нетто,КодВодителя,КодКомбайнера, КодПоля) VALUES('" + Дата.Value.ToShortDateString() + "','" + Зерно.Text + "','" + IDSclad + "','" + Брутто.Text + "','" + Тара.Text + "','" + Нетто.Text + "','" + IDVoditel + "','" + IDKombain + "','" + IDPole + "')";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
            else
            {
                string quest = $"UPDATE Журнал SET Дата = '{Дата.Value.ToShortDateString()}', КодЗернопродукции = '{IDZerno}', КодСклада = '{IDSclad}',Брутто = '{Брутто.Text}', Тара = '{Тара.Text}', Нетто = '{Нетто.Text}', КодВодителя = '{IDVoditel}', КодКомбайнера = '{IDKombain}', КодПоля = '{IDPole}' where КодЖурнала = {Items.listItems[0]}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
        }

        private void Jurnal_Load(object sender, EventArgs e)
        {
            if (Items.Dostup != 0)
            {
                label1.Text = "Запись журнала";

                Дата.Value = Convert.ToDateTime(Items.listItems[1]);
                Зерно.Text = Items.listItems[2];
                Склад.Text = Items.listItems[3];
                Брутто.Text = Items.listItems[4];
                Тара.Text = Items.listItems[5];
                Нетто.Text = Items.listItems[6];
                Водитель.Text = Items.listItems[7];
                Комбайнер.Text = Items.listItems[8];
                Поле.Text = Items.listItems[9];

                db.connect.Open();

                OleDbCommand cmd = db.connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT Журнал.КодСклада, Журнал.КодПоля, Журнал.КодЗернопродукции FROM Журнал WHERE Журнал.КодЖурнала = {Items.listItems[0]}";
                cmd.ExecuteNonQuery();
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IDSclad = Convert.ToInt32(reader[0]);
                    IDPole = Convert.ToInt32(reader[1]);
                    IDZerno = Convert.ToInt32(reader[2]);
                }
                reader.Close();

                cmd = db.connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT КодВодителя FROM Водитель where Фамилия = '" + Items.listItems[7].Split(' ')[0] + "' and Имя = '" + Items.listItems[7].Split(' ')[1] + "' and Отчество = '" + Items.listItems[7].Split(' ')[2] + "'";
                cmd.ExecuteNonQuery();
                IDVoditel = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.CommandText = "SELECT КодКомбайнера FROM Комбайнер where Фамилия = '" + Items.listItems[8].Split(' ')[0] + "' and Имя = '" + Items.listItems[8].Split(' ')[1] + "' and Отчество = '" + Items.listItems[8].Split(' ')[2] + "'";
                cmd.ExecuteNonQuery();
                IDKombain = Convert.ToInt32(cmd.ExecuteScalar());
                db.connect.Close();
            }
            else label1.Text = "Изменения журнала";
        }
    }
}
