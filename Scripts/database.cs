using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiplomZernoKolhoz.Scripts
{
    class database
    {
        public string selectJurnal = "SELECT Журнал.КодЖурнала, Журнал.Дата, Зернопродукция.Культура, Склад.Наименование, Журнал.Брутто, Журнал.Тара, Журнал.Нетто, (Водитель.Фамилия &' '& Водитель.Имя &' '& Водитель.Отчество) as Водитель, (Комбайнер.Фамилия &' '& Комбайнер.Имя &' '& Комбайнер.Отчество) as Комбайнер, Поле.Месторасположение FROM Поле INNER JOIN(Комбайнер INNER JOIN (Водитель INNER JOIN (Склад INNER JOIN (Зернопродукция INNER JOIN Журнал ON Зернопродукция.КодЗернопродукции = Журнал.КодЗернопродукции) ON Склад.КодСклада = Журнал.КодСклада) ON Водитель.КодВодителя = Журнал.КодВодителя) ON Комбайнер.КодКомбайнера = Журнал.КодКомбайнера) ON Поле.КодПоля = Журнал.КодПоля;";

        public OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Zerno.mdb");

    }
}
