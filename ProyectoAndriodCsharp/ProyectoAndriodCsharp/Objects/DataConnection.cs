using ProyectoAndriodCsharp.Model;
using SQLite;
using System;
using System.IO;

namespace ProyectoAndriodCsharp.Models
{
    public class DataConnection
    {
        public static string GetConnectionPath() {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                ,"XamarinDatabase.db");
        }

        public static void CreateTables() {
            SQLiteConnection sqLiteConnection = new SQLiteConnection(GetConnectionPath());
            sqLiteConnection.CreateTable<UsuarioModel>();
            //sqLiteConnection.CreateTable<Usuario>();
            //sqLiteConnection.CreateTable<Compra>();
            //sqLiteConnection.CreateTable<Producto>();
            //sqLiteConnection.CreateTable<Abono>();
            //sqLiteConnection.CreateTable<AbonoPorMes>();
            //sqLiteConnection.CreateTable<CompraProductos>();
        }

        public static void DropTables() {
            SQLiteConnection sqLiteConnection = new SQLiteConnection(GetConnectionPath());
            sqLiteConnection.DropTable<UsuarioModel>();
            //sqLiteConnection.DropTable<Usuario>();
            //sqLiteConnection.DropTable<Compra>();
            //sqLiteConnection.DropTable<Producto>();
            //sqLiteConnection.DropTable<Abono>();
            //sqLiteConnection.DropTable<AbonoPorMes>();
            //sqLiteConnection.DropTable<CompraProductos>();
        }
    }
}
