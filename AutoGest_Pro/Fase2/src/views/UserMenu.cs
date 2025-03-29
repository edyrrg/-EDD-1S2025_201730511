using Fase2.src.auth;
using Fase2.src.models;
using Fase2.src.services;
using Fase2.src.views;
using Gtk;
using System;

namespace Fase2.src.views
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
            // Creando botones
            var btnRegistrarVehiculo = new Button("Registrar Vehículo");
            btnRegistrarVehiculo.Clicked += OnRegistrarVehiculoClicked;
            //btnRegistrarVehiculo.StyleContext.AddClass("button"); // Añadir clase CSS

            var btnVisualizacionServicios = new Button("Visualización de Servicios");
            btnVisualizacionServicios.Clicked += OnVisualizacionServiciosClicked;

            var btnVisualizacionFacturas = new Button("Visualizacion de Facturas");
            btnVisualizacionFacturas.Clicked += OnVisualizacionFacturasClicked;

            var btnCancelarFacturas = new Button("Cancelar Facturas");
            btnCancelarFacturas.Clicked += OnCancelarFacturasClicked;

            // Agregando componentes al contenedor
            vbox.PackStart(lblUserName, false, false, 6);
            vbox.PackStart(btnRegistrarVehiculo, false, false, 6);
            vbox.PackStart(btnVisualizacionServicios, false, false, 6);
            vbox.PackStart(btnVisualizacionFacturas, false, false, 6);
            vbox.PackStart(btnCancelarFacturas, false, false, 6);

            Add(vbox);
            ShowAll();
        }
        private void OnRegistrarVehiculoClicked(object? sender, EventArgs e)
        {
            var registrarVehiculo = new RegistrarVehiculos(this, _datasManager, _userSession);
            registrarVehiculo.Show();
            Hide();
        }
        private void OnVisualizacionServiciosClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnVisualizacionFacturasClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnCancelarFacturasClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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