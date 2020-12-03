using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegistro : TabbedPage
    {
        public LoginRegistro()
        {
            InitializeComponent();
        }

        private void btn_IniciarSesion(object sender, EventArgs e)
        {
            if (Ingreso_EspaciosLlenos())
            {
                LoginRequest loginRequest = new LoginRequest();
                loginRequest.Username = entry_IngresoUsuario.Text;
                loginRequest.Password = entry_IngresoContrasenia.Text;


                if (CNIniciarComoAdmin.IsChecked && UsuarioController.ValidarAdministrador(loginRequest))
                {
                    Memoria.UsuarioActual = UsuarioController.GetUsuarioByUsername(loginRequest.Username);
                    MensajeEmergente("Administrador correcto"
                            , string.Format("Ingreso de {0} correcto.", Memoria.UsuarioActual.NombreUsuario)
                            , "Aceptar");
                    AbrirMenuPrincipal();
                }
                else if (!CNIniciarComoAdmin.IsChecked && UsuarioController.ValidarUsuario(loginRequest))
                {
                    Memoria.UsuarioActual = UsuarioController.GetUsuarioByUsername(loginRequest.Username);
                    MensajeEmergente("Cliente correcto"
                               , string.Format("Ingreso de {0} correcto.", Memoria.UsuarioActual.NombreUsuario)
                               , "Aceptar");
                    AbrirMenuPrincipalCliente();
                }
                else { MensajeEmergente("Ingreso incorrecto", "Los credenciales son incorrectos.", "Aceptar"); }
            }
        }

        private void btn_Registrarse(object sender, EventArgs e)
        {
            if (Registro_EspaciosLlenos() &&
                Registro_ContraseniasCoinciden())
            {
                Usuario usuario = new Usuario()
                {
                    NombreUsuario = entry_RegistroNombreUsuario.Text,
                    Nombre = entry_RegistroNombre.Text + " " + entry_RegistroApellido.Text,
                    Contrasenia = entry_RegistroContrasenia.Text,
                    Email = entry_RegistroEmail.Text,
                    US_ROL = "Usuario",
                    Saldo = Tareas.GenerarNumeroAleatorio(1000, 2000)
                };

                UsuarioController usuarioController = new UsuarioController(DataConnection.GetConnectionPath());
                int FilasAfectadas = usuarioController.InsertarUsuario(usuario);

                if (FilasAfectadas <= 0) // Ha ocurrido un error.
                {
                    MensajeEmergente("Error!", usuarioController.Estado, "Aceptar");
                }
                else
                {
                    Memoria.UsuarioActual = UsuarioController
                        .GetUsuarioByUsername(entry_RegistroNombreUsuario.Text);

                    MensajeEmergente("Registro correcto"
                        ,string.Format("Se ha registrado correctamente como: {0}", Memoria.UsuarioActual.NombreUsuario)
                        , "Aceptar");

                    AbrirMenuPrincipalCliente();
                }
            }
        }

        private bool Ingreso_EspaciosLlenos()
        {
            if (!string.IsNullOrEmpty(entry_IngresoUsuario.Text) &&
                !string.IsNullOrEmpty(entry_IngresoContrasenia.Text))
            {
                return true;
            }
            return false;
        }

        private bool Registro_EspaciosLlenos()
        {
            if (!string.IsNullOrEmpty(entry_RegistroNombre.Text) &&
                !string.IsNullOrEmpty(entry_RegistroApellido.Text) &&
                !string.IsNullOrEmpty(entry_RegistroContrasenia.Text) &&
                !string.IsNullOrEmpty(entry_RegistroContraseniaConfirmar.Text) &&
                !string.IsNullOrEmpty(entry_RegistroEmail.Text) &&
                !string.IsNullOrEmpty(entry_RegistroNombreUsuario.Text))
            {
                return true;
            }
            return false;
        }

        private bool Registro_ContraseniasCoinciden()
        {
            if (entry_RegistroContrasenia.Text
                .Equals(entry_RegistroContraseniaConfirmar.Text))
                return true;
            else
            {
                MensajeEmergente("Error", "Las contraseñas no coinciden.", "Aceptar");
                return false;
            }
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }

        private void AbrirMenuPrincipal()
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }

        private void AbrirMenuPrincipalCliente()
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
        }
    }
}