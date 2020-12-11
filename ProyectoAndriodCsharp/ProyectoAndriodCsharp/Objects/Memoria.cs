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

        //Para diferenciar si un form es para la creacion de un objeto o su actualizacion, se utiliza el "State" para diferenciarlo, valores posibles "See" o "Create"
        public static string State = "";

        // Si es true, abre el menu formulario DevRoom al inicio.
        public static bool CaracteristicasDesarrollador = false;



        //Factura temporal
        public static Compra compra=new Compra();


        public static void ResetParameters() {
            UsuarioActual = null;
            DinamicValue = 0;
            listaCarrito = new List<CompraProductos>();
            State = "";
            CaracteristicasDesarrollador = false;
        
        }
    }
}
