using Fase3.src.auth;
using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class RegistrarVehiculos : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private readonly UserSession _userSession;
        private Entry _txtID;
        private Entry _txtMarca;
        private Entry _txtModelo;
        private Entry _txtPlaca;
        public RegistrarVehiculos(Window contextParent, DatasManager datasManager, UserSession userSession) : base("Registrar Vehículo | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            _userSession = userSession;

            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var grid = new Grid { ColumnSpacing = 10, RowSpacing = 10 };

            var lblID = new Label("ID:") { Halign = Align.Start };
            var lblMarca = new Label("Marca:") { Halign = Align.Start };
            var lblModelo = new Label("Modelo:") { Halign = Align.Start };
            var lblPlaca = new Label("Placa:") { Halign = Align.Start };
            _txtID = new Entry();
            _txtMarca = new Entry();
            _txtModelo = new Entry();
            _txtPlaca = new Entry();
            var btnRegistrarVehiculo = new Button("Guardar");
            btnRegistrarVehiculo.Clicked += OnRegistrarVehiculoClicked;

            grid.Attach(lblID, 0, 0, 1, 1);
            grid.Attach(_txtID, 1, 0, 1, 1);
            grid.Attach(lblMarca, 0, 1, 1, 1);
            grid.Attach(_txtMarca, 1, 1, 1, 1);
            grid.Attach(lblModelo, 0, 2, 1, 1);
            grid.Attach(_txtModelo, 1, 2, 1, 1);
            grid.Attach(lblPlaca, 0, 3, 1, 1);
            grid.Attach(_txtPlaca, 1, 3, 1, 1);
            grid.Attach(btnRegistrarVehiculo, 0, 4, 2, 1);

            vbox.Add(grid);

            Add(vbox);
            ShowAll();
        }
        private void OnRegistrarVehiculoClicked(object? sender, EventArgs e)
        {
            var id = _txtID.Text;
            var marca = _txtMarca.Text;
            var modelo = _txtModelo.Text;
            var placa = _txtPlaca.Text;
            var user = _userSession.GetUser();
            if (user == null)
            {
                PopError("Usuario no encontrado.");
                return;
            }
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo) || string.IsNullOrWhiteSpace(placa))
            {
                PopError("Todos los campos son requeridos.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El ID debe ser un número entero.");
                _txtID.Text = string.Empty;
                return;
            }
            if (!int.TryParse(modelo, out var modeloInt))
            {
                PopError("El modelo corresponde a una fecha, debe ser un número entero.");
                _txtModelo.Text = string.Empty;
                return;
            }
            try
            {
                var newVehiculo = new Vehiculo(idInt, user.ID, marca, modeloInt, placa);
                _datasManager._vehiculoService.InsertVehiculo(newVehiculo);
                PopSucess("Vehículo registrado exitosamente.");
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
            _txtMarca.Text = string.Empty;
            _txtModelo.Text = string.Empty;
            _txtPlaca.Text = string.Empty;
        }
    }
}