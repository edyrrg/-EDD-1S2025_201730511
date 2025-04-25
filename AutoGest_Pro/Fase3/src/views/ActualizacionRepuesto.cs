using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class ActualizacionRepuesto : CustomWindow
    {
        private readonly DatasManager _datasManager;

        private Entry _txtID;
        private Entry _txtRepuesto;
        private Entry _txtDetalles;
        private Entry _txtCosto;
        private Entry txtRepuesto;
        private Entry txtDetalles;
        private Entry txtCosto;

        public ActualizacionRepuesto(Window contextParent, DatasManager datasManager) : base("Actualización de Repuestos | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            SetDefaultSize(550, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 20)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var grid = new Grid() { ColumnSpacing = 10, RowSpacing = 10 };

            // Labels
            var lblID = new Label("ID:") { Halign = Align.Start };
            var lblRepuesto = new Label("Repuesto:") { Halign = Align.Start };
            var lblDetalles = new Label("Detalles:") { Halign = Align.Start };
            var lblCosto = new Label("Costo:") { Halign = Align.Start };

            txtRepuesto = new Entry() { Sensitive = false };
            txtRepuesto.StyleContext.AddClass("txt-lbl");
            txtDetalles = new Entry() { Sensitive = false };
            txtDetalles.StyleContext.AddClass("txt-lbl");
            txtCosto = new Entry() { Sensitive = false };
            txtCosto.StyleContext.AddClass("txt-lbl");

            _txtID = new Entry();
            _txtRepuesto = new Entry();
            _txtDetalles = new Entry();
            _txtCosto = new Entry();

            var btnBuscar = new Button("Buscar");
            btnBuscar.Clicked += OnBuscarClicked;
            var btnActualizar = new Button("Actualizar");
            btnActualizar.Clicked += OnActualizarClicked;

            grid.Attach(lblID, 0, 0, 1, 1);
            grid.Attach(_txtID, 1, 0, 1, 1);
            grid.Attach(btnBuscar, 2, 0, 1, 1);
            grid.Attach(lblRepuesto, 0, 1, 1, 1);
            grid.Attach(txtRepuesto, 1, 1, 1, 1);
            grid.Attach(_txtRepuesto, 2, 1, 1, 1);
            grid.Attach(lblDetalles, 0, 2, 1, 1);
            grid.Attach(txtDetalles, 1, 2, 1, 1);
            grid.Attach(_txtDetalles, 2, 2, 1, 1);
            grid.Attach(lblCosto, 0, 3, 1, 1);
            grid.Attach(txtCosto, 1, 3, 1, 1);
            grid.Attach(_txtCosto, 2, 3, 1, 1);
            grid.Attach(btnActualizar, 1, 4, 2, 1);

            vbox.Add(grid);

            Add(vbox);

            ShowAll();
        }

        private void OnBuscarClicked(object? sender, EventArgs e)
        {
            var id = _txtID.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("No es posible hacer la busqueda de Repuesto sin un ID.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }

            try
            {
                var repuesto = _datasManager._repuestoService.FindRepuestoByID(idInt);
                txtRepuesto.Text = repuesto.Repuesto;
                txtDetalles.Text = repuesto.Detalles;
                txtCosto.Text = repuesto.Costo.ToString();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
                ClearEntries();
            }
        }

        private void OnActualizarClicked(object? sender, EventArgs e)
        {
            var id = _txtID.Text;
            if (string.IsNullOrEmpty(id))
            {
                PopError("No es posible hacer la actualizacion de Repuesto sin un ID.");
                return;
            }
            if (!int.TryParse(id, out var idInt))
            {
                PopError("El campo ID debe ser un número entero.");
                return;
            }
            if (string.IsNullOrEmpty(_txtRepuesto.Text)
                && string.IsNullOrEmpty(_txtDetalles.Text)
                && string.IsNullOrEmpty(_txtCosto.Text))
            {
                PopError("No es posible hacer la actualizacion de Repuesto sin datos.");
                return;
            }

            if (string.IsNullOrEmpty(txtRepuesto.Text)
                && string.IsNullOrEmpty(txtDetalles.Text)
                && string.IsNullOrEmpty(txtCosto.Text))
            {
                PopError("Por favor, primero busque el repuesto para validar su existencia.");
                return;
            }

            try
            {
                var repuesto = new Repuestos(
                    idInt,
                    string.IsNullOrEmpty(_txtRepuesto.Text)?txtRepuesto.Text:_txtRepuesto.Text,
                    string.IsNullOrEmpty(_txtDetalles.Text)?txtDetalles.Text:_txtDetalles.Text,
                    string.IsNullOrEmpty(_txtCosto.Text)?float.Parse(txtCosto.Text):float.Parse(_txtCosto.Text)
                );
                _datasManager._repuestoService.UpdateRepuesto(repuesto);
                PopSucess("Repuesto actualizado correctamente.");
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
            _txtID.Text = string.Empty;
            _txtRepuesto.Text = string.Empty;
            _txtDetalles.Text = string.Empty;
            _txtCosto.Text = string.Empty;
            txtRepuesto.Text = string.Empty;
            txtDetalles.Text = string.Empty;
            txtCosto.Text = string.Empty;
        }
    }
}