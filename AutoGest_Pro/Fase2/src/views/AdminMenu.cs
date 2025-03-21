using Fase2.src.services;
using Gtk;
using System;

namespace Fase2.src.views
{
    public class AdminMenu : CustomWindow
    {
        private readonly DatasManager _datasManager;

        public AdminMenu(Window contextParent, DatasManager datasManager) : base("Menu de Administrador | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;

            SetDefaultSize(450, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

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

            var btnGenerarReportes = new Button("Generar Reportes");
            btnGenerarReportes.Clicked += OnGenerarReportesClicked;

            // Agregando botones al vbox
            vbox.PackStart(btnCargaMasiva, false, false, 6);
            vbox.PackStart(btnIngresoIndividual, false, false, 6);
            vbox.PackStart(btnGestionDeUsuarios, false, false, 6);
            vbox.PackStart(btnGenerarServicio, false, false, 6);
            vbox.PackStart(btnCancelarFactura, false, false, 6);
            vbox.PackStart(btnGenerarReportes, false, false, 6);

            Add(vbox);
            ShowAll();
        }

        private void OnCargaMasivaClicked(object? sender, EventArgs e)
        {
            // var cargaMasiva = new CargaMasiva(this, _DataService);
            // cargaMasiva.ShowAll();
            // Hide();
        }

        private void OnIngresoIndividualClicked(object? sender, EventArgs e)
        {
            // var MenuIngresoManual = new MenuIngresoManual(this, _DataService);
            // MenuIngresoManual.ShowAll();
            // Hide();
        }

        private void OnGestionDeUsuariosClicked(object? sender, EventArgs e)
        {
            // var GestionUsuarios = new GestionUsuarios(this, _DataService);
            // GestionUsuarios.ShowAll();
            // Hide();
        }

        private void OnGenerarServicioClicked(object? sender, EventArgs e)
        {
            // var GenerarServicio = new GenerarServicio(this, _DataService);
            // GenerarServicio.ShowAll();
            // Hide();
        }

        private void OnCancelarFacturaClicked(object? sender, EventArgs e)
        {
            // try
            // {
            //     var factura = _DataService.CancelarFactura();
            //     PopSucess($"Factura Cancelada/Pagada\nID: {factura?.ID}\nOrden: {factura?.IdOrden}\nTotal: {factura?.Total}");
            // }
            // catch (Exception ex)
            // {
            //     PopError(ex.Message);
            // }
        }

        private void OnGenerarReportesClicked(object? sender, EventArgs e)
        {
            // try
            // {
            //     _DataService.GenerarReporteListadoUsuarios();
            //     PopSucess("Reporte de usuarios generado correctamente");
            // }
            // catch (Exception ex)
            // {
            //     PopError(ex.Message);
            // }
            // try
            // {
            //     _DataService.GenerarReporteListadoVehiculos();
            //     PopSucess("Reporte de vehículos generado correctamente");
            // }
            // catch (Exception ex)
            // {
            //     PopError(ex.Message);
            // }
            // try
            // {
            //     _DataService.GenerarReporteListadoRepuestos();
            //     PopSucess("Reporte de repuestos generado correctamente");
            // }
            // catch (Exception ex)
            // {
            //     PopError(ex.Message);
            // }
            // try
            // {
            //     _DataService.GenerarReporteListadoServicios();
            //     PopSucess("Reporte de servicios generado correctamente");
            // }
            // catch (Exception ex)
            // {
            //     PopError(ex.Message);
            // }
            // try
            // {
            //     _DataService.GenerarReporteListadoFacturas();
            //     PopSucess("Reporte de facturas generado correctamente");
            // }
            // catch (Exception ex)
            // {
            //     PopError(ex.Message);
            // }
        }
    }
}