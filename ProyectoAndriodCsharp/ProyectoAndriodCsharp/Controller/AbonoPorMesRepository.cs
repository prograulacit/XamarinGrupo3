using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class AbonoPorMesRepository
    {
        public static bool InsertarAbonoPorMes(AbonoPorMes abonoPorMes) {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                int rowsAffected=sqliteConnection.Insert(abonoPorMes);
                if (rowsAffected>0) { return true; }
                else { return false; }
            }
        }
    }
}
