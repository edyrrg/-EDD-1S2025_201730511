using Gtk;
using Fase2.src.views;
using Fase2.src.services;
using Fase2.src.auth;
using Fase2.src.adts;
using Fase2.src.models;

namespace Fase2
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var btree = new BTree();
            btree.Insertar(new Factura(1, 1, 100));
            btree.Insertar(new Factura(2, 2, 200));
            btree.Insertar(new Factura(3, 3, 300));
            btree.Insertar(new Factura(4, 4, 400));
            btree.Insertar(new Factura(5, 5, 500));
            btree.Insertar(new Factura(6, 6, 600));
            btree.Insertar(new Factura(7, 7, 700));
            btree.Insertar(new Factura(8, 8, 800));
            btree.Insertar(new Factura(9, 9, 900));
            btree.Insertar(new Factura(10, 10, 1000));
            btree.Insertar(new Factura(11, 11, 1100));
            btree.Insertar(new Factura(12, 12, 1200));
            btree.Insertar(new Factura(13, 13, 1300));
            btree.Insertar(new Factura(14, 14, 1400));
            btree.Insertar(new Factura(15, 15, 1500));
            btree.Insertar(new Factura(16, 16, 1600));
            btree.Insertar(new Factura(17, 17, 1700));

            var factura = btree.Buscar(5);

            Console.WriteLine($"Factura encontrada: {factura?.Id}, {factura?.IdServicio}, {factura?.Total}");

            var texto = btree.GraficarGraphviz();

            Console.WriteLine(texto); */

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
