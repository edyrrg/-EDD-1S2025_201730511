using Fase1.src.models;
using Fase1.src.services;
using Gtk;

namespace Fase1.src.gui
{
    public class IngresoUsuario : MyWindow
    {
        private readonly DataService _DataService;
        private Entry _txtId;
        private Entry _txtNombre;
        private Entry _txtApellido;
        private Entry _txtCorreo;
        private Entry _txtContrasenia;

        public IngresoUsuario(Window contextParent, DataService dataService) : base("Ingreso Individual | AutoGest Pro", contextParent)
        {
            // Inyectamos dataService
            _DataService = dataService;

            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            _txtId = new Entry() { PlaceholderText = "ID" };
            _txtNombre = new Entry() { PlaceholderText = "Nombres" };
            _txtApellido = new Entry() { PlaceholderText = "Apellidos" };
            _txtCorreo = new Entry() { PlaceholderText = "Correo" };
            _txtContrasenia = new Entry() { PlaceholderText = "Contraseña" };

            var btnIngresar = new Button("Ingresar");
            btnIngresar.Clicked += OnIngresarClicked;

            vbox.PackStart(_txtId, false, false, 5);
            vbox.PackStart(_txtNombre, false, false, 5);
            vbox.PackStart(_txtApellido, false, false, 5);
            vbox.PackStart(_txtCorreo, false, false, 5);
            vbox.PackStart(_txtContrasenia, false, false, 5);
            vbox.PackStart(btnIngresar, false, false, 20);

            Add(vbox);
            ShowAll();
        }
        private void OnIngresarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtId.Text;
            var nombre = _txtNombre.Text;
            var apellido = _txtApellido.Text;
            var correo = _txtCorreo.Text;
            var contrasena = _txtContrasenia.Text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                PopError("Por favor, llene todos los campos");
                return;
            }
            try
            {
                _DataService.IngresarUsuario(new Usuario
                {
                    ID = int.Parse(id),
                    Nombres = nombre,
                    Apellidos = apellido,
                    Correo = correo,
                    Contrasenia = contrasena
                });
                PopSucess("Usuario ingresado exitosamente");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            finally
            {
                _txtId.Text = "";
                _txtNombre.Text = "";
                _txtApellido.Text = "";
                _txtCorreo.Text = "";
                _txtContrasenia.Text = "";
            }
        }
    }
}