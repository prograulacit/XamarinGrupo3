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
    public partial class PagarAbono : ContentPage
    {
        public Compra compra = CompraRepository.GetSiguientePorPagarByUserID(Memoria.UsuarioActual.UsuarioId);
        public Abono abono = AbonoRepository.GetAbonoByCompraID(CompraRepository.GetSiguientePorPagarByUserID(Memoria.UsuarioActual.UsuarioId).COM_ID);
        public PagarAbono()
        {
            InitializeComponent();
            CompraID.Text = compra.COM_ID.ToString();
            CompraSiguientePago.Text = compra.COM_SIGUIENTE_PAGO.Day.ToString() + "/" + compra.COM_SIGUIENTE_PAGO.Month.ToString() + "/" + compra.COM_SIGUIENTE_PAGO.Year.ToString();
            CompraTotal.Text = compra.COM_PRECIO_TOTAL.ToString();
            AbonoPagarEsteMes.Text = abono.ABO_CANTIDAD_MENSUAL.ToString();
            //TODO   AbonoCantidadMesesRestantes.Text =0;
            AbonoRestante.Text = abono.ABO_RESTANTE.ToString();

        }

        private void Pagar_Clicked(object sender, EventArgs e)
        {
            if (Memoria.UsuarioActual.Saldo >= abono.ABO_CANTIDAD_MENSUAL)
            {
                //Proceder a pagar
                //Set values for Update
                Memoria.UsuarioActual.Saldo -= abono.ABO_CANTIDAD_MENSUAL;
                AbonoPorMes abonoPorMes = new AbonoPorMes { ABO_ID=abono.ABO_ID,ABEM_CANTIDAD_DEPOSITADA=abono.ABO_CANTIDAD_MENSUAL,ABEM_FECHA_DEPOSITADA=DateTime.Today};
                abono.ABO_RESTANTE -= abono.ABO_CANTIDAD_MENSUAL;
                compra.COM_SIGUIENTE_PAGO = compra.COM_SIGUIENTE_PAGO.AddMonths(1);
                UsuarioController.UpdateUser(Memoria.UsuarioActual);
                //Updates
                AbonoRepository.UpdateAbono(abono);
                AbonoPorMesRepository.InsertarAbonoPorMes(abonoPorMes);
                CompraRepository.UpdateCompra(compra);
                Memoria.UsuarioActual = UsuarioController.GetUserByID(Memoria.UsuarioActual.UsuarioId);
                Alert("Realizado","Abono realizado con exito","Ok");
                Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
            }
            else {
                Alert("Alerta","No posee los fondos suficientes","Ok");
            }
        }
        public async void Alert(string a, string b, string c) {
            await DisplayAlert(a,b,c);
        }

        private void Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AllAbonosPendientes());
        }
    }
}