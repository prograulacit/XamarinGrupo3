using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class ProductoController
    {
        
        public static void InsertarPrueba()
        {
            Producto product1 = new Producto { PRO_NOMBRE = "string1", PRO_DESCRIPCION = "string1", PRO_PRECIO = 12 };
            Producto product2 = new Producto { PRO_NOMBRE = "string2", PRO_DESCRIPCION = "string2", PRO_PRECIO = 22 };
            Producto product3 = new Producto { PRO_NOMBRE = "string3", PRO_DESCRIPCION = "string3", PRO_PRECIO = 32 };
            ProductoController.IngresarProducto(product1);
            ProductoController.IngresarProducto(product2);
            ProductoController.IngresarProducto(product3);
        }

        public static bool IngresarProducto(Producto productoModel) {
            bool result=false;
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                int filasAfectadas=sqliteConnection.Insert(productoModel);
                if (filasAfectadas > 0) { result = true; }
            }
            return result;
        }

        public static bool EliminarProducto(Producto productoModel) {
            bool result = false;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                int filasAfectadas=sQLiteConnection.Delete(productoModel);
                if (filasAfectadas > 0) {
                    result = true;
                }
            }
                return result;        
        }

        public static bool UpdateProducto(Producto productoModel) {
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

        public static Producto GetProductoByID(int ID) {
            using (SQLiteConnection sQLiteConnection= new SQLiteConnection(DataConnection.GetConnectionPath())) {
                return sQLiteConnection.Table<Producto>().Where(i => i.PRO_ID == ID).FirstOrDefault();
            }
        }

        public static IEnumerable<Producto> GetAllProductos() {
            using (SQLiteConnection sqliteConnection=new SQLiteConnection(DataConnection.GetConnectionPath())) {
                try
                {
                    sqliteConnection.CreateTable<Producto>();
                    IEnumerable<Producto>listProductos= sqliteConnection.Table<Producto>();
                    return listProductos;
                }
                catch (Exception ex) { }
                return Enumerable.Empty<Producto>();
            }
        }



    }
}
