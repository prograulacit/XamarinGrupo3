using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using ProyectoAndriodCsharp.Model;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;


namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carrito : ContentPage
    {
        public Carrito()
        {
            InitializeComponent();

            if (ProductoRepository.GetProductoByID(7) == null)
            {
                ProductoRepository.InsertarPrueba();
                ProductoRepository.InsertarPrueba();
            }
            int count = 1;

                foreach (var product in ProductoRepository.GetAllProductos())
                {
                    DinamicButton dinamicButton = new DinamicButton();
                    dinamicButton.DinamicValue = product.PRO_ID;
                    dinamicButton.Text = "Ver";

                    DinamicButton dinamicButton2 = new DinamicButton();
                    dinamicButton2.Text = "Eliminar";

                    DinamicButton dinamicButton3 = new DinamicButton();
                    dinamicButton3.Text = "Agregar";


                    //dinamicButton.Clicked += new EventHandler(dinamicButton.SetMemoriaIdByProductID);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE }, 0, count);
                    GridAllProducts.Children.Add(new Label { Text = product.PRO_DESCRIPCION }, 1, count);
                    GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(product.PRO_PRECIO).ToString() }, 2, count);
                    GridAllProducts.Children.Add(dinamicButton, 3, count);
                    GridAllProducts.Children.Add(dinamicButton2, 4, count);
                    GridAllProducts.Children.Add(dinamicButton3, 5, count);

                    count += 1;
                }

        }

        private void Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Memoria.UsuarioActual = null;
            Application.Current.MainPage = new NavigationPage(new LoginRegistro());
        }
    }
}