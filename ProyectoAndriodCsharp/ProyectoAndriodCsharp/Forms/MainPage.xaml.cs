using Base;
using ProyectoAndriodCsharp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoAndriodCsharp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnIniciarSesion.Clicked += Btn_IniciarSesion_Clicked;
            btnRecContra.Clicked += Btn_RecContra_Clicked;
            btnRegistrarse.Clicked += Btn_Registrarse_Clicked;
        }

        void Btn_IniciarSesion_Clicked(object sender, EventArgs args)
        {
            ((NavigationPage)this.Parent).PushAsync(new Forms.MenuPrincipal());
        }

        void Btn_RecContra_Clicked(object sender, EventArgs args)
        {
            ((NavigationPage)this.Parent).PushAsync(new RecuperarContra());

        }

        void Btn_Registrarse_Clicked(object sender, EventArgs args)
        {
            ((NavigationPage)this.Parent).PushAsync(new TestRoom());

        }

    }
}
