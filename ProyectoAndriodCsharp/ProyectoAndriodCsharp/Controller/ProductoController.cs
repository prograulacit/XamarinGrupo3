using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class ProductoController
    {
       
        public static bool IngresarProducto(ProductoModel productoModel) {
            bool result=false;
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                int filasAfectadas=sqliteConnection.Insert(productoModel);
                if (filasAfectadas > 0) { result = true; }
            }
            return result;
        }

        public static bool EliminarProducto(ProductoModel productoModel) {
            bool result = false;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                int filasAfectadas=sQLiteConnection.Delete(productoModel);
                if (filasAfectadas > 0) {
                    result = true;
                }
            }
                return result;        
        }

        public static bool UpdateProducto(ProductoModel productoModel) {
            bool result = false;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sQLiteConnection.Update(productoModel);
                if (filasAfectadas > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public static ProductoModel GetProductoByID(int ID) {
            using (SQLiteConnection sQLiteConnection= new SQLiteConnection(DataConnection.GetConnectionPath())) {
                return sQLiteConnection.Table<ProductoModel>().Where(i => i.PRO_ID == ID).FirstOrDefault();
            }
        }
        public static IEnumerable<ProductoModel> GetAllProductos() {
            using (SQLiteConnection sQLiteConnection=new SQLiteConnection(DataConnection.GetConnectionPath())) {
                return  sQLiteConnection.Table<ProductoModel>();
            }
        }



    }
}
