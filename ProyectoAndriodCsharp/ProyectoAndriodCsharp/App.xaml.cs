using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Forms;
using ProyectoAndriodCsharp.Model;
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
            //DataConnection.DropTables();

            DataConnection.CreateTables();
            //Usuario usuario = new Usuario {Nombre="Edu",US_ROL="Usuario",Contrasenia="123",NombreUsuario="user" };
            //Usuario administrador = new Usuario { Nombre = "Eduardo", US_ROL = "Administrador", Contrasenia = "123", NombreUsuario = "admin" };
            //UsuarioController.IngresarUsuario(usuario);
            //UsuarioController.IngresarUsuario(administrador);
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
