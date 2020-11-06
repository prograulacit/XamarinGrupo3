using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            ProductoController.InsertarPrueba();
            int count = 0;
            NombreUsuario.Text = Memoria.UsuarioActual.Nombre;
            
            if (Memoria.UsuarioActual.US_ROL.Equals("Usuario")) {
                NewProducto.IsVisible = false;
                NewAdmin.IsVisible = false;
                foreach (var product in ProductoController.GetAllProductos())
                {
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE }, 0, count);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_DESCRIPCION }, 1, count);
                    GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(product.PRO_PRECIO).ToString() }, 2, count);
                    count += 1;
                }
            }
            if (Memoria.UsuarioActual.US_ROL.Equals("Administrador")) {
                foreach (var product in ProductoController.GetAllProductos())
                {
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE }, 0, count);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_DESCRIPCION }, 1, count);
                    GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(product.PRO_PRECIO).ToString() }, 2, count);
                    GridAllProducts.Children.Add(new Button { Text = "Borrar Producto" }, 3, count);
                    count += 1;
                }
            }
            


        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Memoria.UsuarioActual = null;
            Application.Current.MainPage = new NavigationPage(new LoginRegistro());
        }

        private void NewProducto_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new NuevoProducto());
        }

        private void NewAdmin_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new NuevoAdmin());
        }
    }
}