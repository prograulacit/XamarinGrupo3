using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllFacturas : ContentPage
    {
        public AllFacturas()
        {
            if (CompraRepository.GetCompraByID(1) == null) { CompraRepository.InsertPrueba(); }
            InitializeComponent();
            if (Memoria.UsuarioActual.US_ROL.Equals("Administrador")) { }
            if (Memoria.UsuarioActual.US_ROL.Equals("Usuario")) { }
            
            int count = 1;

            foreach (var factura in CompraRepository.GetAllFacturas())
            {
                DinamicButton dinamicButton = new DinamicButton();
                dinamicButton.DinamicValue = factura.COM_ID;
                dinamicButton.Text = "Ver";
                //Function DescripcionFactura
                GridAllCompras.Children.Add(new Label { Text = factura.COM_FECHA_COMPRA.ToString() }, 0, count);
                GridAllCompras.Children.Add(new Label { Text = "$" + Math.Truncate(factura.COM_PRECIO_TOTAL).ToString() }, 1, count);
                GridAllCompras.Children.Add(dinamicButton, 3, count);
                count += 1;
            }

        }

       
    }
}
