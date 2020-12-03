using ProyectoAndriodCsharp.Forms;
using ProyectoAndriodCsharp.Objects;
using Xamarin.Forms;

namespace ProyectoAndriodCsharp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DataConnection.CreateTables();
            Tareas.CrearUsuariosIniciales();

            if(!Memoria.CaracteristicasDesarrollador)
                MainPage = new LoginRegistro();
            else
                MainPage = new DevRoom();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
