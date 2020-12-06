using Plugin.Media;
using Plugin.Media.Abstractions;
using ProyectoAndriodCsharp.Forms;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using SQLite;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace ProyectoAndriodCsharp.Controller
{
    public class ProductoRepository
    {
        
        public static void InsertarPrueba()
        {
            Producto product1 = new Producto { PRO_NOMBRE = "Lavadora", PRO_DESCRIPCION = "Soporta 75kgs", PRO_PRECIO = 2000 ,PRO_ESTADO="Activo"};
            Producto product2 = new Producto { PRO_NOMBRE = "Refrigeradora", PRO_DESCRIPCION = "2m alto", PRO_PRECIO = 1500, PRO_ESTADO = "Activo" };
            Producto product3 = new Producto { PRO_NOMBRE = "Cocina", PRO_DESCRIPCION = "6 discos", PRO_PRECIO = 2500, PRO_ESTADO = "Activo" };
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

        public static async void SetImageByIDAsync(int id) {
            MediaFile _mediaFile;
            await CrossMedia.Current.Initialize();
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return;
            Producto producto = GetProductoByID(id);
            producto.ImagePath = _mediaFile.Path;
            
            NuevoProducto.ImagePath = _mediaFile.Path;
            UpdateProducto(producto);
            Application.Current.MainPage = new NavigationPage(new NuevoProducto());
        }
        public static async void SetImageByIDAsync()
        {
            MediaFile _mediaFile;
            await CrossMedia.Current.Initialize();
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return; 
            NuevoProducto.ImagePath = _mediaFile.Path;
        }


        public static IEnumerable<Producto> GetAllProductos() {
            SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            IEnumerable<Producto>listProductos= sqliteConnection.Table<Producto>();
            return listProductos;
        }
    }
}
