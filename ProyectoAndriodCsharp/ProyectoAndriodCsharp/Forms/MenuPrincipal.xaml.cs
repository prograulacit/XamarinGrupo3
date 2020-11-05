using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
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
            //IEnumerable<Producto> listProductos = ProductoController.GetAllProductos();
            //ggpz.Text= listProductos.First().PRO_NOMBRE;
            IEnumerable<Producto> listProductos = ProductoController.GetAllProductos();
            



        }
    }
}