using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class CompraRepository
    {
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
            if (sqliteConnection.Table<Compra>().Where(v => (v.US_ID == UserID && v.COM_ESTADO.Equals("Carrito"))) == null) { result = false; }
            return result;
        }





    }
}
