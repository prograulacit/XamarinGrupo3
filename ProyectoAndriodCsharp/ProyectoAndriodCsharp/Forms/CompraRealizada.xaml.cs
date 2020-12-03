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
    public partial class CompraRealizada : ContentPage
    {
        public CompraRealizada()
        {
            InitializeComponent();
        }

        private void Button_Volver(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
        }

        private void Button_MisCompras(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MisCompras());
        }
    }
}