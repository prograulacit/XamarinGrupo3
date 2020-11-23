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
        Label label;
        public Carrito()
        {
            InitializeComponent();

         
            int count = 1;

              foreach (var product in Memoria.listaCarrito)
              {
                DinamicButton dinamicButton = new DinamicButton();
                dinamicButton.DinamicValue = product.PRO_ID;
                dinamicButton.Text = "Eliminar";
                

                Stepper stepper = new Stepper();
                stepper.Margin = new Thickness(20);
                stepper.HorizontalOptions = LayoutOptions.Start;
                stepper.Maximum = 10;
                stepper.Minimum = 1;
                stepper.Increment = 1;
                stepper.ValueChanged += Stepper_ValueChanged;

                label = new Label { Text = "" };
                

                 GridAllProducts.Children.Add(new Label { Text = product.PRO_ID.ToString() }, 0, count);
                 //GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(10).ToString() }, 1, count);
                 GridAllProducts.Children.Add(new Label { Text = "$100" }, 1, count);
                 GridAllProducts.Children.Add(new Label { Text = product.COMP_CANTIDAD.ToString() }, 2, count);
                 
                 GridAllProducts.Children.Add(label, 3, count);
                 GridAllProducts.Children.Add(stepper, 4, count);
                 //GridAllProducts.Children.Add(dinamicButton, 5, count);

                count += 1;
              }

        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            label.Text = String.Format(e.NewValue.ToString());
        }

        private void Factura_Clicked(object sender, EventArgs e)
        {
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