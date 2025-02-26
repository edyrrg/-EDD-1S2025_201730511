using Gtk;

namespace Fase1.src.gui
{
    public class IngresoRepuesto : MyWindow
    {
        private Entry _txtId;
        private Entry _txtRepuesto;
        private Entry _txtDetalles;
        private Entry _txtCosto;

        public IngresoRepuesto(Window contextParent) : base("Ingreso Individual | AutoGest Pro", contextParent)
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
            _txtRepuesto = new Entry(){ PlaceholderText = "Repuesto"};
            _txtDetalles = new Entry(){ PlaceholderText = "Detalles"};
            _txtCosto = new Entry(){ PlaceholderText = "Costo"};

            var btnIngresar = new Button("Ingresar");
            btnIngresar.Clicked += OnIngresarClicked;

            vbox.PackStart(_txtId, false, false, 5);
            vbox.PackStart(_txtRepuesto, false, false, 5);
            vbox.PackStart(_txtDetalles, false, false, 5);
            vbox.PackStart(_txtCosto, false, false, 5);
            vbox.PackStart(btnIngresar, false, false, 20);

            Add(vbox);
            ShowAll();
        }
        private void OnIngresarClicked(object? sender, System.EventArgs e)
        {
            var id = _txtId.Text;
            var repuesto = _txtRepuesto.Text;
            var detalles = _txtDetalles.Text;
            var costo = _txtCosto.Text;

            return;
        }
    }
}