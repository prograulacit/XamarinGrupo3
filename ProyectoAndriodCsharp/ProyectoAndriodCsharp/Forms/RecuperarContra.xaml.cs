using ProyectoAndriodCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperarContra : ContentPage
    {
        public RecuperarContra()
        {
            InitializeComponent();
            btnRecuperar.Clicked += BtnRecuperar_Clicked;
        }

        private async void BtnRecuperar_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alerta", "Se le envió un correo electrónico con la información para recuperar su contraseña", "OK");

            await Navigation.PushAsync(new MainPage());
        }

    }
}