using Gtk;
using Fase2.src.views;
using Fase2.src.services;
using Fase2.src.auth;

namespace Fase2
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            // Crear una instancia de DataService para inyectar en la vista Login
            var datasManager = DatasManager.Instance;
            // Crear una instancia de UserSession para inyectar en la vista Login
            var userSession = UserSession.Instance;
            // Crear una instancia de LogHistorySessionService para inyectar en la vista Login
            var logHistorySessionService = LogHistorySessionService.Instance;
            _ = new Login(datasManager, userSession, logHistorySessionService);
            Application.Run();
        }
    }
}
