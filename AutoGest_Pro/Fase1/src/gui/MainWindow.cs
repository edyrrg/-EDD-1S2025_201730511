using Gtk;
using System;

namespace Fase1.src.gui
{
    public class MainWindow : MyWindow
    {
        public MainWindow() : base("Menu Principal | AutoGest Pro")
        {
            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => Application.Quit();

            // Aplicando estilos
            AplicarEstilos();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };


            // Creando botones
            var btnCargaMasiva = new Button("Carga Masiva");
            btnCargaMasiva.Clicked += OnCargaMasivaClicked;
            btnCargaMasiva.StyleContext.AddClass("button"); // Añadir clase CSS

            var btnIngresoIndividual = new Button("Ingreso Individual");
            btnIngresoIndividual.Clicked += OnIngresoIndividualClicked;

            var btnGestionDeUsuarios = new Button("Gestión de Usuarios");
            btnGestionDeUsuarios.Clicked += OnGestionDeUsuariosClicked;

            var btnGenerarServicio = new Button("Generar Servicio");
            btnGenerarServicio.Clicked += OnGenerarServicioClicked;

            var btnCancelarFactura = new Button("Cancelar Factura");
            btnCancelarFactura.Clicked += OnCancelarFacturaClicked;

            // Agregando botones al vbox
            vbox.PackStart(btnCargaMasiva, false, false, 6);
            vbox.PackStart(btnIngresoIndividual, false, false, 6);
            vbox.PackStart(btnGestionDeUsuarios, false, false, 6);
            vbox.PackStart(btnGenerarServicio, false, false, 6);
            vbox.PackStart(btnCancelarFactura, false, false, 6);

            Add(vbox);
            ShowAll();
        }

        private void OnCargaMasivaClicked(object? sender, EventArgs e)
        {
            var cargaMasiva = new CargaMasiva(this);
            cargaMasiva.ShowAll();
            Hide();
        }

        private void OnIngresoIndividualClicked(object? sender, EventArgs e)
        {
            var MenuIngresoManual = new MenuIngresoManual(this);
            MenuIngresoManual.ShowAll();
            Hide();
        }

        private void OnGestionDeUsuariosClicked(object? sender, EventArgs e)
        {
            var GestionUsuarios = new GestionUsuarios(this);
            GestionUsuarios.ShowAll();
            Hide();
        }

        private void OnGenerarServicioClicked(object? sender, EventArgs e)
        {
            var GenerarServicio = new GenerarServicio(this);
            GenerarServicio.ShowAll();
            Hide();
        }

        private void OnCancelarFacturaClicked(object? sender, EventArgs e)
        {
            var dialog = new MessageDialog(this,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                "Funcionalidad no implementada | Facturación\n\nID:\t\t\t1\nID Orden:\t\t1\nTotal:\t\t200");
            dialog.Run();
            dialog.Destroy();
            return;
        }
    }
}