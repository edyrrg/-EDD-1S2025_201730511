// See https://aka.ms/new-console-template for more information

using System;
using Fase1.src.adt;
using Fase1.src.gui;
using Fase1.src.models;
using Fase1.src.services;
using Gtk;

namespace Fase1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Application.Init();
            var dataService = DataService.Instance;
            _ = new Login(dataService);
            Application.Run();

            // var SimpleList = new SimpleList<int>();
            // SimpleList.Insert(1, "Juan", "Perez", "jperez@mail.com", "1234");
            // SimpleList.Insert(2, "Maria", "Lopez", "mlopez@mail.com", "5678");

            // Console.WriteLine("Lista de Usuarios");
            // SimpleList.Print();

            // Console.WriteLine("Update status response: " + SimpleList.Udpate(2, "Maria", "Lopez", "mlopez@gmail.com", "5678"));

            // Console.WriteLine("Lista de Usuarios");
            // SimpleList.Print();

            // Console.WriteLine("Update status response: " + SimpleList.Delete(2));

            // Console.WriteLine("Lista de Usuarios");
            // SimpleList.Print();

            // var DoubleLinkedList = new DoubleLinkedList<int>();
            // DoubleLinkedList.Insert(1, 1, "Toyota", "Corolla", "P123ABC");
            // DoubleLinkedList.Insert(2, 1, "Nissan", "Sentra", "P456DEF");
            // DoubleLinkedList.Insert(3, 2, "Honda", "Civic", "P789GHI");
            // DoubleLinkedList.Print();

            // Console.WriteLine("Search status response: " + DoubleLinkedList.Search(2));

            // var tmp = DoubleLinkedList.SearchVehiclesByUserId(1);
            // tmp?.ForEach(vehicle => Console.WriteLine(vehicle.Marca));
        }
    }
}
