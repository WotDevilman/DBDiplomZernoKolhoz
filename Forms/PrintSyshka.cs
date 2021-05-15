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
using Word = Microsoft.Office.Interop.Word;

namespace DBDiplomZernoKolhoz.Forms
{
    public partial class PrintSyshka : Form
    {
        public PrintSyshka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        database db = new database();
        private void PrintSyshka_Load(object sender, EventArgs e)
        {
            
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Зернопродукция.Культура FROM Зернопродукция INNER JOIN Сушка ON Зернопродукция.КодЗернопродукции = Сушка.КодЗернопродукции GROUP BY Зернопродукция.Культура;";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                listBox1.Items.Add(item[0].ToString());
            }
            db.connect.Close();


        }

        private void ReplateWordDocument(string stupToReplate, string text, Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupToReplate, ReplaceWith: text);
        }

        private readonly string TemplateFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Documents\\410.docx");
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Document (*.docx) | *.docx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    double massa = 0;
                    double massot = 0;
                    double ot = 0;

                    db.connect.Open();
                    OleDbCommand cmd = db.connect.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT Sum(Сушка.Масса) AS [Sum-Масса], Sum(Сушка.МассаПослеСушки) AS [Sum-МассаПослеСушки], Sum(Сушка.Отходы) AS [Sum-Отходы] FROM Зернопродукция INNER JOIN Сушка ON Зернопродукция.КодЗернопродукции = Сушка.КодЗернопродукции GROUP BY Зернопродукция.Культура HAVING Зернопродукция.Культура = '{listBox1.SelectedItem.ToString()}'";
                    cmd.ExecuteNonQuery();
                db.connect.Close();
                DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        massa = Convert.ToDouble(item[0]);
                        massot = Convert.ToDouble(item[1]);
                        ot = Convert.ToDouble(item[2]);
                    }

                    var newpathdoc = sfd.FileName;

                    //TODO
                    var wordAPP = new Word.Application();
                    wordAPP.Visible = false;

                    var worddocument = wordAPP.Documents.Open(TemplateFileName);

                    ReplateWordDocument("{Культура}", listBox1.SelectedItem.ToString(), worddocument);
                    ReplateWordDocument("{Культура}", listBox1.SelectedItem.ToString(), worddocument);
                    ReplateWordDocument("{масса}", massa.ToString(), worddocument);
                    ReplateWordDocument("{массапослесушки}", massot.ToString(), worddocument);
                    ReplateWordDocument("{отходы}", ot.ToString(), worddocument);
                    worddocument.SaveAs(newpathdoc);
                    MessageBox.Show("Документ сохранился");
                }

        }
    }
}
