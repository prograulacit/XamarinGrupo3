using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using ProyectoAndriodCsharp.Objects;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoAndriodCsharp.Controller
{
    public class ProductoRepository
    {
        
        public static void InsertarPrueba()
        {
            Producto product1 = new Producto { PRO_NOMBRE = "string1", PRO_DESCRIPCION = "string1", PRO_PRECIO = 12 ,PRO_ESTADO="Activo"};
            Producto product2 = new Producto { PRO_NOMBRE = "string2", PRO_DESCRIPCION = "string2", PRO_PRECIO = 22, PRO_ESTADO = "Activo" };
            Producto product3 = new Producto { PRO_NOMBRE = "string3", PRO_DESCRIPCION = "string3", PRO_PRECIO = 32, PRO_ESTADO = "Activo" };
            ProductoRepository.IngresarProducto(product1);
            ProductoRepository.IngresarProducto(product2);
            ProductoRepository.IngresarProducto(product3);
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
            productoModel.PRO_ESTADO = "Inactivo";
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath())) {
                int filasAfectadas=sQLiteConnection.Update(productoModel);
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
        public static IEnumerable<Producto> GetAllProductosDisponibles() {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Producto> listProductos = sqliteConnection.Table<Producto>().Where(v=>v.PRO_ESTADO.Equals("Activo"));
            return listProductos;
        }
        public static IEnumerable<Producto> GetAllProductosNoDisponibles()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Producto> listProductos = sqliteConnection.Table<Producto>().Where(v => v.PRO_ESTADO.Equals("Inactivos"));
            return listProductos;
        }


        public static IEnumerable<Producto> GetAllProductos() {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Producto>listProductos= sqliteConnection.Table<Producto>();
            return listProductos;
        }



    }
}
