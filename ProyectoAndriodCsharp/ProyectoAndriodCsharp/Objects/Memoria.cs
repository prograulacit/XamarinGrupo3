using ProyectoAndriodCsharp.Model;
using System.Collections.Generic;

namespace ProyectoAndriodCsharp.Objects
{
    public class Memoria
    {
        /// <summary>
        /// Guarda el usuario que actualmente se ha logeado. Ejemplo de uso en
        /// UsuarioController -> ValidarUsuario
        /// </summary>
        public static Usuario UsuarioActual = null;
        public static int DinamicValue = 0;
        public static List<CompraProductos> listaCarrito = new List<CompraProductos>();

        // Sin documentación escrita.
        public static string State = "";

        // Si es true, abre el menu formulario DevRoom al inicio.
        public static bool CaracteristicasDesarrollador = false;
    }
}
