using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllFacturas : ContentPage
    {
        
        public AllFacturas()
        {
            InitializeComponent();
            if (Memoria.UsuarioActual.US_ROL.Equals("Usuario"))
            {
                AdminView.IsEnabled = false;
                AdminView.IsVisible = false;
                
                int count = 1;
                foreach (var compra in CompraRepository.GetAllComprasByUserId(Memoria.UsuarioActual.UsuarioId))
                {
                    DinamicButton dinamicButton = new DinamicButton();
                    dinamicButton.DinamicValue = compra.COM_ID;
                    dinamicButton.Text = "Ver";
                    dinamicButton.Clicked += new EventHandler(dinamicButton.SeeFactura);
                    GridAllFacturas.Children.Add(new Label { Text = compra.COM_FECHA_COMPRA.ToString() }, 0, count);
                    GridAllFacturas.Children.Add(new Label { Text = "$" + Math.Truncate(compra.COM_PRECIO_TOTAL).ToString() }, 1, count);
                    GridAllFacturas.Children.Add(dinamicButton, 2, count);
                    count += 1;
                }
            }
            else {
                UserView.IsEnabled = false;
                UserView.IsVisible = false;

            }

        }

        private void Buscar_Clicked(object sender, EventArgs e)
        {
            try {
                int id=Int32.Parse(FacturaID.Text);
                Memoria.DinamicValue = id;
                Memoria.State = "Create";
                Application.Current.MainPage = new NavigationPage(new Factura());
            }
            catch {
                Alert("Alerta","Valor para ID de Factura no valido","Ok");
            }
        }

        public async void Alert(string a, string b, string c) {
            await DisplayAlert(a,b,c);
        }

        private void Home_Clicked2(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
        }
    }
}
