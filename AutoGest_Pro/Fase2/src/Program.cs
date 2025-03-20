using Gtk;
using Fase2.src.views;

namespace Fase2
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            // Crear una instancia de DataService para inyectar en la vista Login
            var dataService = DataService.Instance;
            _ = new Login(dataService);
            Application.Run();
        }
    }
}
