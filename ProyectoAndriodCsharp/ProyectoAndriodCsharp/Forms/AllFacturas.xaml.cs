using ProyectoAndriodCsharp.Controller;
using ProyectoAndriodCsharp.Model;
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
        public ObservableCollection<Compra> Facturas { get; set; }
        public ObservableCollection<Usuario> users { get; set; }
        public AllFacturas()
        {
            InitializeComponent();

            Facturas = new ObservableCollection<Compra>();

            foreach (var fact in CompraRepository.GetAllFacturas()) {
                Facturas.Add(fact);
            }
            MyListView.ItemsSource = Facturas;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
