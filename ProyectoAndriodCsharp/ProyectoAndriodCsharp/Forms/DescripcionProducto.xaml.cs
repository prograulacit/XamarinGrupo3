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
    public partial class DescripcionProducto : ContentPage
    {
        Producto producto;
        public DescripcionProducto()
        {
            InitializeComponent();
            producto = ProductoRepository.GetProductoByID(Memoria.ProductoID);
            NombreProducto.Text = producto.PRO_NOMBRE;
            DescripcionProduct.Text = producto.PRO_DESCRIPCION;
            PrecioProducto.Text = producto.PRO_PRECIO.ToString();

        }

        private async void btnComprar_Clicked(object sender, EventArgs e)
        {
           Memoria.listaCarrito.Add(new CompraProductos(10, producto.PRO_ID, Int32.Parse(lbl_qnt_productos.Text)));
           string texto = "";
            
            foreach (var i in Memoria.listaCarrito)
            {
                texto += i.PRO_ID.ToString() + " " + i.COM_ID.ToString() + " " + i.COMP_CANTIDAD.ToString() + " " + "\n";
            }

           await DisplayAlert("Aviso", "Productos: " + "\n" + texto, "OK");
        }

        private async void btnMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPrincipal());
        }
        

        private async void btnlogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage ());
        }

        private async void btnCarrito_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Carrito());
        }
    }
}