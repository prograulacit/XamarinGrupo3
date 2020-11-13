using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;
using ProyectoAndriodCsharp.Objects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Factura : ContentPage
    {
        public Factura()
        {
            InitializeComponent();
            btnVerFactura.Clicked += BtnVerFactura_Clicked;

            listaProductos.ItemTapped += ListaProductos_ItemTapped;
            btnSalir.Clicked += BtnSalir_Clicked;
        }
        private void ListaProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Producto:", listaProductos.SelectedItem.ToString(), "OK");
        }

        private void BtnSalir_Clicked(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new LoginRegistro());
        }



        private void BtnVerFactura_Clicked(object sender, EventArgs e)
        {
            lblCliente.Text = "Cliente: " + Memoria.UsuarioActual.Nombre;
            lblEmail.Text = "Correo Registrado: " + Memoria.UsuarioActual.Email;
            listaProductos.IsVisible = true;
            lblTotal.Text = "Monto Total a Pagar: $4450";
        }
    }
}