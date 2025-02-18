using Gtk;
using System;

namespace Fase1.src.gui
{
    public class MainWindow : Window
    {
        public MainWindow() : base("AutoGest Pro")
        {
            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => Application.Quit();

            var vbox = new Box(Orientation.Vertical, 0);
            var entry = new Entry();
            

            vbox.PackStart(entry, false, false, 0);

            Add(vbox);
            ShowAll();
        }
    }
}