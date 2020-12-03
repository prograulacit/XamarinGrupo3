using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using System;

namespace ProyectoAndriodCsharp.Objects
{
    public class Tareas
    {
        /// <summary>
        /// Genera un numero aleatorio entre el minimo y maximo 
        /// dado.
        /// </summary>
        /// <param name="min">Minimo de número aleatorio.</param>
        /// <param name="max">Máximo de número aleatorio.</param>
        /// <returns>int -> Número aleatorio.</returns>
        public static int GenerarNumeroAleatorio(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public static void CrearUsuariosIniciales()
        {
            Usuario usuario = new Usuario
            {
                Nombre = "Edu",
                US_ROL = "Usuario",
                Contrasenia = "123",
                NombreUsuario = "user",
                Email = "edu@mail.com",
                Saldo = Tareas.GenerarNumeroAleatorio(1000, 20000)
            };

            Usuario administrador = new Usuario
            {
                Nombre = "Eduardo",
                US_ROL = "Administrador",
                Contrasenia = "123",
                NombreUsuario = "admin"
            };

            if (UsuarioController.GetUsuarioByUsername("admin") == null)
                UsuarioController.IngresarUsuario(administrador);
            if (UsuarioController.GetUsuarioByUsername("user") == null)
                UsuarioController.IngresarUsuario(usuario);
        }
    }
}
