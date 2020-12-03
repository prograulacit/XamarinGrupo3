using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CambiarContraseniaCliente : ContentPage
    {
        public CambiarContraseniaCliente()
        {
            InitializeComponent();
        }

        private void Button_GuardarCambios(object sender, EventArgs e)
        {
            if (EspaciosLlenos() && ContraseniasCoinciden() && ContraseniaCorrecta())
            {
                CambiarContrasenia_();
                LimpiarEntries();
                MensajeEmergente("Hecho", "Contraseña actualizada exitosamente.", "OK");
            }
        }

        private bool EspaciosLlenos()
        {
            if (!string.IsNullOrEmpty(entry_ContraseniaActual.Text) &&
                !string.IsNullOrEmpty(entry_NuevaContrasenia.Text) &&
                !string.IsNullOrEmpty(entry_ConfirmarContrasenia.Text))
                return true;
            else
            {
                MensajeEmergente("Alerta", "Por favor, rellene todos los espacios", "OK");
                return false;
            }
        }

        private bool ContraseniasCoinciden()
        {
            if (entry_NuevaContrasenia.Text
                .Equals(entry_ConfirmarContrasenia.Text))
                return true;
            else
            {
                MensajeEmergente("Alerta", "Las contraseñas no coinciden.", "OK");
                return false;
            }
        }

        // Checkea si la contraseña actual escrita coincide con la contraseña
        // actual.
        private bool ContraseniaCorrecta()
        {
            string ContraseniaActual =
                UsuarioController.GetUsuarioByUsername(
                    Memoria.UsuarioActual.NombreUsuario).Contrasenia;

            if (ContraseniaActual.Equals(entry_ContraseniaActual.Text))
                return true;
            else
            {
                MensajeEmergente("Credenciales incorrectos."
                    , "La contraseña ingresada no es correcta.", "OK");
                return false;
            }
        }

        private void CambiarContrasenia_()
        {
            // Se cambia la contraseña en la base de datos.
            UsuarioController usuarioController =
                new UsuarioController(DataConnection.GetConnectionPath());

            Memoria.UsuarioActual.Contrasenia = entry_ConfirmarContrasenia.Text;
            usuarioController.ActualizarUsuario(Memoria.UsuarioActual);
        }

        private void LimpiarEntries()
        {
            entry_ContraseniaActual.Text = "";
            entry_NuevaContrasenia.Text = "";
            entry_ConfirmarContrasenia.Text = "";
        }

        private void Button_Volver(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string boton)
        {
            await DisplayAlert(titulo, cuerpo, boton);
        }
    }
}