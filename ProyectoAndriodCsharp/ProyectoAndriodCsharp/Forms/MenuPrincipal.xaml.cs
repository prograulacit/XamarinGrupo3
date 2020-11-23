using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            if (ProductoRepository.GetProductoByID(7) == null) {
                ProductoRepository.InsertarPrueba();
                ProductoRepository.InsertarPrueba();
            }
            int count = 1;
            NombreUsuario.Text = Memoria.UsuarioActual.Nombre;
            if (Memoria.UsuarioActual.US_ROL.Equals("Usuario")) {
                NewProducto.IsVisible = false;
                NewAdmin.IsVisible = false;

                foreach (var product in ProductoRepository.GetAllProductos())
                {
                    DinamicButton dinamicButton = new DinamicButton();
                    dinamicButton.DinamicValue = product.PRO_ID;
                    dinamicButton.Text = "Ver";
                    dinamicButton.Clicked += new EventHandler(dinamicButton.SetMemoriaIdByProductID);

                    GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE }, 0, count);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_DESCRIPCION }, 1, count);
                    GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(product.PRO_PRECIO).ToString() }, 2, count);

                    GridAllProducts.Children.Add(dinamicButton, 3, count);

                    count += 1;
                }
            }
            
            if (Memoria.UsuarioActual.US_ROL.Equals("Administrador")) {
                foreach (var product in ProductoRepository.GetAllProductos())
                {
                    DinamicButton dinamicButton = new DinamicButton();
                    dinamicButton.DinamicValue = product.PRO_ID;
                    dinamicButton.Text = "Ver";
                    dinamicButton.Clicked += new EventHandler(dinamicButton.SetMemoriaIdByProductID);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE ,HorizontalTextAlignment=TextAlignment.Center,VerticalTextAlignment=TextAlignment.Center}, 0, count);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_DESCRIPCION, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }, 1, count);
                    GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(product.PRO_PRECIO).ToString(), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }, 2, count);
                    GridAllProducts.Children.Add(dinamicButton, 3, count);
                    count += 1;
                }
            }
            


        }

        private void Carrito_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Carrito());
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