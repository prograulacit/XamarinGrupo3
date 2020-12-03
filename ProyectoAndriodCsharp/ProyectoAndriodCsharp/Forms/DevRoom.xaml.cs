using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevRoom : ContentPage
    {
        public DevRoom()
        {
            InitializeComponent();
        }

        private void button_Continuar(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginRegistro());
        }

        private void button_ReiniciarTablas(object sender, EventArgs e)
        {
            DataConnection.DropTables();
            DataConnection.CreateTables();
                MensajeEmergente("Hecho"
                , "Tablas reiniciadas", "OK");
        }

        private void button_CrearUsuariosIniciales(object sender, EventArgs e)
        {
            Tareas.CrearUsuariosIniciales();
            MensajeEmergente("Hecho"
                ,"Ejecución de creación Administrador y usuario por defecto hecho.","OK");
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }

        // Muestra en pantalla todos los registros de Compra y CompraProductos de la base de datos.
        private void button_RegistrosCompra(object sender, EventArgs e)
        {
            string str = "";
            List<Compra> lista_compra = Controller.CompraRepository.GetAllCompra().ToList();
            foreach (var compra in lista_compra)
            {
                str += "COMPRA-> ID:" + compra.COM_ID + ". US_ID: " + compra.US_ID + ". Fecha: " +compra.COM_FECHA_COMPRA + "" +
                    ". Precio total: " + compra.COM_PRECIO_TOTAL + "\r\n\r\n";
            }

            List<CompraProductos> lista_CompraProductos = Controller.CompraProductosRepository.GetAllCompraProductos().ToList();
            foreach (var compraProducto in lista_CompraProductos)
            {
                str += "COMPRAPRODUCTO-> ID:" + compraProducto.COMP_ID + ". CompraID:" + compraProducto.COM_ID + "" +
                    ". ProductoID: " + compraProducto.PRO_ID + ". Cantidad:" + compraProducto.COMP_CANTIDAD + "\r\n";
            }

            MensajeEmergente("Resultado",str,"OK");
        }
    }
}