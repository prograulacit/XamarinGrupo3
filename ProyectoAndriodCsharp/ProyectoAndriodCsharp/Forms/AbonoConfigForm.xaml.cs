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
    public partial class AbonoConfigForm : ContentPage
    {
        public AbonoConfigForm()
        {
            InitializeComponent();
            PrecioTotal.Text = Memoria.compra.COM_PRECIO_TOTAL.ToString();
            CantidadMensual.Text = ((Decimal.Parse(PrecioTotal.Text) / Decimal.Parse("2"))*Decimal.Parse("1.03")).ToString();
            ConfigAbono.IsVisible = false;
        }

        private void ValidacionStepper(object sender, ValueChangedEventArgs e)
        {
            CantidadMensual.Text = ((Int32.Parse(PrecioTotal.Text) / Int32.Parse(CantidadDeMeses.Text)) * 1.03).ToString();
        }

        private void cb_Abono_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (cb_Abono.IsChecked == true) { ConfigAbono.IsVisible = true; }
            else { ConfigAbono.IsVisible = false; }

        }

        private void Facturar_Clicked(object sender, EventArgs e)
        {
            if (cb_Abono.IsChecked)
            {
                //Config  abono, insert factura & insert abonoPorMes
                if (Memoria.UsuarioActual.Saldo >= Decimal.Parse(PrecioTotal.Text))
                {
                    Memoria.compra.US_ID = 0;
                    Memoria.compra.COM_FECHA_COMPRA = DateTime.Today;
                    Memoria.compra.COM_ESTADO = "Abonos";
                    Memoria.compra.COM_PRECIO_TOTAL = Decimal.Parse(PrecioTotal.Text);
                    Memoria.compra.COM_INTERES = (Decimal.Parse(PrecioTotal.Text));
                    Memoria.compra.COM_SIGUIENTE_PAGO = DateTime.Today.AddMonths(1);
                    int CompraID=CompraRepository.InsertAndReturn(Memoria.compra);
                    List<CompraProductos> listTemp=new List<CompraProductos>();
                    foreach (var cp in Memoria.listaCarrito) {
                        cp.COM_ID = CompraID;
                        listTemp.Add(cp);
                    }
                    Memoria.listaCarrito = listTemp;
                    IEnumerable<CompraProductos> list = Memoria.listaCarrito;
                    CompraProductosRepository.InsertAll(list);
                    Memoria.DinamicValue = CompraID;
                    Abono abono = new Abono {COM_ID=0,ABO_CANTIDAD_DE_MESES=Int32.Parse(CantidadDeMeses.Text),ABO_CANTIDAD_MENSUAL=Decimal.Parse(CantidadMensual.Text),ABO_RESTANTE=(Decimal.Parse(PrecioTotal.Text)-Decimal.Parse(CantidadMensual.Text)) };
                    int AbonoID=AbonoRepository.InsertAndReturn(abono);
                    Memoria.UsuarioActual.Saldo -= Decimal.Parse(CantidadMensual.Text);
                    AbonoPorMesRepository.InsertarAbonoPorMes(new AbonoPorMes {ABO_ID=AbonoID,ABEM_FECHA_DEPOSITADA=DateTime.Today,ABEM_CANTIDAD_DEPOSITADA= Decimal.Parse(CantidadMensual.Text) });
                    UsuarioController.UpdateUser(Memoria.UsuarioActual);
                    Memoria.UsuarioActual = UsuarioController.GetUserByID(Memoria.UsuarioActual.UsuarioId);
                    Memoria.State = "Create";
                    Memoria.DinamicValue = CompraID;
                    Memoria.listaCarrito = new List<CompraProductos>();
                    Application.Current.MainPage = new NavigationPage(new Factura());
                }
                else {
                    Alert("Alerta", "No posee los fondos suficientes","Ok");
                }
            }
            else {
                //set Contado
                if (Memoria.UsuarioActual.Saldo >= Decimal.Parse(PrecioTotal.Text)) {
                    Memoria.compra.US_ID = 0;
                    Memoria.compra.COM_FECHA_COMPRA = DateTime.Today;
                    Memoria.compra.COM_ESTADO = "Contado";
                    Memoria.compra.COM_SIGUIENTE_PAGO = DateTime.Today;
                    Memoria.compra.COM_PRECIO_TOTAL = Decimal.Parse(PrecioTotal.Text);
                    Memoria.compra.COM_INTERES = (Decimal.Parse(PrecioTotal.Text) * Decimal.Parse("0,3"));
                    int CompraID = CompraRepository.InsertAndReturn(Memoria.compra);
                    List<CompraProductos> listTemp = new List<CompraProductos>();
                    foreach (var cp in Memoria.listaCarrito)
                    {
                        cp.COM_ID = CompraID;
                        listTemp.Add(cp);
                    }
                    Memoria.DinamicValue = CompraID;
                    Memoria.listaCarrito = listTemp;
                    IEnumerable<CompraProductos> list = Memoria.listaCarrito;
                    CompraProductosRepository.InsertAll(list);
                    Abono abono = new Abono { COM_ID = 0, ABO_CANTIDAD_DE_MESES = 1, ABO_CANTIDAD_MENSUAL = 0, ABO_RESTANTE = 0 };
                    int AbonoID = AbonoRepository.InsertAndReturn(abono);
                    Memoria.UsuarioActual.Saldo -= Decimal.Parse(PrecioTotal.Text);
                    AbonoPorMesRepository.InsertarAbonoPorMes(new AbonoPorMes { ABO_ID = AbonoID, ABEM_FECHA_DEPOSITADA = DateTime.Today, ABEM_CANTIDAD_DEPOSITADA = Decimal.Parse(PrecioTotal.Text) });
                    UsuarioController.UpdateUser(Memoria.UsuarioActual);
                    Memoria.State = "Create";
                    Memoria.DinamicValue = CompraID;
                    Memoria.UsuarioActual = UsuarioController.GetUserByID(Memoria.UsuarioActual.UsuarioId);
                    Memoria.listaCarrito = new List<CompraProductos>();
                    Application.Current.MainPage = new NavigationPage(new Factura());
                }
                else
                {
                    Alert("Alerta", "No posee los fondos suficientes", "Ok");
                }


            }
        }

        public async void Alert(string a, string b, string c) {

            await DisplayAlert(a,b,c);
        }

        private void Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Carrito());
        }
    }
}