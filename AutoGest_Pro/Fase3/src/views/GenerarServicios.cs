using Fase3.src.services;
using Gtk;
using Fase3.src.models;

namespace Fase3.src.views
{
    public class GenerarServicios : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private Entry _txtID;
        private Entry _txtIdRepuesto;
        private Entry _txtIdVehiculo;
        private Entry _txtDetalles;
        private Entry _txtCosto;
        public GenerarServicios(Window contextParent, DatasManager datasManager) : base("Crear Servicio | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            SetDefaultSize(500, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var grid = new Grid { ColumnSpacing = 10, RowSpacing = 10 };

            var lblID = new Label("ID:") { Halign = Align.Start };
            var lblIdRepuesto = new Label("ID Repuesto:") { Halign = Align.Start };
            var lblIdVehiculo = new Label("ID Vehiculo:") { Halign = Align.Start };
            var lblDetalles = new Label("Detalles:") { Halign = Align.Start };
            var lblCosto = new Label("Costo:") { Halign = Align.Start };

            _txtID = new Entry();
            _txtIdRepuesto = new Entry();
            _txtIdVehiculo = new Entry();
            _txtDetalles = new Entry();
            _txtCosto = new Entry();

            var btnCrear = new Button("Guardar");
            btnCrear.Clicked += OnCrearServicioClicked;

            grid.Attach(lblID, 0, 0, 1, 1);
            grid.Attach(_txtID, 1, 0, 1, 1);
            grid.Attach(lblIdRepuesto, 0, 1, 1, 1);
            grid.Attach(_txtIdRepuesto, 1, 1, 1, 1);
            grid.Attach(lblIdVehiculo, 0, 2, 1, 1);
            grid.Attach(_txtIdVehiculo, 1, 2, 1, 1);
            grid.Attach(lblDetalles, 0, 3, 1, 1);
            grid.Attach(_txtDetalles, 1, 3, 1, 1);
            grid.Attach(lblCosto, 0, 4, 1, 1);
            grid.Attach(_txtCosto, 1, 4, 1, 1);
            grid.Attach(btnCrear, 0, 5, 2, 1);

            vbox.Add(grid);
            Add(vbox);
            ShowAll();
        }

        private void OnCrearServicioClicked(object? sender, EventArgs e)
        {
            var id = _txtID.Text;
            var idRepuesto = _txtIdRepuesto.Text;
            var idVehiculo = _txtIdVehiculo.Text;
            var costo = _txtCosto.Text;
            // Validar campos y tipos
            if (string.IsNullOrEmpty(_txtID.Text) || string.IsNullOrEmpty(_txtIdRepuesto.Text) || string.IsNullOrEmpty(_txtIdVehiculo.Text) || string.IsNullOrEmpty(_txtDetalles.Text) || string.IsNullOrEmpty(_txtCosto.Text))
            {
                PopError("Todos los campos son requeridos.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El ID debe ser un número entero.");
                return;
            }
            if (!int.TryParse(idRepuesto, out var idRepuestoInt))
            {
                PopError("El ID Repuesto debe ser un número entero.");
                return;
            }
            if (!int.TryParse(idVehiculo, out var idVehiculoInt))
            {
                PopError("El ID Vehiculo debe ser un número entero.");
                return;
            }
            if (!float.TryParse(costo, out var costoDecimal))
            {
                PopError("El costo debe ser un número decimal.");
                return;
            }
            // Validar existencia de repuesto y vehiculo
            if (!_datasManager._vehiculoService.SearchVehiculoById(idVehiculoInt))
            {
                PopError($"El vehiculo con ID {idVehiculoInt} no existe.");
                return;
            }

            if (!_datasManager._repuestoService.SearchRepuestoById(idRepuestoInt))
            {
                PopError($"El repuesto con ID {idRepuestoInt} no existe.");
                return;
            }
            
            try
            {
                var respuesto = _datasManager._repuestoService.FindRepuestoByID(idRepuestoInt);
                var costoTotal = costoDecimal + respuesto.Costo;
                var servicio = new Servicio(idInt, idRepuestoInt, idVehiculoInt, _txtDetalles.Text, costoDecimal);
                _datasManager._servicioService.InsertServicio(servicio);
                PopSucess("Servicio creado exitosamente.");
                _datasManager._facturaService.InsertFactura(servicio.ID, costoTotal);
                ClearEntries();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
                _txtID.Text = string.Empty;
            }
        }

        private void ClearEntries()
        {
            _txtID.Text = string.Empty;
            _txtIdRepuesto.Text = string.Empty;
            _txtIdVehiculo.Text = string.Empty;
            _txtDetalles.Text = string.Empty;
            _txtCosto.Text = string.Empty;
        }
    }
}