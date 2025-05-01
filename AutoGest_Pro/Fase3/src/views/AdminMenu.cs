using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
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

            SetDefaultSize(475, 400);
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

            var btnVisualizacionUsuarios = new Button("Visualización de Usuarios");
            btnVisualizacionUsuarios.Clicked += OnVisualizacionUsuariosClicked;

            var btnNuevoUsuario = new Button("Agregar nuevo Usuario");
            btnNuevoUsuario.Clicked += OnAgregarNuevoUsuariosClicked;

            var btnViewRepuestos = new Button("Visualización de Repuestos");
            btnViewRepuestos.Clicked += OnViewRepuestosClicked;

            var btnGenerarServicios = new Button("Generar Servicios");
            btnGenerarServicios.Clicked += OnGenerarServiciosClicked;

            var btnControlLogeo = new Button("Control de Inicios de Sesión");
            btnControlLogeo.Clicked += OnControlLogeoClicked;

            var btnGenerarReportes = new Button("Generar Reportes");
            btnGenerarReportes.Clicked += OnGenerarReportesClicked;

            var btnGuardarBackups = new Button("Generar y Guardar Backups");
            btnGuardarBackups.Clicked += OnGuardarBackupsClicked;

            // Agregando botones al vbox
            vbox.PackStart(btnCargaMasiva, false, false, 6);
            vbox.PackStart(btnNuevoUsuario, false, false, 6);
            vbox.PackStart(btnVisualizacionUsuarios, false, false, 6);
            vbox.PackStart(btnViewRepuestos, false, false, 6);
            vbox.PackStart(btnGenerarServicios, false, false, 6);
            vbox.PackStart(btnControlLogeo, false, false, 6);
            vbox.PackStart(btnGenerarReportes, false, false, 6);
            vbox.PackStart(btnGuardarBackups, false, false, 6);

            Add(vbox);
            ShowAll();
        }

        private void OnGuardarBackupsClicked(object? sender, EventArgs e)
        {
            try
            {
                _datasManager._userService.GuardarBakckup();
                PopSucess("Backup de Usuarios generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            
        }

        private void OnCargaMasivaClicked(object? sender, EventArgs e)
        {
            var cargaMasiva = new CargaMasiva(this, _datasManager);
            cargaMasiva.ShowAll();
            Hide();
        }

        private void OnVisualizacionUsuariosClicked(object? sender, EventArgs e)
        {
            var visualizacionUsuarios = new VisualizacionUsuarios(this, _datasManager);
            visualizacionUsuarios.ShowAll();
            Hide();
        }

        private void OnAgregarNuevoUsuariosClicked(object? sender, EventArgs e)
        {
            var nuevoUsuario = new NuevoUsuario(this, _datasManager);
            nuevoUsuario.ShowAll();
            Hide();
        }

        private void OnViewRepuestosClicked(object? sender, EventArgs e)
        {
            var VisualizacionRepuestos = new VisualizacionRepuestos(this, _datasManager);
            VisualizacionRepuestos.ShowAll();
            Hide();
        }
        private void OnGenerarServiciosClicked(object? sender, EventArgs e)
        {
            var GenerarServicios = new GenerarServicios(this, _datasManager);
            GenerarServicios.ShowAll();
            Hide();
        }

        public void OnControlLogeoClicked(object? sender, EventArgs e)
        {
            var filename = "LogHistorySessions.json";
            _logHistorySessionService.ExportToJsonAndSave(filename);
            Console.WriteLine($"Archivo JSON exportado correctamente: ./Reportes/{filename}");
        }

        private void OnGenerarReportesClicked(object? sender, EventArgs e)
        {
            try
            {
                _datasManager._userService.GenerateReport();
                PopSucess("Reporte de Usuarios generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            try
            {
                _datasManager._vehiculoService.GenerateReport();
                PopSucess("Reporte de Vehiculos generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            try
            {
                _datasManager._repuestoService.GenerateReport();
                PopSucess("Reporte de Repuestos generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            try
            {
                _datasManager._servicioService.GenerateReport();
                PopSucess("Reporte de Servicios generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            try
            {
                _datasManager._facturaService.GenerateReport();
                PopSucess("Reporte de Facturas generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }

            try
            {
                _datasManager._grafoService.GenerateReport();
                PopSucess("Reporte de Grafo generado correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        override
        public void OnDeleteEvent()
        {
            base.OnDeleteEvent();
            try
            {
                var dateNow = DateTime.UtcNow;
                string utcFormattedDate = dateNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); // Formato ISO 8601 en UTC
                _logHistorySession.Salida = utcFormattedDate;
                _logHistorySessionService.AddLogHistorySession(_logHistorySession);
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }

        }
    }
}