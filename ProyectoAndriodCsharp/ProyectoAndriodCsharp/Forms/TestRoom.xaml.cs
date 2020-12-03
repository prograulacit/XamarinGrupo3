using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Controller;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoAndriodCsharp.Objects;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestRoom : ContentPage
    {
        public TestRoom()
        {
            InitializeComponent();
        }

        private void RegistrarUsuario(object sender, EventArgs e)
        {
            Usuario UsuarioNuevo = new Usuario()
            {
                NombreUsuario = entry_nombreUsuario.Text,
                Nombre = entry_nombre.Text,
                Email = entry_Email.Text,
                Contrasenia = entry_Contrasenia.Text
            };
            UsuarioController usuarioController =
                new UsuarioController(DataConnection.GetConnectionPath());
            usuarioController.InsertarUsuario(UsuarioNuevo);
        }

        private void MostrarUsuarios(object sender, EventArgs e)
        {
            UsuarioController usuarioController =
                new UsuarioController(DataConnection.GetConnectionPath());

            IEnumerable<Usuario> lista_usuarios = usuarioController.GetAllUsuarios();
            label_listaUsuarios.Text = "";
            foreach (var itemUsuario in lista_usuarios)
            {
                string info = @"ID: "+itemUsuario.UsuarioId+". Nombre: " + itemUsuario.UsuarioId +
                    ". Nombre usuario: " + itemUsuario.NombreUsuario +
                    ". Email: " + itemUsuario.Email +
                    ". Contraseña: " + itemUsuario.Contrasenia + ".\n\n";
                label_listaUsuarios.Text += info;
            }
        }

        private void EliminarTablas(object sender, EventArgs e)
        {
            DataConnection.DropTables();
            DataConnection.CreateTables();
        }

        private void EjecutarSELECTLast_inserted_rowid(object sender, EventArgs e)
        {
            
        }
    }
}