using Gtk;
using Fase3.src.views;
using Fase3.src.services;
using Fase3.src.auth;
using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();

            blockchain.AddBlock(new Usuario(1, "Juan", "Pérez", "jperez@mail.com", 30, "password123"));
            blockchain.AddBlock(new Usuario(2, "María", "Gómez", "mgomez@mail.com", 25, "mypassword"));
            blockchain.AddBlock(new Usuario(3, "Carlos", "López", "clopez@mail.com", 28, "123456"));

            Console.WriteLine($"Encontrado por email y contraseña: {blockchain.SearchByEmailAndPass("jperez@mail.com", "password123")}");

            Console.WriteLine($"Encontrado por id: {blockchain.FindUserById(2)}");

            try
            {
                blockchain.GenerateReport();
            }
            catch (System.Exception)
            {

                Console.Error.WriteLine("Error al generar el grafo.");
            }

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
