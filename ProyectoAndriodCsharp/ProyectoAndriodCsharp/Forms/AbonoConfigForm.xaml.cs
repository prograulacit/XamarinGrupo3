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
                if (Memoria.UsuarioActual.Saldo >= Int32.Parse(PrecioTotal.Text))
                {
                    Memoria.compra.COM_FECHA_COMPRA = DateTime.Today;
                    Memoria.compra.COM_PRECIO_TOTAL = Decimal.Parse(PrecioTotal.Text);
                    Memoria.compra.COM_INTERES = (Decimal.Parse(PrecioTotal.Text));
                    int CompraID=CompraRepository.InsertAndReturn(Memoria.compra);
                    List<CompraProductos> listTemp=new List<CompraProductos>();
                    foreach (var cp in Memoria.listaCarrito) {
                        cp.COM_ID = CompraID;
                        listTemp.Add(cp);
                    }
                    Memoria.listaCarrito = listTemp;
                    IEnumerable<CompraProductos> list = Memoria.listaCarrito;
                    CompraProductosRepository.InsertAll(list);
                    Abono abono = new Abono {COM_ID=CompraID,ABO_CANTIDAD_DE_MESES=Int32.Parse(CantidadDeMeses.Text),ABO_CANTIDAD_MENSUAL=Decimal.Parse(CantidadMensual.Text),ABO_RESTANTE=(Int32.Parse(PrecioTotal.Text)-Int32.Parse(CantidadMensual.Text)) };
                    int AbonoID=AbonoRepository.InsertAndReturn(abono);
                    Memoria.UsuarioActual.Saldo -= Int32.Parse(CantidadMensual.Text);
                    AbonoPorMesRepository.InsertarAbonoPorMes(new AbonoPorMes {ABO_ID=AbonoID,ABEM_FECHA_DEPOSITADA=DateTime.Today,ABEM_CANTIDAD_DEPOSITADA= Int32.Parse(CantidadMensual.Text) });
                    UsuarioController.UpdateUser(Memoria.UsuarioActual);
                    Memoria.State = "Create";
                    Application.Current.MainPage = new NavigationPage(new Factura());
                }
                else {
                    Alert("Alerta", "No posee los fondos suficientes","Ok");
                }
            }
            else {
                //set Contado

            }
        }

        public async void Alert(string a, string b, string c) {

            await DisplayAlert(a,b,c);
        }

    }
}