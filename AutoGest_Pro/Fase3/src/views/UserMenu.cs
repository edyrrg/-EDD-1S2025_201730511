using Fase3.src.auth;
using Fase3.src.models;
using Fase3.src.services;
using Fase3.src.views;
using Gtk;
using System;

namespace Fase3.src.views
{
    public class UserMenu : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private readonly UserSession _userSession;
        private readonly LogHistorySessionService _logHistorySessionService;
        private readonly LogHistorySession _logHistorySession;

        public UserMenu(Window contextParent,
                        DatasManager datasManager,
                        UserSession userSession,
                        LogHistorySession logHistorySession,
                        LogHistorySessionService logHistorySessionService
                        ) : base("Menu | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            _userSession = userSession;
            _logHistorySessionService = logHistorySessionService;
            _logHistorySession = logHistorySession;

            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            // Aplicando estilos
            AplicarEstilos();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var lblUserName = new Label($"Bienvenido {_userSession.GetUser()?.Nombres}");
            lblUserName.StyleContext.AddClass("title-user");

            var btnVisualizacionVehiculos = new Button("Visualizar mis Vehículos");
            btnVisualizacionVehiculos.Clicked += OnVisualizarVehiculosClicked;

            var btnVisualizacionServicios = new Button("Visualización de Servicios");
            btnVisualizacionServicios.Clicked += OnVisualizacionServiciosClicked;

            var btnVisualizacionFacturas = new Button("Visualizacion de Facturas");
            btnVisualizacionFacturas.Clicked += OnVisualizacionFacturasClicked;

            var btnCancelarFacturas = new Button("Cancelar Facturas");
            btnCancelarFacturas.Clicked += OnCancelarFacturasClicked;

            // Agregando componentes al contenedor
            vbox.PackStart(lblUserName, false, false, 6);
            vbox.PackStart(btnVisualizacionVehiculos, false, false, 6);
            vbox.PackStart(btnVisualizacionServicios, false, false, 6);
            vbox.PackStart(btnVisualizacionFacturas, false, false, 6);
            vbox.PackStart(btnCancelarFacturas, false, false, 6);

            Add(vbox);
            ShowAll();
        }
        private void OnVisualizarVehiculosClicked(object? sender, EventArgs e)
        {
            var visualizacionVehiculos = new VisualizacionVehiculos(this, _datasManager, _userSession);
            visualizacionVehiculos.Show();
            Hide();
        }
        private void OnVisualizacionServiciosClicked(object? sender, EventArgs e)
        {
            var visualizacionServicios = new VisualizacionServicios(this, _datasManager, _userSession);
            visualizacionServicios.Show();
            Hide();
        }
        private void OnVisualizacionFacturasClicked(object? sender, EventArgs e)
        {
            var visualizacionFacturas = new VisualizacionFacturas(this, _datasManager, _userSession);
            visualizacionFacturas.Show();
            Hide();
        }
        private void OnCancelarFacturasClicked(object? sender, EventArgs e)
        {
            var cancelarFactura = new CancelarFactura(this, _datasManager, _userSession);
            cancelarFactura.Show();
            Hide();
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
                PopSucess("Sesión cerrada correctamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
    }
}