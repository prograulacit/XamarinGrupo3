using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class AbonoRepository
    {
        public static int InsertAndReturn(Abono abono) {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                sqliteConnection.Insert(abono);

                int id= sqliteConnection.Table<Abono>().Where(v => v.COM_ID==0).FirstOrDefault().ABO_ID;
                abono = GetAbonoByID(id);
                abono.COM_ID = Memoria.DinamicValue;
                UpdateAbono(abono);
                return id;
            }
        }
        

        public static bool UpdateAbono(Abono abono)
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int RowsAffected=sqliteConnection.Update(abono);
                if (RowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public static Abono GetAbonoByCompraID(int id)
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                return sqliteConnection.Table<Abono>().Where(v => v.COM_ID == id).FirstOrDefault();
            }

        }
        public static Abono GetAbonoByID(int id)
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                return sqliteConnection.Table<Abono>().Where(v => v.ABO_ID==id).FirstOrDefault();
            }

        }


    }
}
