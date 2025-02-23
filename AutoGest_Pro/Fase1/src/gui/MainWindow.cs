using Gtk;
using System;
using System.Xml.Serialization;

namespace Fase1.src.gui
{
    public class MainWindow : Window
    {
        public MainWindow() : base("AutoGest Pro")
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
            btnIngresoIndividual.Clicked += OnCargaMasivaClicked;

            var btnGestionDeUsuarios = new Button("Gestión de Usuarios");
            btnGestionDeUsuarios.Clicked += OnCargaMasivaClicked;

            var btnGenerarServicio = new Button("Generar Servicio");
            btnGenerarServicio.Clicked += OnCargaMasivaClicked;

            var btnCancelarFactura = new Button("Cancelar Factura");
            btnCancelarFactura.Clicked += OnCargaMasivaClicked;

            // Agregando botones al vbox
            vbox.PackStart(btnCargaMasiva, false, false, 6);
            vbox.PackStart(btnIngresoIndividual, false, false, 6);
            vbox.PackStart(btnGestionDeUsuarios, false, false, 6);
            vbox.PackStart(btnGenerarServicio, false, false, 6);
            vbox.PackStart(btnCancelarFactura, false, false, 6);

            Add(vbox);
            ShowAll();
        }

        private static void AplicarEstilos()
        {
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromPath("src/gui/styles/styles.css");

            StyleContext.AddProviderForScreen(
                Gdk.Screen.Default,
                cssProvider,
                (uint)StyleProviderPriority.Application
            );
        }

        private void OnCargaMasivaClicked(object? sender, EventArgs e)
        {
            return;
        }

        private void OnIngresoIndividualClicked(object? sender, EventArgs e)
        {
            return;
        }

        private void OnGestionDeUsuariosClicked(object? sender, EventArgs e)
        {
            return;
        }

        private void OnGenerarServicioClicked(object? sender, EventArgs e)
        {
            return;
        }

        private void OnCancelarFacturaClicked(object? sender, EventArgs e)
        {
            return;
        }
    }
}