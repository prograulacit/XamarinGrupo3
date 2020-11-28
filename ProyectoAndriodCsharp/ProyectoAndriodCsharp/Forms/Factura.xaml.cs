using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Factura : ContentPage
    {
        public Factura()
        {
            InitializeComponent();
            lblCliente.Text = "Cliente: " + Memoria.UsuarioActual.Nombre;
            lblEmail.Text = "Correo Registrado: " + Memoria.UsuarioActual.Email;
            decimal cobroTotal = 0;
            int count = 0;
            if (Memoria.UsuarioActual.US_ROL.Equals("Usuario"))
            {
                if (Memoria.State.Equals("Create"))
                {
                    foreach (var product in Memoria.listaCarrito)
                    {
                        listaProductos.Children.Add(new Label { Text = ProductoRepository.GetProductoByID(product.PRO_ID.PRO_ID).PRO_NOMBRE, TextColor = Color.Black }, 0, count);
                        listaProductos.Children.Add(new Label { Text = "c/u $" + Math.Truncate(ProductoRepository.GetProductoByID(product.PRO_ID.PRO_ID).PRO_PRECIO).ToString(), TextColor = Color.Black }, 1, count);
                        listaProductos.Children.Add(new Label { Text = "Cantidad: " + product.COMP_CANTIDAD.ToString(), TextColor = Color.Black }, 2, count);
                        cobroTotal = cobroTotal + ((ProductoRepository.GetProductoByID(product.PRO_ID.PRO_ID).PRO_PRECIO) * product.COMP_CANTIDAD);
                        count += 1;
                    }
                }
                if (Memoria.State.Equals("See")) {
                    //Leer desde DB
                    foreach (var cp in CompraProductosRepository.GetAllCPByCompraID(Memoria.ProductoID)) {
                    
                    
                    
                    }
                }

            }
            lblTotal.Text = "Total: $" + cobroTotal;
            btnSalir.Clicked += BtnSalir_Clicked;
        }


        private void BtnSalir_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }
    }
}