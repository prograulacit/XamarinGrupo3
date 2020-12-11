using Plugin.Media;
using Plugin.Media.Abstractions;
using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoProducto : ContentPage
    {
        private MediaFile _mediaFile;
        public NuevoProducto()
        {
            InitializeComponent();
            if (Memoria.State.Equals("New"))
            {

                Actualizar.IsVisible = false;

            }
            else {
                Insertar.Text = "Borrar Producto";
                Producto producto = ProductoRepository.GetProductoByID(Memoria.DinamicValue);
                Activo.Text = "Activo";
                if (producto.PRO_ESTADO.Equals("Activo")) { CNInsertarComoInactivo.IsChecked = true; } 
                entry_Nombre.Text = producto.PRO_NOMBRE;
                entry_Descripcion.Text = producto.PRO_DESCRIPCION;
                entry_Precio.Text = producto.PRO_PRECIO.ToString();
            }
        }

        private void Insertar_Clicked(object sender, EventArgs e)
        {
            if (Memoria.State.Equals("New"))
            {
                Producto producto = new Producto();
                try
                {
                    producto.PRO_NOMBRE = entry_Nombre.Text;
                    producto.PRO_DESCRIPCION = entry_Descripcion.Text;
                    producto.PRO_PRECIO = Int32.Parse(entry_Precio.Text);
                    producto.PRO_ESTADO = "Activo";
                    if (CNInsertarComoInactivo.IsChecked == true) { producto.PRO_ESTADO = "Inactivo"; }
                    ProductoRepository.IngresarProducto(producto);
                    Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
                }
                catch
                {
                    MensajeEmergente("Error", "Valor no valido.", "Ok");
                }
            }
            if (Memoria.State.Equals("See")) {
                ProductoRepository.EliminarProducto(ProductoRepository.GetProductoByID(Memoria.DinamicValue));
                Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
            }


        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }

        private void Actualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto { PRO_ID = Memoria.DinamicValue, PRO_NOMBRE = entry_Nombre.Text, PRO_DESCRIPCION = entry_Descripcion.Text, PRO_PRECIO = Int32.Parse(entry_Precio.Text) };
                if (CNInsertarComoInactivo.IsChecked == true) { producto.PRO_ESTADO = "Activo"; }
                else { producto.PRO_ESTADO = "Inactivo"; }
                ProductoRepository.UpdateProducto(producto);
                Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
            }
            catch {
                MensajeEmergente("Error", "Valor no valido.", "Ok");
            }
        }

        private void MenuPrincipal_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }

        private void AllFacturas_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AllFacturas());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async  void subir_foto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":(No PickPhoto available", "Ok");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return;

            LocalPathLabel.Text = _mediaFile.Path;

            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });


        }
    }
}