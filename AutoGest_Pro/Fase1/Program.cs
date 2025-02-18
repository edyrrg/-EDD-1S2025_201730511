// See https://aka.ms/new-console-template for more information

using System;
using Fase1.src.gui;
using Gtk;

namespace Fase1
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            _ = new Login();
            Application.Run();
        }
    }
}

