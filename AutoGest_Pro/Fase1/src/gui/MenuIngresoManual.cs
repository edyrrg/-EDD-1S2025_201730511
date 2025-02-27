using System.Xml.Serialization;
using Gtk;

namespace Fase1.src.gui
{
    public class MenuIngresoManual : MyWindow
    {
        /**
         * Init the window
         */
        public MenuIngresoManual(Window contextParent) : base("Menu | AutoGest Pro", contextParent)
        {
            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var btnIngresoUsuario = new Button("Ingresar Nuevo Usuario");
            btnIngresoUsuario.Clicked += OnIngresoUsuario;

            var btnIngresoVehiculo = new Button("Ingresar Nuevo Vehiculo");
            btnIngresoVehiculo.Clicked += OnIngresoVehiculo;

            var btnIngresoRepuesto = new Button("Ingresar Nuevo Repuesto");
            btnIngresoRepuesto.Clicked += OnIngresarRepuesto;

            var btnIngresoServicio = new Button("Ingresar Nuevo Servicio");
            btnIngresoServicio.Clicked += OnIngresarServicio;

            vbox.PackStart(btnIngresoUsuario, false, false, 10);
            vbox.PackStart(btnIngresoVehiculo, false, false, 10);
            vbox.PackStart(btnIngresoRepuesto, false, false, 10);
            vbox.PackStart(btnIngresoServicio, false, false, 10);

            Add(vbox);
            ShowAll();
        }

        private void OnIngresoUsuario(object? sender, System.EventArgs e)
        {
            var ingresoUsuario = new IngresoUsuario(this);
            ingresoUsuario.ShowAll();
            Hide();
        }

        private void OnIngresoVehiculo(object? sender, System.EventArgs e)
        {
            var ingresoVehiculo = new IngresoVehiculo(this);
            ingresoVehiculo.ShowAll();
            Hide();
        }

        private void OnIngresarRepuesto(object? sender, System.EventArgs e)
        {
            var ingresoRepuesto = new IngresoRepuesto(this);
            ingresoRepuesto.ShowAll();
            Hide();
        }

        private void OnIngresarServicio(object? sender, System.EventArgs e)
        {
            var ingresoServicio = new IngresoServicio(this);
            ingresoServicio.ShowAll();
            Hide();
        }
    }
}