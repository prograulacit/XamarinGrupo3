using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using Xamarin.Forms.Xaml;

namespace ProyectoAndriodCsharp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            ProductoController.InsertarPrueba();
            IEnumerable<Producto> listProductos = ProductoController.GetAllProductos();
            if (listProductos == null) { ListNull.Text = "Lista no trae nada"; }
            else { ListNull.Text = "Lista trae cosas"; }
            int count = 0;
            foreach (var product in ProductoController.GetAllProductos())
            {
                GridAllProducts.Children.Add(new Label { Text = product.PRO_NOMBRE },0,count) ;
                GridAllProducts.Children.Add(new Label { Text = product.PRO_DESCRIPCION }, 1, count);
                GridAllProducts.Children.Add(new Label { Text = "$"+Math.Truncate(product.PRO_PRECIO).ToString() }, 1, count);
            count += 1;
            }


        }
    }
}