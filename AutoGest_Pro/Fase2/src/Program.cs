using Gtk;
using Fase2.src.views;
using Fase2.src.models;
using Fase2.src.adts;

namespace Fase2
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            // Crear una instancia de DataService para inyectar en la vista Login
            // var dataService = DataService.Instance;
            // _ = new Login(dataService);
            // Application.Run();
            var simpleList = new SimpleLinkedList<Usuario>();
            Console.WriteLine("Insertando usuarios...");
            simpleList.Insert(new Usuario(1, "Juan", "Perez", "jperez@mail.com", "25", "1234"));
            simpleList.Insert(new Usuario(2, "Maria", "Lopez", "mlopez@mail.com", "30", "5678"));
            simpleList.Insert(new Usuario(3, "Pedro", "Gomez", "pgomez@mail.com", "35", "91011"));
            Console.WriteLine("Imprimiendo lista...");
            simpleList.Print();
            Console.WriteLine("Actualizando usuario...");
            simpleList.Update(new Usuario(2, "Maria", "Lopez", "mlopez@gmail.com", "30", "5678"));
            Console.WriteLine("Imprimiendo lista...");
            simpleList.Print();
            Console.WriteLine("Eliminando usuario...");
            simpleList.DeleteById(1);
            Console.WriteLine("Imprimiendo lista...");
            simpleList.Print();
        }
    }
}
