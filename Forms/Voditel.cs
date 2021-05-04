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
    public partial class Voditel : Form
    {
        public Voditel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        database db = new database();

        private void button3_Click(object sender, EventArgs e)
        {
            if (Items.Dostup != 1)
            {
                string quest = "INSERT INTO Водитель (Фамилия, Имя, Отчество, Марка, Номер, Тара) VALUES('" + Фамилия.Text + "','" + Имя.Text + "','" + Отчество.Text + "','" + Марка.Text + "','" + Номер.Text + "', '"+Тара.Text+"')";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
            else
            {
                string quest = $"UPDATE Водитель SET Фамилия = '{Фамилия.Text}', Имя = '{Имя.Text}', Отчество = '{Отчество.Text}',Марка = '{Марка.Text}', Номер = '{Номер.Text}', Тара = '{Тара.Text}' where КодВодителя = {Items.listItems[0]}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
            }
        }

        private void Voditel_Load(object sender, EventArgs e)
        {
            if (Items.Dostup != 0)
            {
                Фамилия.Text = Items.listItems[1].Split(' ')[0];
                Имя.Text = Items.listItems[1].Split(' ')[1];
                Отчество.Text = Items.listItems[1].Split(' ')[2];
                Марка.Text = Items.listItems[2];
                Номер.Text = Items.listItems[3];
                Тара.Text = Items.listItems[4];
            }
        }
    }
}
