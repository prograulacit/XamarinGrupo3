using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjustesCuenta : ContentPage
    {
        public AjustesCuenta()
        {
            InitializeComponent();
                CargarDatos();
        }

        private void CargarDatos()
        {
            entry_NombreUsuario.Text = Memoria.UsuarioActual.NombreUsuario;
            entry_NombreCompleto.Text = Memoria.UsuarioActual.Nombre;
            entry_CorreoElectronico.Text = Memoria.UsuarioActual.Email;
            entry_Saldo.Text = "" + Memoria.UsuarioActual.Saldo;
        }

        private void button_GuardarCambios(object sender, EventArgs e)
        {
            if (EspaciosLlenos() && SalarioEsNumero() && NombreUsuarioUnico())
            {
                UsuarioController usuarioController = new UsuarioController(DataConnection.GetConnectionPath());
                Usuario usuarioTemporal = Memoria.UsuarioActual;

                usuarioTemporal.Nombre = entry_NombreCompleto.Text;
                usuarioTemporal.NombreUsuario = entry_NombreUsuario.Text;
                usuarioTemporal.Email = entry_CorreoElectronico.Text;
                usuarioTemporal.Saldo = Convert.ToDecimal(entry_Saldo.Text);

                int FilasAfectadas = usuarioController.ActualizarUsuario(usuarioTemporal);

                if (FilasAfectadas > 0)
                {
                    Memoria.UsuarioActual = usuarioTemporal;
                    MensajeEmergente("Hecho", "Los datos del usuario han sido actualizados.", "OK");
                }
                else
                {
                    MensajeEmergente("Error!", usuarioController.Estado, "OK");
                }
            }
        }

        private void button_CambiarContrasenia(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CambiarContraseniaCliente());
        }

        private bool EspaciosLlenos()
        {
            if (!string.IsNullOrWhiteSpace(entry_CorreoElectronico.Text) &&
                !string.IsNullOrWhiteSpace(entry_NombreCompleto.Text) &&
                !string.IsNullOrWhiteSpace(entry_NombreUsuario.Text) &&
                !string.IsNullOrWhiteSpace(entry_Saldo.Text))
            {
                return true;
            }
            else
            {
                MensajeEmergente("Datos incompletos", "Por favor, rellene todos los espacios","OK");
            }
            return false;
        }

        private bool SalarioEsNumero()
        {
            try
            {
                decimal salario = Convert.ToDecimal(entry_Saldo.Text);
                return true;
            }
            catch (Exception)
            {
                MensajeEmergente("Dato incorrecto"
                    ,"El salario debe ser en formato numeral.","OK");
                return false;
            }
        }

        // Valida que si se ha cambiado el nombre de usuario este no se encuentre
        // en uso.
        private bool NombreUsuarioUnico()
        {
            UsuarioController usuarioController = 
                new UsuarioController(DataConnection.GetConnectionPath());
            if (usuarioController.NombreUsuarioUnico(entry_NombreUsuario.Text))
                return true;
            else
                MensajeEmergente("Alerta", "El nombre de usuario ingresado se encuentra en uso.", "OK");
            return false;
        }

        private void button_Volver(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }
    }
}