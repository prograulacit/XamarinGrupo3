using ProyectoAndriodCsharp.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoAndriodCsharp.Objects
{
    public partial class DinamicButton:Button
    {
        public int DinamicValue { get; set; }
        
        
        public void MyButton_Click(object sender, EventArgs e)
        {
            Memoria.ProductoID=DinamicValue;
            Application.Current.MainPage = new NavigationPage(new DescripcionProducto());
        }
    }
}
