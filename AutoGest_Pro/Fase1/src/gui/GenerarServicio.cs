using Gtk;
namespace Fase1.src.gui
{
    public class GenerarServicio : MyWindow
    {
        private Entry _txtId;
        private Entry _txtIdRepuesto;
        private Entry _txtIdVehiculo;
        private Entry _txtDetalles;
        private Entry _txtCosto;

        public GenerarServicio(Window contextParent) : base("Generar Servicio | AutoGest Pro", contextParent)
        {
            SetDefaultSize(450, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            _txtId = new Entry() { PlaceholderText = "ID" };
            _txtIdRepuesto = new Entry() { PlaceholderText = "ID Repuesto" };
            _txtIdVehiculo = new Entry() { PlaceholderText = "ID Vehiculo" };
            _txtDetalles = new Entry() { PlaceholderText = "Detalles" };
            _txtCosto = new Entry() { PlaceholderText = "Costo" };

            var btnGenerar = new Button("Generar");
            btnGenerar.Clicked += OnGenerarClicked;

            vbox.PackStart(_txtId, false, false, 5);
            vbox.PackStart(_txtIdRepuesto, false, false, 5);
            vbox.PackStart(_txtIdVehiculo, false, false, 5);
            vbox.PackStart(_txtDetalles, false, false, 5);
            vbox.PackStart(_txtCosto, false, false, 5);
            vbox.PackStart(btnGenerar, false, false, 20);

            Add(vbox);
            ShowAll();
        }

        private void OnGenerarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtId.Text;
            var idRepuesto = _txtIdRepuesto.Text;
            var idVehiculo = _txtIdVehiculo.Text;
            var detalles = _txtDetalles.Text;
            var costo = _txtCosto.Text;

            return;
        }
    }
}