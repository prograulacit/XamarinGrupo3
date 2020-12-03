using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescripcionProducto : ContentPage
    {
        Producto producto;
        public DescripcionProducto()
        {
            InitializeComponent();
            btnComprar.IsEnabled = false;
            producto = ProductoRepository.GetProductoByID(Memoria.DinamicValue);
            NombreProducto.Text = producto.PRO_NOMBRE;
            DescripcionProduct.Text = producto.PRO_DESCRIPCION;
            PrecioProducto.Text = producto.PRO_PRECIO.ToString();
            if (Memoria.listaCarrito.Find(x => x.PRO_ID == producto.PRO_ID) != null)
            {
                stepperproducto.Value = Memoria.listaCarrito.Find(x => x.PRO_ID == producto.PRO_ID).COMP_CANTIDAD;
                btnComprar.Text = "Actualizar";
            }
        }

        private async void btnComprar_Clicked(object sender, EventArgs e)
        {
            if (Memoria.listaCarrito.Find(x => x.PRO_ID == producto.PRO_ID) != null)
            {
                List<CompraProductos> UpdateList = new List<CompraProductos>();
                foreach (var cp in Memoria.listaCarrito)
                {
                    if (cp.PRO_ID == producto.PRO_ID)
                    {
                        //update CompraProducto
                        if (Int32.Parse(lbl_qnt_productos.Text) == 0)
                        {

                        }
                        else
                        {
                            UpdateList.Add(new CompraProductos(0, producto.PRO_ID, Int32.Parse(lbl_qnt_productos.Text)));
                        }
                    }
                    else
                    {
                        //Seguir con proceso
                        UpdateList.Add(cp);
                    }
                }
                Memoria.listaCarrito = UpdateList;
                await DisplayAlert("Aviso", "Producto actualizado con éxito.", "Ok");
                Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
            }
            else
            {
                Memoria.listaCarrito.Add(new CompraProductos(0, producto.PRO_ID, Int32.Parse(lbl_qnt_productos.Text)));
                await DisplayAlert("Aviso", "Producto agregado con éxito.", "Ok");
                Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
            }

        }

        private async void btnMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPrincipal());
        }

        private void btnlogout_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnCarrito_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Carrito());
        }

        private void stepperproducto_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Int32.Parse(lbl_qnt_productos.Text) > 0 && btnComprar.Text.Equals("Comprar"))
            {
                btnComprar.IsEnabled = true;
            }
            if (Int32.Parse(lbl_qnt_productos.Text) == 0 && btnComprar.Text.Equals("Comprar"))
            {
                btnComprar.IsEnabled = false;
            }
            if (btnComprar.Text.Equals("Actualizar"))
            {
                btnComprar.IsEnabled = true;
            }
        }
    }
}