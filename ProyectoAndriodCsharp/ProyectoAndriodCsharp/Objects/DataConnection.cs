using ProyectoAndriodCsharp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProyectoAndriodCsharp.Objects
{
    public class DataConnection
    {
        public static string GetConnectionPath() {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                ,"XamarinDatabase.db");
        }

        public static void CreateTables() {
            SQLiteConnection sqLiteConnection = new SQLiteConnection(GetConnectionPath());
            sqLiteConnection.CreateTable<Usuario>();
            sqLiteConnection.CreateTable<Compra>();
            sqLiteConnection.CreateTable<Producto>();
            sqLiteConnection.CreateTable<CompraProductos>();  //BORRAR UNIQUE EN EL MODEL COMPRAPRODUCTOS.CS PARA QUE SIRVA POR MIENTRAS
            sqLiteConnection.CreateTable<Abono>();
            sqLiteConnection.CreateTable<AbonoPorMes>();
        }

        public static void DropTables() {
            SQLiteConnection sqLiteConnection = new SQLiteConnection(GetConnectionPath());
            sqLiteConnection.DropTable<Usuario>();
            sqLiteConnection.DropTable<Compra>();
            sqLiteConnection.DropTable<Producto>();
            //sqLiteConnection.DropTable<Abono>();
            //sqLiteConnection.DropTable<AbonoPorMes>();
            sqLiteConnection.DropTable<CompraProductos>();
        }
    }
}
