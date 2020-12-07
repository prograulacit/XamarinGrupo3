using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            Inicializadores();
        }

        private void Inicializadores()
        {
            if (ProductoRepository.GetProductoByID(7) == null)
            {
                ProductoRepository.InsertarPrueba();
                ProductoRepository.InsertarPrueba();
            }
            int count = 1;
            NombreUsuario.Text = Memoria.UsuarioActual.Nombre;
            if (Memoria.UsuarioActual.US_ROL.Equals("Usuario"))
            {
                NewProducto.IsVisible = false;
                NewAdmin.IsVisible = false;

                foreach (var product in ProductoRepository.GetAllProductosDisponibles())
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
            if (Memoria.UsuarioActual.US_ROL.Equals("Administrador"))
            {
                Home.IsVisible = true; 
                foreach (var product in ProductoRepository.GetAllProductos())
                {
                    DinamicButton dinamicButton = new DinamicButton();
                    dinamicButton.DinamicValue = product.PRO_ID;
                    dinamicButton.Text = "Ver";
                    dinamicButton.Clicked += new EventHandler(dinamicButton.ViewProductasAdmin);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }, 0, count);
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
            Memoria.State = "New";
            Application.Current.MainPage = new NavigationPage(new NuevoProducto());
        }

        private void NewAdmin_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new NuevoAdmin());
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
        }
    }
}