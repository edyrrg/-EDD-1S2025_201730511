using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class VisualizacionRepuestos : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private ComboBoxText _comboBox;
        private TreeView treeView;
        private ListStore? listStore;
        public VisualizacionRepuestos(Window contextParent, DatasManager datasManager) : base("Visualización de Repuestos | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            SetDefaultSize(550, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 5)
            {
                Halign = Align.Center,
                Valign = Align.Start
            };

            _comboBox = new ComboBoxText();
            _comboBox.AppendText("PRE-ORDERN");
            _comboBox.AppendText("POST-ORDERN");
            _comboBox.AppendText("IN-ORDERN");
            _comboBox.Active = 0;

            _comboBox.Changed += OnComboBoxChanged;

            treeView = new TreeView();
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Repuesto", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Descripción", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Precio", new CellRendererText(), "text", 3);
            treeView.Margin = 5;

            DefaultView();

            vbox.PackStart(_comboBox, false, false, 15);
            vbox.PackStart(treeView, true, false, 0);
            Add(vbox);
            ShowAll();
        }
        public void OnComboBoxChanged(object? sender, System.EventArgs e)
        {
            try
            {
                var active = _comboBox.ActiveText;
                if (active == "PRE-ORDERN")
                {
                    var repuestos = _datasManager._repuestoService.GetRepuestosPreOrder();
                    listStore = GetListStore(repuestos);
                    treeView.Model = listStore;
                    treeView.ShowAll();
                }
                else if (active == "POST-ORDERN")
                {
                    var repuestos = _datasManager._repuestoService.GetRepuestosPostOrder();
                    listStore = GetListStore(repuestos);
                    treeView.Model = listStore;
                    treeView.ShowAll();
                }
                else if (active == "IN-ORDERN")
                {
                    var repuestos = _datasManager._repuestoService.GetRepuestosInOrder();
                    listStore = GetListStore(repuestos);
                    treeView.Model = listStore;
                    treeView.ShowAll();
                }
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }

        }

        private void DefaultView()
        {
            try
            {
                var repuestos = _datasManager._repuestoService.GetRepuestosPreOrder();
                listStore = GetListStore(repuestos);
                treeView.Model = listStore;
                treeView.ShowAll();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }

        private ListStore GetListStore(List<Repuestos> repuestos)
        {
            // ID, Repuesto, Descripción, Precio
            var listStore = new ListStore(typeof(int), typeof(string), typeof(string), typeof(double));
            foreach (var repuesto in repuestos)
            {
                listStore.AppendValues(repuesto.ID, repuesto.Repuesto, repuesto.Detalles, repuesto.Costo);
            }
            return listStore;
        }
    }
}