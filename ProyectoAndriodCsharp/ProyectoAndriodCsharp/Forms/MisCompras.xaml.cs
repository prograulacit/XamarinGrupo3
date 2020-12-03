using ProyectoAndriodCsharp.Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisCompras : ContentPage
    {
        public MisCompras()
        {
            InitializeComponent();
            Inicializador();
        }

        private void Inicializador()
        {
            Objects.MisCompras misCompras = new Objects.MisCompras(Memoria.UsuarioActual.UsuarioId);
            label_compras.Text = misCompras.GetEstructura_1();
        }

        private void Button_Volver(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
        }
    }
}