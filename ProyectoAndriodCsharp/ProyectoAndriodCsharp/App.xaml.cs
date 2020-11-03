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
            DataConnection.CreateTables();
            // DataConnection.DropTables();
            MainPage = new TestRoom();
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
