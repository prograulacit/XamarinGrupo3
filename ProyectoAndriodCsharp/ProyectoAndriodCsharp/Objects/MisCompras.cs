using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoAndriodCsharp.Objects
{
    /**
     * Contiene el contenido de los modelos:
     * Compra, CompraProductos y Producto para mostrar la informacion
     * en pantalla.
     */

    public class MisCompras
    {
        public List<Compra> lista_Compra { get; set; }
        public List<CompraProductos> lista_CompraProductos { get; set; }
        public List<Producto> lista_Producto { get; set; }

        public MisCompras(int usuarioId)
        {
            lista_CompraProductos = new List<CompraProductos>();
            lista_Producto = new List<Producto>();

            // Carga registros de Compra.
            lista_Compra = CompraRepository.GetAllComprasByUserId(usuarioId).ToList();

            // Carga registros de CompraProducto.
            List<CompraProductos> lista_CompraProductos_temporal =
                new List<CompraProductos>();

            foreach (var registroCompra in lista_Compra)
            {
                lista_CompraProductos_temporal = CompraProductosRepository.GetAllCPByCompraID(registroCompra.COM_ID).ToList();

                foreach (var CompraProducto in lista_CompraProductos_temporal)
                {
                    lista_CompraProductos.Add(CompraProducto);
                }
            }

            // Carga registros de Producto.
            foreach (var CompraProductos in lista_CompraProductos)
            {
                lista_Producto.Add(ProductoRepository.GetProductoByID(CompraProductos.PRO_ID));
            }
        }

        // Retorna un string con todos los detalles para ver las compras de un usuario
        // para hacer pruebas.
        public string GetEstructura_1()
        {
            string str = "";
            if (lista_Compra.Count > 0)
            {
                foreach (var Compra in lista_Compra)
                {
                    str += "Compra " + Compra.COM_ID + "\r\n" +
                        "Fecha: " + Compra.COM_FECHA_COMPRA + "\r\n" +
                        "Monto total: " + Compra.COM_PRECIO_TOTAL + "\r\n\r\n";

                    foreach (var CompraProductos in lista_CompraProductos)
                    {
                        if (CompraProductos.COM_ID == Compra.COM_ID)
                        {
                            foreach (var Productos in lista_Producto)
                            {
                                if (CompraProductos.PRO_ID == Productos.PRO_ID)
                                {
                                    str += Productos.PRO_NOMBRE + ". Precio: " + Productos.PRO_PRECIO + "\r\n" +
                                        Productos.PRO_DESCRIPCION + ". Cantidad adquirido/a: " + CompraProductos.COMP_CANTIDAD + "\r\n\r\n";
                                }
                            }
                        }
                    }
                }
            }
            else
                str = "No ha realizado ninguna compra.";
            return str;
        }
    }
}
