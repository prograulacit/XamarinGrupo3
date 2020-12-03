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
            CantidadMensual.Text = ((Int32.Parse(PrecioTotal.Text)/Int32.Parse(CantidadDeMeses.Text)) * 1.03).ToString();
        }

        private void cb_Abono_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (cb_Abono.IsChecked == true) { ConfigAbono.IsVisible = true; }
            else { ConfigAbono.IsVisible = false; }

        }
    }
}