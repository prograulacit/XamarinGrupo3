using ProyectoAndriodCsharp.Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipalCliente : ContentPage
    {
        public MenuPrincipalCliente()
        {
            InitializeComponent();
        }

        private void Button_MisCompras(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AllFacturas());
        }

        private void Button_MisAbonos(object sender, System.EventArgs e)
        {
            MensajeEmergente("Pendiente", "Aun no implementado", "OK");
        }

        private void Button_AjustesCuenta(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AjustesCuenta());
        }

        private void Button_CerrarSesion(object sender, System.EventArgs e)
        {
            Memoria.UsuarioActual = null;
            Application.Current.MainPage = new NavigationPage(new LoginRegistro());
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }

        private void Button_Productos(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }
    }
}