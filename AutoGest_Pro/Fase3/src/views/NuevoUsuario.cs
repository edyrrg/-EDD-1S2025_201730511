using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class NuevoUsuario : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private Entry _txtID;
        private Entry _txtNombres;
        private Entry _txtApellidos;
        private Entry _txtCorreo;
        private Entry _txtEdad;
        private Entry _txtContrasenia;
        public NuevoUsuario(
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
            _txtNombres = new Entry();
            _txtApellidos = new Entry();
            _txtCorreo = new Entry();
            _txtEdad = new Entry();
            _txtContrasenia = new Entry();

            var btnInsertar = new Button("Insertar");
            btnInsertar.Clicked += OnInsertarClicked;

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
            grid.Attach(btnInsertar, 1, 6, 3, 1);

            vbox.Add(grid);

            Add(vbox);
            ShowAll();
        }

        private void OnInsertarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtID.Text;
            var nombres = _txtNombres.Text;
            var apellidos = _txtApellidos.Text;
            var correo = _txtCorreo.Text;
            var edad = _txtEdad.Text;
            var contrasenia = _txtContrasenia.Text;

            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(edad) ||
                string.IsNullOrWhiteSpace(contrasenia))
            {
                PopError("Todos los campos son obligatorios.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                _txtID.Text = string.Empty;
                return;
            }
            if (!int.TryParse(edad, out var edadInt))
            {
                PopError("El campo Edad debe ser un número entero.");
                _txtEdad.Text = string.Empty;
                return;
            }
            if (contrasenia.Length < 3)
            {
                PopError("La contraseña debe tener al menos 3 caracteres.");
                return;
            }
            try
            {
                var newUsuario = new Usuario(idInt, nombres, apellidos, correo, edadInt, contrasenia);
                _datasManager._userService.InsertUser(newUsuario);
                PopSucess("Usuario registrado exitosamente.");
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
            _txtID.Text = "";
            _txtNombres.Text = "";
            _txtApellidos.Text = "";
            _txtCorreo.Text = "";
            _txtEdad.Text = "";
            _txtContrasenia.Text = "";
        }
    }
}