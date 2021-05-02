﻿using System;
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
    public partial class UC_Kombain : UserControl
    {
        public UC_Kombain()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Kombain_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectKombain, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns[1].Visible = false;
        }
    }
}
