using Fase1.src.models;
using Fase1.src.services;
using Gtk;

namespace Fase1.src.gui
{
    public class GestionUsuarios : MyWindow
    {
        private readonly DataService _DataService;
        private Entry _txtId;
        private Entry _txtNombres;
        private Entry _txtApellidos;
        private Entry _txtCorreo;
        private TextView _txtvVehiculos;
        public GestionUsuarios(Window contextParent, DataService dataService) : base("Gestion de Usuarios | AutoGest Pro", contextParent)
        {
            // Inyección de dependencias
            _DataService = dataService;

            SetSizeRequest(900, 500);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            AplicarEstilos();

            var hbox = new Box(Orientation.Horizontal, 20)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var grid = new Grid() { ColumnSpacing = 8, RowSpacing = 8 };

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Start,
                Valign = Align.Center
            };

            var lblId = new Label("ID") { Halign = Align.Start };
            var lblNombres = new Label("Nombres") { Halign = Align.Start };
            var lblApellidos = new Label("Apellidos") { Halign = Align.Start };
            var lblCorreo = new Label("Correo") { Halign = Align.Start };

            _txtId = new Entry() { PlaceholderText = "ID" };
            _txtNombres = new Entry() { PlaceholderText = "Nombres", Sensitive = false };
            _txtApellidos = new Entry() { PlaceholderText = "Apellidos", Sensitive = false };
            _txtCorreo = new Entry() { PlaceholderText = "Correo", Sensitive = false };

            var btnBuscar = new Button("Buscar");
            btnBuscar.Clicked += OnBuscarClicked;

            var btnModificar = new Button("Modificar");
            btnModificar.Clicked += OnModificarClicked;

            var btnActualizar = new Button("Actualizar");
            btnActualizar.Clicked += OnActualizarClicked;

            var btnEliminar = new Button("Eliminar");
            btnEliminar.Clicked += OnEliminarClicked;

            grid.Attach(lblId, 0, 0, 1, 1);
            grid.Attach(_txtId, 1, 0, 1, 1);
            grid.Attach(btnBuscar, 2, 0, 1, 1);
            grid.Attach(lblNombres, 0, 1, 1, 1);
            grid.Attach(_txtNombres, 1, 1, 2, 1);
            grid.Attach(lblApellidos, 0, 2, 1, 1);
            grid.Attach(_txtApellidos, 1, 2, 2, 1);
            grid.Attach(lblCorreo, 0, 3, 1, 1);
            grid.Attach(_txtCorreo, 1, 3, 2, 1);
            grid.Attach(btnModificar, 0, 4, 1, 1);
            grid.Attach(btnActualizar, 1, 4, 1, 1);
            grid.Attach(btnEliminar, 2, 4, 1, 1);

            vbox.Add(grid);

            _txtvVehiculos = new TextView() { Editable = false, WrapMode = WrapMode.Word };
            _txtvVehiculos.Buffer.Text = "Hello World!\nVehiculos del Usuario";
            _txtvVehiculos.SetSizeRequest(300, 150);
            _txtvVehiculos.VscrollPolicy = ScrollablePolicy.Natural;

            hbox.PackStart(vbox, true, true, 0);
            hbox.PackStart(_txtvVehiculos, true, true, 0);

            Add(hbox);

            ShowAll();
        }

        private void OnBuscarClicked(object? sender, EventArgs e)
        {
            var id = _txtId.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("El campo ID no puede estar vacío.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }

            try
            {
                Usuario usuario = _DataService.BuscarUsuario(idInt);
                List<Vehiculo>? vehiculos = _DataService.BuscarVehiculoPorUsuario(idInt);
                if (vehiculos.Count > 0)
                {
                    _txtvVehiculos.Buffer.Text = $"Vehiculos de {usuario.Nombres} {usuario.Apellidos}\n\n";
                    foreach (var vehiculo in vehiculos)
                    {
                        _txtvVehiculos.Buffer.Text += $"[ ID: {vehiculo.ID}, Marca: {vehiculo.Marca}, Modelo: {vehiculo.Modelo}, Placa: {vehiculo.Placa} ]\n\n";
                    }
                }
                else
                {
                    _txtvVehiculos.Buffer.Text = "Vehiculos del Usuario\nNo hay vehiculos registrados.";
                }
                _txtNombres.Text = usuario.Nombres;
                _txtApellidos.Text = usuario.Apellidos;
                _txtCorreo.Text = usuario.Correo;
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }

        private void OnActualizarClicked(object? sender, EventArgs e)
        {
            var id = _txtId.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("El campo ID no puede estar vacío.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }

            var nombres = _txtNombres.Text;
            if (string.IsNullOrEmpty(nombres))
            {
                PopError("El campo Nombres no puede estar vacío.");
                return;
            }

            var apellidos = _txtApellidos.Text;
            if (string.IsNullOrEmpty(apellidos))
            {
                PopError("El campo Apellidos no puede estar vacío.");
                return;
            }

            var correo = _txtCorreo.Text;
            if (string.IsNullOrEmpty(correo))
            {
                PopError("El campo Correo no puede estar vacío.");
                return;
            }

            try
            {
                _DataService.ActualizarUsuario(idInt, nombres, apellidos, correo);
                PopSucess("Usuario actualizado correctamente.");
                ClearEntries();
                _txtvVehiculos.Buffer.Text = "Hello World!\nVehiculos del Usuario";
                EnableViews(false);
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            var id = _txtId.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("El campo ID no puede estar vacío.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }

            try
            {
                _DataService.EliminarUsuario(idInt);
                PopSucess("Usuario eliminado correctamente.");
                ClearEntries();
                _txtvVehiculos.Buffer.Text = "Hello World!\nVehiculos del Usuario";
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        private void OnModificarClicked(object? sender, EventArgs e)
        {
            EnableViews(true);
            PopSucess("Modo de edición activado.");
        }
        private void EnableViews(bool enable)
        {
            _txtId.Sensitive = !enable;
            _txtNombres.Sensitive = enable;
            _txtApellidos.Sensitive = enable;
            _txtCorreo.Sensitive = enable;
        }
        private void ClearEntries()
        {
            _txtId.Text = "";
            _txtNombres.Text = "";
            _txtApellidos.Text = "";
            _txtCorreo.Text = "";
        }   
    }
}