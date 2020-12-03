using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using SQLite;
using System.Collections.Generic;

namespace ProyectoAndriodCsharp.Controller
{
    public class CompraProductosRepository
    {
        public static void CrearTabla()
        {
            SQLiteConnection sQLiteConnection = 
                new SQLiteConnection(DataConnection.GetConnectionPath());
            sQLiteConnection.CreateTable<Compra>();
        }

        public static bool IngresarCP(CompraProductos cp)
        {
            bool result = false;
            using (SQLiteConnection sqliteConnection = 
                new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sqliteConnection.Insert(cp);
                if (filasAfectadas > 0) { result = true; }
            }
            return result;
        }

        public static bool EliminarCP(CompraProductos cp)
        {
            bool result = false;
            using (SQLiteConnection sQLiteConnection = 
                new SQLiteConnection(DataConnection.GetConnectionPath()))
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
            using (SQLiteConnection sQLiteConnection = 
                new SQLiteConnection(DataConnection.GetConnectionPath()))
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
            using (SQLiteConnection sQLiteConnection = 
                new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                return sQLiteConnection.Table<CompraProductos>()
                    .Where(i => i.COMP_ID == ID).FirstOrDefault();
            }
        }

        public static IEnumerable<CompraProductos> GetAllCPByCompraID(int ID)
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            return sqliteConnection.Table<CompraProductos>()
                .Where(v=>v.COM_ID==ID);
        }

        public static IEnumerable<CompraProductos> GetAllCompraProductos()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<CompraProductos> lista_CompraProductos = sqliteConnection.Table<CompraProductos>();
            return lista_CompraProductos;
        }
    }
}
