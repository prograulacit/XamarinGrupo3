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
    public partial class AllAbonosPendientes : ContentPage
    {
        public AllAbonosPendientes()
        {
            InitializeComponent();
            IEnumerable<Compra> compras = CompraRepository.GetAllComprasAbonosActivosByUserID(Memoria.UsuarioActual.UsuarioId);
            int count = 2;
            if (compras == null)
            {
                lblInit.Text = "No hay Facturas con abonos Activos";
                btnInit.IsEnabled = false;
                btnInit.IsVisible = false;
            }
            else {
                lblInit.IsVisible = false;
                btnInit.IsVisible = true;
            }
            foreach (var compra in compras)
            {
                DinamicButton dinamicButton = new DinamicButton();
                dinamicButton.DinamicValue = compra.COM_ID;
                dinamicButton.Text = "Ver";
                dinamicButton.Clicked += new EventHandler(dinamicButton.SeeFactura);
                AllFacturasAbonoActivo.Children.Add(new Label { Text = compra.COM_ID.ToString() }, 0, count);
                AllFacturasAbonoActivo.Children.Add(new Label { Text = AbonoRepository.GetAbonoByCompraID(compra.COM_ID).ABO_RESTANTE.ToString() },1,count);
                AllFacturasAbonoActivo.Children.Add(new Label { Text = "$" + Math.Truncate(compra.COM_PRECIO_TOTAL).ToString() }, 2, count);
                AllFacturasAbonoActivo.Children.Add(dinamicButton, 3, count);
                count += 1;
            }
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
        }

        private void btnInit_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new PagarAbono());
        }
    }
}