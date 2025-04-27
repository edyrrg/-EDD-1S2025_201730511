using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class VisualizacionUsuarios : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private Entry _txtID;
        private Entry _txtNombres;
        private Entry _txtApellidos;
        private Entry _txtCorreo;
        private Entry _txtEdad;
        private Entry _txtContrasenia;
        public VisualizacionUsuarios(
                                Window contextParent,
                                DatasManager datasManager
                                ) : base("Visualización de Usuarios | AutoGest Pro", contextParent)
        {
            // Inyectamos dependencias
            _datasManager = datasManager;

            SetDefaultSize(500, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var grid = new Grid
            {
                RowSpacing = 10,
                ColumnSpacing = 10
            };

            var lblID = new Label("ID:") { Halign = Align.Start };
            var lblNombres = new Label("Nombres:") { Halign = Align.Start };
            var lblApellidos = new Label("Apellidos:") { Halign = Align.Start };
            var lblCorreo = new Label("Correo:") { Halign = Align.Start };
            var lblEdad = new Label("Edad:") { Halign = Align.Start };
            var lblContrasenia = new Label("Contraseña:") { Halign = Align.Start };

            _txtID = new Entry();
            _txtNombres = new Entry() { Sensitive = false };
            _txtApellidos = new Entry() { Sensitive = false };
            _txtCorreo = new Entry() { Sensitive = false };
            _txtEdad = new Entry() { Sensitive = false };
            _txtContrasenia = new Entry() { Sensitive = false };

            var btnBuscar = new Button("Buscar");
            btnBuscar.Clicked += OnBuscarClicked;

            grid.Attach(lblID, 0, 0, 1, 1);
            grid.Attach(_txtID, 1, 0, 3, 1);
            grid.Attach(lblNombres, 0, 1, 1, 1);
            grid.Attach(_txtNombres, 1, 1, 3, 1);
            grid.Attach(lblApellidos, 0, 2, 1, 1);
            grid.Attach(_txtApellidos, 1, 2, 3, 1);
            grid.Attach(lblCorreo, 0, 3, 1, 1);
            grid.Attach(_txtCorreo, 1, 3, 3, 1);
            grid.Attach(lblEdad, 0, 4, 1, 1);
            grid.Attach(_txtEdad, 1, 4, 3, 1);
            grid.Attach(lblContrasenia, 0, 5, 1, 1);
            grid.Attach(_txtContrasenia, 1, 5, 3, 1);
            grid.Attach(btnBuscar, 1, 6, 3, 1);

            vbox.Add(grid);

            Add(vbox);
            ShowAll();
        }

        private void OnBuscarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtID.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("No se posible hacer la busqueda sin un ID.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }
            try
            {
                Usuario userFound = _datasManager._userService.FindUserById(idInt);
                _txtNombres.Text = userFound.Nombres;
                _txtApellidos.Text = userFound.Apellidos;
                _txtCorreo.Text = userFound.Correo;
                _txtEdad.Text = userFound.Edad.ToString();
                _txtContrasenia.Text = userFound.Contrasenia;
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
                ClearEntries();
            }
        }
        private void ClearEntries()
        {
            _txtID.Text = "";
            _txtNombres.Text = "";
            _txtApellidos.Text = "";
            _txtCorreo.Text = "";
            _txtEdad.Text = "";
            _txtContrasenia.Text = "";
        }
    }
}