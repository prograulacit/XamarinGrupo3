using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescripcionProducto : ContentPage
    {
        public DescripcionProducto()
        {
            InitializeComponent();
            Producto producto = ProductoController.GetProductoByID(Memoria.ProductoID);
        }
    }
}