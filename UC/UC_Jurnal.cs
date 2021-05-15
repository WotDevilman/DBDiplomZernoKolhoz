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
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

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

        private void button1_Click(object sender, EventArgs e)
        {
            using (Jurnal f = new Jurnal()){
                f.ShowDialog();
                this.OnLoad(e);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Items.Dostup = 1;
            Items.listItems.Clear();

            for (int i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                Items.listItems.Add(dataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString());
            }

            using (Jurnal f = new Jurnal())
            {
                f.ShowDialog();
                this.OnLoad(e);
            }
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
                string quest = $"DELETE FROM Журнал WHERE КодЖурнала = {deleteID[i]}";
                db.connect.Open();
                OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                dataCommander.ExecuteNonQuery();
                db.connect.Close();
            }
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectJurnal, db.connect);
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
        private readonly string TemplateFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Documents\\407.docx");
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Document (*.docx) | *.docx";

            if (sfd.ShowDialog() == DialogResult.OK) 
            {
                List<string> data = new List<string>(); 
                List<string> nomer = new List<string>(); 
                List<string> zerno = new List<string>(); 
                List<string> b = new List<string>(); 
                List<string> t = new List<string>(); 
                List<string> n = new List<string>(); 
                List<string> komb = new List<string>(); 
                List<string> vod = new List<string>(); 

                db.connect.Open();
                OleDbCommand cmd = db.connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT Журнал.Дата, Водитель.Номер, Зернопродукция.Культура, Журнал.Брутто, Журнал.Тара, Журнал.Нетто, (Комбайнер.Фамилия &' '& Комбайнер.Имя &' '& Комбайнер.Отчество) as a, (Водитель.Фамилия &' '& Водитель.Имя &' '& Водитель.Отчество) as w FROM Зернопродукция INNER JOIN(Комбайнер INNER JOIN (Водитель INNER JOIN Журнал ON Водитель.КодВодителя = Журнал.КодВодителя) ON Комбайнер.КодКомбайнера = Журнал.КодКомбайнера) ON Зернопродукция.КодЗернопродукции = Журнал.КодЗернопродукции GROUP BY Журнал.Дата, Водитель.Номер, Зернопродукция.Культура, Журнал.Брутто, Журнал.Тара, Журнал.Нетто, Комбайнер.Фамилия, Комбайнер.Имя, Комбайнер.Отчество, Водитель.Фамилия, Водитель.Имя, Водитель.Отчество";
                cmd.ExecuteNonQuery();
                db.connect.Close();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        data.Add(item[0].ToString());
                        nomer.Add(item[1].ToString());
                        zerno.Add(item[2].ToString());
                        b.Add(item[3].ToString());
                        t.Add(item[4].ToString());
                        n.Add(item[5].ToString());
                        komb.Add(item[6].ToString());
                        vod.Add(item[7].ToString());
                    }
                var newpathdoc = sfd.FileName;

                //TODO
                var wordAPP = new Word.Application();
                wordAPP.Visible = false;

                var worddocument = wordAPP.Documents.Open(TemplateFileName);

                var nn = 0;
                Word.Table tab = worddocument.Tables[1];
                for (int i = 2; i <= data.Count+1; i++)
                {
                    tab.Rows.Add(Missing.Value);
                    tab.Cell(i, 1).Range.Text = data[nn];
                    tab.Cell(i, 2).Range.Text = nomer[nn];
                    tab.Cell(i, 3).Range.Text = zerno[nn];
                    tab.Cell(i, 4).Range.Text = b[nn];
                    tab.Cell(i, 5).Range.Text = t[nn];
                    tab.Cell(i, 6).Range.Text = n[nn];
                    tab.Cell(i, 7).Range.Text = komb[nn];
                    tab.Cell(i, 8).Range.Text = vod[nn];
                    nn++;
                }
                worddocument.SaveAs(newpathdoc);
                MessageBox.Show("Документ сохранился");
            }

        }
    }
}
