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
    public partial class NuevoProducto : ContentPage
    {
        public NuevoProducto()
        {
            InitializeComponent();
            if (Memoria.State.Equals("New")) { Actualizar.IsVisible = false; }
        }

        private void Insertar_Clicked(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            
            try
            {
                producto.PRO_NOMBRE = entry_Nombre.Text;
                producto.PRO_PRECIO = Int32.Parse(entry_Precio.Text);
                producto.PRO_ESTADO = "Activo";
                if (CNInsertarComoInactivo.IsChecked==true) { producto.PRO_ESTADO = "Inactivo"; }
                ProductoRepository.IngresarProducto(producto);
            }
            catch (Exception c) {
                MensajeEmergente("Error","Valor no valido.","Ok");
            }
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }

        private void Actualizar_Clicked(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_Clicked(object sender, EventArgs e)
        {

        }
    }
}