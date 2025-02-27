using Gtk;

namespace Fase1.src.gui
{
    public class IngresoUsuario : MyWindow
    {
        private Entry _txtId;
        private Entry _txtNombre;
        private Entry _txtApellido;
        private Entry _txtCorreo;
        private Entry _txtContrasenia;

        public IngresoUsuario(Window contextParent) : base("Ingreso Individual | AutoGest Pro", contextParent)
        {
            // Pasando referencia de la ventana principal
            // _contextMain = contextMain;

            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            _txtId = new Entry(){ PlaceholderText = "ID"};
            _txtNombre = new Entry(){ PlaceholderText = "Nombre"};
            _txtApellido = new Entry(){ PlaceholderText = "Apellido"};
            _txtCorreo = new Entry(){ PlaceholderText = "Correo"};
            _txtContrasenia = new Entry(){ PlaceholderText = "Contraseña"};

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
            var nombre = _txtNombre.Text;
            var apellido = _txtApellido.Text;
            var correo = _txtCorreo.Text;
            var contrasena = _txtContrasenia.Text;

            return;
        }
    }
}