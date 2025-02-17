// See https://aka.ms/new-console-template for more information

using System;
using Gtk;

class Program
{
    static void Main(string[] args)
    {
        Application.Init();

        var win = new Window("Hello World");
        win.SetDefaultSize(200, 200);
        win.DeleteEvent += (_, _) => Application.Quit();

        var label = new Label("Hello World");
        win.Add(label);

        win.ShowAll();

        Application.Run();
    }
}
