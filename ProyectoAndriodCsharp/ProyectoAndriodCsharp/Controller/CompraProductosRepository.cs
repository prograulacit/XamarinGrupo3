using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class CompraProductosRepository
    {
        public static void CrearTabla()
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            sQLiteConnection.CreateTable<Compra>();
        }
        public static bool IngresarCP(CompraProductos cp)
        {
            bool result = false;
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sqliteConnection.Insert(cp);
                if (filasAfectadas > 0) { result = true; }
            }
            return result;
        }

        public static bool EliminarCP(CompraProductos cp)
        {
            bool result = false;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sQLiteConnection.Delete(cp);
                if (filasAfectadas > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        
        public static bool UpdateCP(Compra compra)
        {
            bool result = false;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sQLiteConnection.Update(compra);
                if (filasAfectadas > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        public static CompraProductos GetCPByID(int ID)
        {
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                return sQLiteConnection.Table<CompraProductos>().Where(i => i.COMP_ID == ID).FirstOrDefault();
            }
        }
        public static IEnumerable<CompraProductos> GetAllCPByCompraID(int ID)
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            return sqliteConnection.Table<CompraProductos>().Where(v=>v.COM_ID==ID);
        }
        


    }
}
