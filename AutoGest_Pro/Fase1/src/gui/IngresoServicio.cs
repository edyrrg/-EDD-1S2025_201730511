using Gtk;

namespace Fase1.src.gui
{
    public class IngresoServicio : MyWindow
    {
        private Entry _txtId;
        private Entry _txtIdRepuesto;
        private Entry _txtIdVehiculo;
        private Entry _txtDetaller;
        private Entry _txtCosto;

        public IngresoServicio(Window contextParent) : base("Ingreso Individual | AutoGest Pro", contextParent)
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

            _txtIdRepuesto = new Entry(){ PlaceholderText = "ID Repuesto"};
            _txtIdVehiculo = new Entry(){ PlaceholderText = "ID Vehiculo"};
            _txtDetaller = new Entry(){ PlaceholderText = "Detalles"};
            _txtCosto = new Entry(){ PlaceholderText = "Costo"};

            var btnIngresar = new Button("Ingresar");
            btnIngresar.Clicked += OnIngresarClicked;

            vbox.PackStart(_txtId, false, false, 5);
            vbox.PackStart(_txtIdRepuesto, false, false, 5);
            vbox.PackStart(_txtIdVehiculo, false, false, 5);
            vbox.PackStart(_txtDetaller, false, false, 5);
            vbox.PackStart(btnIngresar, false, false, 20);

            Add(vbox);
            ShowAll();
        }
        private void OnIngresarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtId.Text;
            var idRepuesto = _txtIdRepuesto.Text;
            var idVehiculo = _txtIdVehiculo.Text;
            var detalles = _txtDetaller.Text;
            var costo = _txtCosto.Text;

            return;
        }
    }
}