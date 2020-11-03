using ProyectoAndriodCsharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrarse : ContentPage
    {
        public Registrarse()
        {
            InitializeComponent();
            btnRegistrarse.Clicked += BtnRegistrarse_ClickedAsync;

        }

        private async void BtnRegistrarse_ClickedAsync(object sender, EventArgs e)
        {
            await DisplayAlert("Alerta", "Registro Exitoso", "OK");

            await Navigation.PushAsync(new MainPage());
        }
    }
}