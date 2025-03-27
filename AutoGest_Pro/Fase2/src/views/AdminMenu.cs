using Fase2.src.models;
using Fase2.src.services;
using Gtk;
using System;

namespace Fase2.src.views
{
    public class AdminMenu : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private readonly LogHistorySessionService _logHistorySessionService;
        private readonly LogHistorySession _logHistorySession;

        public AdminMenu(
                        Window contextParent,
                        DatasManager datasManager,
                        LogHistorySession logHistorySession,
                        LogHistorySessionService logHistorySessionService
                        ) : base("Menu de Administrador | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            _logHistorySessionService = logHistorySessionService;
            _logHistorySession = logHistorySession;

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

            var btnGestionEntidades = new Button("Gestion Entidades");
            btnGestionEntidades.Clicked += OnGestionEntidadesClicked;

            var btnActualizarRepuestos = new Button("Actualización de Repuestos");
            btnActualizarRepuestos.Clicked += OnActualizacionRepuestosClicked;

            var btnViewRepuestos = new Button("Visualización de Repuestos");
            btnViewRepuestos.Clicked += OnViewServiciosClicked;

            var btnGenerarServicios = new Button("Generar Servicios");
            btnGenerarServicios.Clicked += OnGenerarServiciosClicked;

            var btnControlLogeo = new Button("Control de Logeo");
            btnControlLogeo.Clicked += OnControlLogeoClicked;

            var btnGenerarReportes = new Button("Generar Reportes");
            btnGenerarReportes.Clicked += OnGenerarReportesClicked;

            // Agregando botones al vbox
            vbox.PackStart(btnCargaMasiva, false, false, 6);
            vbox.PackStart(btnGestionEntidades, false, false, 6);
            vbox.PackStart(btnActualizarRepuestos, false, false, 6);
            vbox.PackStart(btnViewRepuestos, false, false, 6);
            vbox.PackStart(btnGenerarServicios, false, false, 6);
            vbox.PackStart(btnControlLogeo, false, false, 6);
            vbox.PackStart(btnGenerarReportes, false, false, 6);

            Add(vbox);
            ShowAll();
        }

        private void OnCargaMasivaClicked(object? sender, EventArgs e)
        {
            var cargaMasiva = new CargaMasiva(this, _datasManager);
            cargaMasiva.ShowAll();
            Hide();
        }

        private void OnGestionEntidadesClicked(object? sender, EventArgs e)
        {
            var GestionEntidadesMenu = new GestionEntidadesMenu(this, _datasManager);
            GestionEntidadesMenu.ShowAll();
            Hide();
        }

        private void OnActualizacionRepuestosClicked(object? sender, EventArgs e)
        {
            // var GestionUsuarios = new GestionUsuarios(this, _DataService);
            // GestionUsuarios.ShowAll();
            // Hide();
        }

        private void OnGenerarServiciosClicked(object? sender, EventArgs e)
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

        public void OnViewServiciosClicked(object? sender, EventArgs e)
        {
            // var ViewServicios = new ViewServicios(this, _DataService);
            // ViewServicios.ShowAll();
            // Hide();
        }

        public void OnControlLogeoClicked(object? sender, EventArgs e)
        {
            var filename = "LogHistorySessions.json";
            _logHistorySessionService.ExportToJsonAndSave(filename);
            Console.WriteLine($"Archivo JSON exportado correctamente: ./Reportes/{filename}");
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
        override
        public void OnDeleteEvent()
        {
            base.OnDeleteEvent();
            var dateNow = DateTime.UtcNow;
            string utcFormattedDate = dateNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); // Formato ISO 8601 en UTC
            _logHistorySession.Salida = utcFormattedDate;
            _logHistorySessionService.AddLogHistorySession(_logHistorySession);
        }
    }
}