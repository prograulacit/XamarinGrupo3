using ProyectoAndriodCsharp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using ProyectoAndriodCsharp.Objects;

namespace ProyectoAndriodCsharp.Controller
{
    public class CompraRepository
    {
        public static void InsertarPrueba()
        {
            DeleteAllRows();
            Compra c1 = new Compra { US_ID = 1, COM_ESTADO = "Factura", COM_FECHA_COMPRA = DateTime.Now, COM_PRECIO_IVA = 100, COM_PRECIO_TOTAL = 2000 };
            Compra c2 = new Compra { US_ID = 1, COM_ESTADO = "Factura", COM_FECHA_COMPRA = DateTime.Now, COM_PRECIO_IVA = 200, COM_PRECIO_TOTAL = 3000 };
            Compra c3 = new Compra { US_ID = 1, COM_ESTADO = "Factura", COM_FECHA_COMPRA = DateTime.Now, COM_PRECIO_IVA = 300, COM_PRECIO_TOTAL = 4000 };
            IngresarCompra(c1);
            IngresarCompra(c2);
            IngresarCompra(c3);
        }

        public static void CrearTabla()
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            sQLiteConnection.CreateTable<Compra>();
        }

        public static void DeleteAllRows()
        {
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                sQLiteConnection.DeleteAll<Compra>();
            }
        }

        public static bool IngresarCompra(Compra compra)
        {
            bool result = false;
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sqliteConnection.Insert(compra);
                if (filasAfectadas > 0) { result = true; }
            }
            return result;
        }

        // Guarda elemento en la base de datos y retorna su ID de la base
        // de datos.
        public static int IngresarCompraRetornarID(Compra compra)
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sqliteConnection.Insert(compra);
                if (filasAfectadas > 0) return compra.COM_ID;
            }
            return -1;
        }

        public static bool CarritoAFactura(Compra compra)
        {
            bool result = false;
            compra.COM_ESTADO = "Factura";
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            sqliteConnection.Update(compra);
            return result;
        }

        public static bool EliminarCompra(Compra compra)
        {
            bool result = false;
            compra.COM_ESTADO = "Inactivo";
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

        public static bool UpdateCompra(Compra compra)
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

        public static Compra GetCompraByID(int ID)
        {
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                return sQLiteConnection.Table<Compra>().Where(i => i.COM_ID == ID).FirstOrDefault();
            }
        }

        // Retorna todos los registros de compra.
        public static IEnumerable<Compra> GetAllCompra()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Compra> lista_compra = sqliteConnection.Table<Compra>();
            return lista_compra;
        }

        public static IEnumerable<Compra> GetAllFacturas()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Compra> listCompra = sqliteConnection.Table<Compra>().Where(v => v.COM_ESTADO.Equals("Factura"));

            return listCompra;
        }

        public static IEnumerable<Compra> GetAllComprasByUserId(int UserId)
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Compra> listCompra = sqliteConnection.Table<Compra>().Where(v => v.US_ID == UserId);
            return listCompra;
        }

        public static bool ExisteCarritoPorUsuario(int UserID)
        {
            bool result = true;
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            if (sqliteConnection.Table<Compra>()
                .Where(v => (v.US_ID == UserID && v.COM_ESTADO.Equals("Activo"))) == null) result = false;
            return result;
        }
    }
}
