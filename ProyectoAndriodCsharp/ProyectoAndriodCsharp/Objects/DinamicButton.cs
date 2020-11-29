using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Forms;
using ProyectoAndriodCsharp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoAndriodCsharp.Objects
{
    public partial class DinamicButton:Button
    {
        public int DinamicValue { get; set; }

        public void SetMemoriaIdByProductID(object sender, EventArgs e)
        {
            Memoria.DinamicValue=DinamicValue;
            Application.Current.MainPage = new NavigationPage(new DescripcionProducto());
        }
        public void ViewProductasAdmin(object sender, EventArgs e) {
            Memoria.DinamicValue = DinamicValue;
            Memoria.State = "See";
            Application.Current.MainPage = new NavigationPage(new NuevoProducto());
        }
        public void BorrarProducto(object sender, EventArgs e)
        {
            ProductoRepository.EliminarProducto(ProductoRepository.GetProductoByID(Memoria.DinamicValue));
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }
        public void BorrarCompraProducto(object sender,EventArgs e) {
            List<CompraProductos> UpdateList = new List<CompraProductos>();
            foreach (var cp in Memoria.listaCarrito)
            {
                if (cp.PRO_ID == DinamicValue)
                {
                    
                }
                else
                {
                    //Seguir con proceso

                    UpdateList.Add(cp);

                }

            }
            Memoria.listaCarrito = UpdateList;
            Application.Current.MainPage = new NavigationPage(new Carrito());
        }
        public void SeeFactura(object sender,EventArgs e) {
            Memoria.DinamicValue = DinamicValue;
            Memoria.State = "See";
            Application.Current.MainPage = new NavigationPage(new Factura());
        }



    }
}
