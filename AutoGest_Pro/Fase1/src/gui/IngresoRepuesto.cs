using Fase1.src.models;
using Fase1.src.services;
using Gtk;

namespace Fase1.src.gui
{
    public class IngresoRepuesto : MyWindow
    {
        private readonly DataService _DataService;
        private Entry _txtId;
        private Entry _txtRepuesto;
        private Entry _txtDetalles;
        private Entry _txtCosto;

        public IngresoRepuesto(Window contextParent, DataService dataService) : base("Ingreso Individual | AutoGest Pro", contextParent)
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

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(repuesto) || string.IsNullOrWhiteSpace(detalles) || string.IsNullOrWhiteSpace(costo))
            {
                PopError("Por favor, llene todos los campos.");
                return;
            }

            try
            {
                _DataService.IngresarRepuesto(new RepuestoModel
                {
                    ID = int.Parse(id),
                    Repuesto = repuesto,
                    Detalles = detalles,
                    Costo = float.Parse(costo)
                });
                PopSucess("Repuesto ingresado exitosamente.");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            } finally {
                _txtId.Text = "";
                _txtRepuesto.Text = "";
                _txtDetalles.Text = "";
                _txtCosto.Text = "";
            }
        }
    }
}