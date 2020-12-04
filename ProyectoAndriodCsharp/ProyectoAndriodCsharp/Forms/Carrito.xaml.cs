using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                //Boton para eliminar
                DinamicButton dinamicButton = new DinamicButton();
                dinamicButton.DinamicValue = product.PRO_ID;
                dinamicButton.Text = "Eliminar";
                dinamicButton.Clicked += new EventHandler(dinamicButton.BorrarCompraProducto);
                labelCantidad = new Label { Text = product.COMP_CANTIDAD.ToString() };

                //Boton para ver producto y actualizar
                DinamicButton dinamicButtonVer = new DinamicButton();
                dinamicButtonVer.DinamicValue = product.PRO_ID;
                dinamicButtonVer.Text = "Ver";
                dinamicButtonVer.Clicked += new EventHandler(dinamicButtonVer.SetMemoriaIdByProductID);

                GridAllProducts.Children.Add(new Label { Text = ProductoRepository.GetProductoByID(product.PRO_ID).PRO_NOMBRE }, 0, count);
                GridAllProducts.Children.Add(new Label { Text = "$" + Math.Truncate(ProductoRepository.GetProductoByID(product.PRO_ID).PRO_PRECIO).ToString() }, 1, count);
                GridAllProducts.Children.Add(new Label { Text = product.COMP_CANTIDAD.ToString()}, 2, count);
                GridAllProducts.Children.Add(dinamicButton, 3, count);
                GridAllProducts.Children.Add(dinamicButtonVer, 4, count);
               


                count += 1;
              }
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            labelCantidad.Text = String.Format(e.NewValue.ToString());
        }
        public async void Alert(string a,string b,string c) {
            await DisplayAlert(a, b, c);
        }
        private void Factura_Clicked(object sender, EventArgs e)
        {
            Memoria.State = "Create";
            decimal total=0;
            if (Memoria.listaCarrito.Count == 0)
            {
                Alert("Alerta","No hay productos en el carrito","Ok");
            }
            else
            {
                foreach (var product in Memoria.listaCarrito)
                {
                    total += ((ProductoRepository.GetProductoByID(product.PRO_ID).PRO_PRECIO) * product.COMP_CANTIDAD);
                }
                Memoria.compra.US_ID = Memoria.UsuarioActual.UsuarioId;
                Memoria.compra.COM_PRECIO_TOTAL = total;
                Application.Current.MainPage = new NavigationPage(new AbonoConfigForm());
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