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

            var btnActualizar = new Button("Actualizar");

            var btnEliminar = new Button("Eliminar");

            grid.Attach(lblId, 0, 0, 1, 1);
            grid.Attach(_txtId, 1, 0, 1, 1);
            grid.Attach(btnBuscar, 2, 0, 1, 1);
            grid.Attach(lblNombres, 0, 1, 1, 1);
            grid.Attach(_txtNombres, 1, 1, 2, 1);
            grid.Attach(lblApellidos, 0, 2, 1, 1);
            grid.Attach(_txtApellidos, 1, 2, 2, 1);
            grid.Attach(lblCorreo, 0, 3, 1, 1);
            grid.Attach(_txtCorreo, 1, 3, 2, 1);
            grid.Attach(btnActualizar, 0, 4, 1, 1);
            grid.Attach(btnEliminar, 1, 4, 1, 1);

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
                _txtNombres.Text = usuario.Nombres;
                _txtApellidos.Text = usuario.Apellidos;
                _txtCorreo.Text = usuario.Correo;
                EnableViews(true);
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        private void EnableViews(bool enable)
        {
            _txtNombres.Sensitive = enable;
            _txtApellidos.Sensitive = enable;
            _txtCorreo.Sensitive = enable;
        }
    }
}