using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Factura : ContentPage
    {
        decimal cobroTotal = 0;
        Usuario user = UsuarioController.GetUserByID(CompraRepository.GetCompraByID(Memoria.DinamicValue).US_ID);
        Compra compra = CompraRepository.GetCompraByID(Memoria.DinamicValue);
        Abono abono = AbonoRepository.GetAbonoByCompraID(Memoria.DinamicValue);
        public Factura()
        {
            InitializeComponent();
            inicializacion();
        }

        private void inicializacion()
        {
            lblFacturaID.Text += compra.COM_ID.ToString();
            lblCliente.Text = "Cliente: " + user.Nombre;
            lblEmail.Text += user.Email;
            lblEstado.Text += compra.COM_ESTADO;
            lblFecha.Text += compra.COM_FECHA_COMPRA.ToString("dd/MM/yyyy");
            if (!string.IsNullOrWhiteSpace(user.Email))
                lblEmail.Text += user.Email;
            else
                lblEmail.Text += "Correo no registrado";


            int contador = 0;

            if (Memoria.State.Equals("Create"))
            {

                foreach (var product in CompraProductosRepository.GetAllCPByCompraID(Memoria.DinamicValue))
                {
                    listaProductos.Children.Add(new Label { Text = ProductoRepository.GetProductoByID(product.PRO_ID).PRO_NOMBRE, TextColor = Color.Black }, 0, contador);
                    listaProductos.Children.Add(new Label { Text = "c/u $" + Math.Truncate(ProductoRepository.GetProductoByID(product.PRO_ID).PRO_PRECIO).ToString(), TextColor = Color.Black }, 1, contador);
                    listaProductos.Children.Add(new Label { Text = "Cantidad: " + product.COMP_CANTIDAD.ToString(), TextColor = Color.Black }, 2, contador);
                    cobroTotal += ((ProductoRepository.GetProductoByID(product.PRO_ID).PRO_PRECIO) * product.COMP_CANTIDAD);
                    contador++;
                }
            }

            if (Memoria.State.Equals("See"))
            {
                //Leer desde DB
                if (CompraProductosRepository.GetAllCPByCompraID(Memoria.DinamicValue).FirstOrDefault() == null)
                    listaProductos.Children.Add(new Label { Text = "No hay productos registrados a esta factura.", TextColor = Color.Black }, 0, contador);
                else
                {
                    foreach (var cp in CompraProductosRepository.GetAllCPByCompraID(Memoria.DinamicValue))
                    {
                    }
                }
            }

            lblTotal.Text = "Total: $" + cobroTotal;
            lblMontoAbono.Text = "Próximo Abono es de: $" + abono.ABO_CANTIDAD_MENSUAL;
            if (abono.ABO_CANTIDAD_MENSUAL <= 0)
            {
                lblMontoAbono.Text = "Monto Cancelado";
            }
            lblFechaGarantia.Text += compra.COM_FECHA_COMPRA.AddYears(1).ToString("dd/MM/yyyy");
            btnSalir.Clicked += BtnSalir_Clicked;
        }

        private void BtnSalir_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPrincipalCliente());
        }

        private void Btn_ConfirmarCompra(object sender, EventArgs e)
        {
            if (RealizarCompra() && ReducirSaldoDeUsuario())
            {
                // Ejecucion de compra realizada con exito.
                Memoria.listaCarrito = new List<CompraProductos>();
                Application.Current.MainPage = new NavigationPage(new CompraRealizada());
            }
            else
            {
                MensajeEmergente("Error", "Ha ocurrido un error. Contacte al administrador.", "OK");
            }
        }

        private bool ReducirSaldoDeUsuario()
        {
            Memoria.UsuarioActual.Saldo -= cobroTotal;
            UsuarioController usuarioController = new UsuarioController(DataConnection.GetConnectionPath());
            return usuarioController.ActualizarUsuario(Memoria.UsuarioActual) != 0;
        }

        // Retorna true si la compra es exitosa.
        private bool RealizarCompra()
        {
            if (SaldoSuficiente())
            {
                int COM_ID = RegistrarCompra(); // Guarda registro de Compra realizada en la base de datos.
                Console.WriteLine(COM_ID);
                if (COM_ID == -1)
                {
                    return false; // Error al guardar registro de Compra.
                }
                return RegistrarCompraProducto(COM_ID); // Registros guardados exitosamente.
            }
            return false;
        }

        private bool RegistrarCompraProducto(int CompraId)
        {
            foreach (var item in Memoria.listaCarrito)
            { // Guarda n cantidad de registros de CompraProductos con mismo ID de Compra.
                CompraProductos compraProductos = new CompraProductos()
                {
                    COM_ID = CompraId, // Mismo ID de compra.
                    PRO_ID = item.PRO_ID,
                    COMP_CANTIDAD = item.COMP_CANTIDAD
                };
                // Error al guardar el registro de CompraProductos.
                if (!CompraProductosRepository.IngresarCP(compraProductos))
                {
                    return false;
                }
            }
            return true;
        }

        private int RegistrarCompra()
        {
            Compra compra = new Compra()
            {
                US_ID = Memoria.UsuarioActual.UsuarioId,
                COM_FECHA_COMPRA = DateTime.Now,
                COM_ESTADO = "obsoleto",
                COM_INTERES = 0,
                COM_PRECIO_TOTAL = cobroTotal
            };
            return CompraRepository.IngresarCompraRetornarID(compra);
        }

        private bool SaldoSuficiente()
        {
            if (Memoria.UsuarioActual.Saldo >= cobroTotal) return true;
            else
            {
                MensajeEmergente("Saldo insuficiente"
                    , "No cuenta con suficientes fondos para realizar la compra."
                    , "OK");
                return false;
            }
        }

        private async void MensajeEmergente(string titulo, string cuerpo, string mensajeBoton)
        {
            await DisplayAlert(titulo, cuerpo, mensajeBoton);
        }
    }
}