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
namespace DBDiplomZernoKolhoz.UC
{
    public partial class UC_Jurnal : UserControl
    {
        public UC_Jurnal()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Jurnal_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectJurnal, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[1].Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
        }
    }
}
