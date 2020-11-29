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
        Label labelCantidad;
        public Carrito()
        {
            InitializeComponent();
            int count = 1;

              foreach (var product in Memoria.listaCarrito)
              {
                DinamicButton dinamicButton = new DinamicButton();
                dinamicButton.DinamicValue = product.PRO_ID;
                dinamicButton.Text = "Eliminar";

                var entry = new Entry { Text = "" };
                entry.Text = product.COMP_CANTIDAD.ToString();
                entry.Keyboard = Keyboard.Numeric;


                labelCantidad = new Label { Text = product.COMP_CANTIDAD.ToString() };

                GridAllProducts.Children.Add(new Label { Text = ProductoRepository.GetProductoByID(product.PRO_ID).PRO_NOMBRE }, 0, count);
                GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(ProductoRepository.GetProductoByID(product.PRO_ID).PRO_PRECIO).ToString() }, 1, count);

                GridAllProducts.Children.Add(dinamicButton, 2, count);
                GridAllProducts.Children.Add(entry, 3, count);


                count += 1;
              }
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            labelCantidad.Text = String.Format(e.NewValue.ToString());
        }

        private void Factura_Clicked(object sender, EventArgs e)
        {
            Memoria.State = "Create";
            Application.Current.MainPage = new NavigationPage(new Factura());
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