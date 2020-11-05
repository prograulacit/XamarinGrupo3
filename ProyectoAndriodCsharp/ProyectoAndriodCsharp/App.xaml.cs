using ProyectoAndriodCsharp.Forms;
using ProyectoAndriodCsharp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // DataConnection.DropTables();

            DataConnection.CreateTables();
            MainPage = new LoginRegistro();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
