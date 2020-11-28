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
            InitializeComponent();
            int count = 1;
            if (CompraRepository.GetCompraByID(7) == null) { CompraRepository.InsertarPrueba(); }


            foreach (var compra in CompraRepository.GetAllFacturas()) {
                DinamicButton dinamicButton = new DinamicButton();
                dinamicButton.DinamicValue = compra.COM_ID;
                dinamicButton.Text = "Ver";
                dinamicButton.Clicked += new EventHandler(dinamicButton.SeeFactura);
                GridAllFacturas.Children.Add(new Label { Text = compra.COM_FECHA_COMPRA.ToString() }, 0, count);
                GridAllFacturas.Children.Add(new Label { Text = "$"+Math.Truncate(compra.COM_PRECIO_TOTAL).ToString() }, 1, count);
                GridAllFacturas.Children.Add(dinamicButton, 2, count);
                count += 1;
            }
            
        }

        
    }
}
