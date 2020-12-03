using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
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
            Memoria.DinamicValue = 0;
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
            if (CamposDeTextoLlenos())
            {
                if (!UsuarioController.ExisteUsuarioByUsername(entry_RegistroNombreUsuario.Text))
                {
                    if (entry_RegistroContrasenia.Text
                        .Equals(entry_RegistroContraseniaConfirmar.Text))
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
                else
                {
                    MostrarMensaje("Alerta", "Este nombre de usuario ya está en uso", "Ok");
                }
            }
        }

        private bool CamposDeTextoLlenos()
        {
            if (!string.IsNullOrWhiteSpace(entry_RegistroNombreUsuario.Text) &&
                !string.IsNullOrWhiteSpace(entry_RegistroNombre.Text) &&
                !string.IsNullOrWhiteSpace(entry_RegistroApellido.Text) &&
                !string.IsNullOrWhiteSpace(entry_RegistroEmail.Text) &&
                !string.IsNullOrWhiteSpace(entry_RegistroContrasenia.Text) &&
                !string.IsNullOrWhiteSpace(entry_RegistroContraseniaConfirmar.Text))
            {
                return true;
            }
            else
            {
                MostrarMensaje("Información incompleta", "Por favor, rellene todos los espacios.", "OK");
                return false;
            }
        }

        public async void MostrarMensaje(string titulo, string mensaje, string Ok)
        {
            await DisplayAlert(titulo, mensaje, Ok);
        }
    }
}