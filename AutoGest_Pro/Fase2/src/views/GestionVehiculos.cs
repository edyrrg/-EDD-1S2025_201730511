using Fase2.src.services;
using Gtk;

namespace Fase2.src.views
{
    public class GestionVehiculos : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private Entry _txtID;
        private Entry _txtIdUsuario;
        private Entry _txtMarca;
        private Entry _txtModelo;
        private Entry _txtPlaca;
        public GestionVehiculos(
                                Window contextParent,
                                DatasManager datasManager
                                ) : base("Gestión de Vehiculos | AutoGest Pro", contextParent)
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
            _txtIdUsuario = new Entry() { PlaceholderText = "ID Usuario", Sensitive = false };
            _txtMarca = new Entry() { PlaceholderText = "Marca", Sensitive = false };
            _txtModelo = new Entry() { PlaceholderText = "Modelo", Sensitive = false };
            _txtPlaca = new Entry() { PlaceholderText = "Placa", Sensitive = false };

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
            vbox.PackStart(_txtIdUsuario, false, false, 10);
            vbox.PackStart(_txtMarca, false, false, 10);
            vbox.PackStart(_txtModelo, false, false, 10);
            vbox.PackStart(_txtPlaca, false, false, 10);
            vbox.PackStart(hboxButtons, false, false, 10);

            Add(vbox);
            ShowAll();
        }



        private void OnBuscarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtID.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("No es posible hacer la eliminacion de Vehiculo sin un ID.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }

            try
            {
                var vehiculo = _datasManager._vehiculoService.FindVehiculoById(idInt);
                _txtIdUsuario.Text = vehiculo.ID_Usuario.ToString();
                _txtMarca.Text = vehiculo.Marca;
                _txtModelo.Text = vehiculo.Modelo.ToString();
                _txtPlaca.Text = vehiculo.Placa;
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
                ClearEntries();
            }
        }

        private void OnEliminarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtID.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("No es posible hacer la eliminacion de Vehiculo sin un ID.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }

            // Confirmación de eliminación
            var confirmacion = new MessageDialog(this,
                DialogFlags.Modal, MessageType.Question,
                ButtonsType.YesNo, $"¿Está seguro de eliminar el Vehiculo con ID {idInt}?");
            var response = (ResponseType)confirmacion.Run();
            confirmacion.Destroy();
            if (response == ResponseType.No)
            {
                ClearEntries();
                return;
            }

            try
            {
                _datasManager._vehiculoService.DeleteVehiculo(idInt);
                PopSucess("Vehiculo eliminado correctamente.");
                ClearEntries();
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
            _txtIdUsuario.Text = "";
            _txtMarca.Text = "";
            _txtModelo.Text = "";
            _txtPlaca.Text = "";
        }
    }
}