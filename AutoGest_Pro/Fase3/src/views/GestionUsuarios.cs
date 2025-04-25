using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class GestionUsuarios : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private Entry _txtID;
        private Entry _txtNombres;
        private Entry _txtApellidos;
        private Entry _txtCorreo;
        private Entry _txtEdad;
        public GestionUsuarios(
                                Window contextParent,
                                DatasManager datasManager
                                ) : base("Gestión de Usuarios | AutoGest Pro", contextParent)
        {
            // Inyectamos dependencias
            _datasManager = datasManager;

            SetDefaultSize(500, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            _txtID = new Entry() { PlaceholderText = "ID" };
            _txtNombres = new Entry() { PlaceholderText = "Nombres", Sensitive = false };
            _txtApellidos = new Entry() { PlaceholderText = "Apellidos", Sensitive = false };
            _txtCorreo = new Entry() { PlaceholderText = "Correo", Sensitive = false };
            _txtEdad = new Entry() { PlaceholderText = "Edad", Sensitive = false };

            var hboxButtons = new Box(Orientation.Horizontal, 5)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var btnBuscar = new Button("Buscar");
            btnBuscar.Clicked += OnBuscarClicked;
            var btnEliminar = new Button("Eliminar");
            btnEliminar.Clicked += OnEliminarClicked;

            hboxButtons.PackStart(btnBuscar, false, false, 0);
            hboxButtons.PackStart(btnEliminar, false, false, 0);

            vbox.PackStart(_txtID, false, false, 10);
            vbox.PackStart(_txtNombres, false, false, 10);
            vbox.PackStart(_txtApellidos, false, false, 10);
            vbox.PackStart(_txtCorreo, false, false, 10);
            vbox.PackStart(_txtEdad, false, false, 10);
            vbox.PackStart(hboxButtons, false, false, 10);

            Add(vbox);
            ShowAll();
        }

        private void OnEliminarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtID.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("No es posible hacer la eliminacion de Usuario sin un ID.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }
            var confirmacion = new MessageDialog(this,
                DialogFlags.Modal, MessageType.Question,
                ButtonsType.YesNo, $"¿Está seguro de eliminar el Usuario con ID {idInt}?");
            var response = (ResponseType)confirmacion.Run();
            confirmacion.Destroy();
            if (response == ResponseType.No)
            {
                ClearEntries();
                return;
            }
            try
            {
                _datasManager._userService.DeleteUser(idInt);
                PopSucess("Usuario eliminado correctamente.");
                ClearEntries();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
                ClearEntries();
            }
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
        }
    }
}