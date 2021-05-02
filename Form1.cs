using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DBDiplomZernoKolhoz.UC;

namespace DBDiplomZernoKolhoz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void addControll(UserControl uc)
        {
            panelUC.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelUC.Controls.Add(uc);
            uc.BringToFront();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UC_Jurnal uc = new UC_Jurnal();

            addControll(uc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UC_Syshka uc = new UC_Syshka();

            addControll(uc);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UC_Zerno uc = new UC_Zerno();

            addControll(uc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UC_Kombain uc = new UC_Kombain();

            addControll(uc);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UC_Voditel uc = new UC_Voditel();

            addControll(uc);
        }
    }
}
