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
                int RowsAffected = sqliteConnection.Insert(abono);
                return sqliteConnection.Table<Abono>().Where(v => v.ABO_CANTIDAD_MENSUAL == abono.ABO_CANTIDAD_MENSUAL && v.ABO_CANTIDAD_DE_MESES == abono.ABO_CANTIDAD_DE_MESES && v.ABO_RESTANTE == abono.ABO_RESTANTE && v.COM_ID==abono.COM_ID).FirstOrDefault().ABO_ID;
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


    }
}
