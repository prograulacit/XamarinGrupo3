using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using ProyectoAndriodCsharp.Objects;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class CompraRepository
    {
        public static void InsertPrueba() {
            Compra compra1 = new Compra { US_ID = Memoria.UsuarioActual.UsuarioId,COM_FECHA_COMPRA=DateTime.Now,COM_ESTADO="Factura",COM_PRECIO_IVA
            =12,COM_PRECIO_TOTAL=1000};
            Compra compra2 = new Compra {
                US_ID = Memoria.UsuarioActual.UsuarioId,
                COM_FECHA_COMPRA = DateTime.Now,
                COM_ESTADO = "Factura",
                COM_PRECIO_IVA
            = 10,
                COM_PRECIO_TOTAL = 3000
            };
            Compra compra3 = new Compra {
                US_ID = Memoria.UsuarioActual.UsuarioId,
                COM_FECHA_COMPRA = DateTime.Now,
                COM_ESTADO = "Factura",
                COM_PRECIO_IVA
            = 11,
                COM_PRECIO_TOTAL = 2000
            };
            IngresarCompra(compra1);
            IngresarCompra(compra2);
            IngresarCompra(compra3);
        }
        public static void CrearTabla() {
            SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            sQLiteConnection.CreateTable<Compra>();
        
        }
        public static bool IngresarCompra(Compra compra)
        {
            bool result = false;
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())){
                int filasAfectadas = sqliteConnection.Insert(compra);
                if (filasAfectadas > 0) { result = true; }
            }
            return result;
        }

        public static bool CarritoAFactura(Compra compra) {
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
        public static bool UpdateCompra(Compra compra){
            bool result = false;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())){
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
        public static IEnumerable<Compra> GetAllFacturas()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Compra> listCompra = sqliteConnection.Table<Compra>().Where(v => v.COM_ESTADO.Equals("Factura"));
            
            return listCompra;
        }
        public static IEnumerable<Compra> GetAllComprasByUserId(int UserId) {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Compra> listCompra = sqliteConnection.Table<Compra>().Where(v => v.US_ID==UserId);
            return listCompra;
        }

        public static bool ExisteCarritoPorUsuario(int UserID)
        {
            bool result = true;
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            if (sqliteConnection.Table<Compra>().Where(v => (v.US_ID == UserID && v.COM_ESTADO.Equals("Activo"))) == null) { result = false; }
            return result;
        }





    }
}
