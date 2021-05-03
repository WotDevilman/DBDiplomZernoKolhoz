﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiplomZernoKolhoz.Scripts
{
    class database
    {

        //More
        public string selectReproductia = "SELECT Репродукция.КодРепродукции, Репродукция.Значение FROM Репродукция";
        public string selectSclad = "SELECT Склад.КодСклада, Склад.Наименование, Склад.Расположение FROM Склад";
        public string selectPole = "SELECT Поле.КодПоля, Поле.Наименование, Поле.Месторасположение, Поле.Площадь FROM Поле";

        //Основыне
        public string selectVoditel = "SELECT Водитель.КодВодителя, (Водитель.Фамилия &' '& Водитель.Имя &' '& Водитель.Отчество) AS ФИО, Водитель.Марка, Водитель.Номер, Водитель.Тара FROM Водитель ";
        public string selectSyshka = "SELECT Сушка.КодСушки, Сушка.Дата, Зернопродукция.КодЗернопродукции, Сушка.Масса, Сушка.МассаПослеСушки, Сушка.Отходы FROM Зернопродукция INNER JOIN Сушка ON Зернопродукция.КодЗернопродукции = Сушка.КодЗернопродукции ";
        public string selectZerno = "SELECT Зернопродукция.КодЗернопродукции, Зернопродукция.Культура, Зернопродукция.Сорт, Репродукция.Значение,Зернопродукция.ВремяПосева, Зернопродукция.Цена, Зернопродукция.[Всего на складе] FROM Репродукция INNER JOIN Зернопродукция ON Репродукция.КодРепродукции = Зернопродукция.Репродукция ";
        public string selectKombain = "SELECT Комбайнер.КодКомбайнера, (Комбайнер.Фамилия &' '& Комбайнер.Имя &' '& Комбайнер.Отчество) AS ФИО, Комбайнер.Марка, Комбайнер.Номер FROM Комбайнер ";
        public string selectJurnal = "SELECT Журнал.КодЖурнала, Журнал.Дата, Зернопродукция.Культура, Склад.Наименование, Журнал.Брутто, Журнал.Тара, Журнал.Нетто, (Водитель.Фамилия &' '& Водитель.Имя &' '& Водитель.Отчество) as Водитель, (Комбайнер.Фамилия &' '& Комбайнер.Имя &' '& Комбайнер.Отчество) as Комбайнер, Поле.Наименование, Поле.Месторасположение FROM Поле INNER JOIN(Комбайнер INNER JOIN (Водитель INNER JOIN (Склад INNER JOIN (Зернопродукция INNER JOIN Журнал ON Зернопродукция.КодЗернопродукции = Журнал.КодЗернопродукции) ON Склад.КодСклада = Журнал.КодСклада) ON Водитель.КодВодителя = Журнал.КодВодителя) ON Комбайнер.КодКомбайнера = Журнал.КодКомбайнера) ON Поле.КодПоля = Журнал.КодПоля ";

        public OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Zerno.mdb");

    }
}
