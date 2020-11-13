using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoAdmin : ContentPage
    {
        public NuevoAdmin()
        {
            InitializeComponent();
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Memoria.ProductoID = 0;
            Memoria.UsuarioActual = null;
            Application.Current.MainPage = new NavigationPage(new LoginRegistro());
        }

        private void NewProducto_Clicked(object sender, EventArgs e)
        {
            //Crud Ingresar Producto
        }

        private void MenuPrincipal_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }

        private void NewAdmin_Clicked(object sender, EventArgs e)
        {
            if (!UsuarioController.ExisteUsuarioByUsername(entry_RegistroNombreUsuario.Text))
            {

                if (entry_RegistroContrasenia.Text.Equals(entry_RegistroContraseniaConfirmar.Text))
                {
                    Usuario administrador = new Usuario
                    {
                        NombreUsuario = entry_RegistroNombreUsuario.Text,
                        Nombre = entry_RegistroNombre.Text + " " + entry_RegistroApellido.Text,
                        Email = entry_RegistroEmail.Text,
                        Contrasenia = entry_RegistroContrasenia.Text,
                        US_ROL = "Administrador"
                    };
                    UsuarioController.IngresarUsuario(administrador);
                }
            }
            else {
                MostrarMensaje("Alerta","Este nombre de usuario ya está en uso","Ok");
            }
        }


        public async void MostrarMensaje(string titulo,string mensaje,string Ok) {
            await DisplayAlert(titulo,mensaje,Ok);

        }
    }
}