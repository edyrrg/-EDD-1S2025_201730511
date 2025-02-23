// See https://aka.ms/new-console-template for more information

using System;
using Fase1.src.adt;
using Fase1.src.gui;
using Fase1.src.models;
using Gtk;

namespace Fase1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Application.Init();
            // _ = new Login();
            // Application.Run();
            var SimpleList = new SimpleList<int>();
            SimpleList.Insert(1, "Juan", "Perez", "jperez@mail.com", "1234");
            SimpleList.Insert(2, "Maria", "Lopez", "mlopez@mail.com", "5678");

            Console.WriteLine("Lista de Usuarios");
            SimpleList.Print();

            Console.WriteLine("Update status response: " + SimpleList.Udpate(2, "Maria", "Lopez", "mlopez@gmail.com", "5678"));

            Console.WriteLine("Lista de Usuarios");
            SimpleList.Print();

            Console.WriteLine("Update status response: " + SimpleList.Delete(2));
            
            Console.WriteLine("Lista de Usuarios");
            SimpleList.Print();

        }
    }
}

