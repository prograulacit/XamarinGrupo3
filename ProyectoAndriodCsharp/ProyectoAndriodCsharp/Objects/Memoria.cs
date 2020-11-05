using ProyectoAndriodCsharp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Objects
{
    public class Memoria
    {
        /// <summary>
        /// Guarda el usuario que actualmente se ha logeado. Ejemplo de uso en
        /// UsuarioController -> ValidarUsuario
        /// </summary>
        public static UsuarioModel UsuarioActual = null;
    }
}
