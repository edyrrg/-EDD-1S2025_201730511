using Gtk;
using Fase2.src.services;
using Fase2.src.auth;
using Fase2.src.models;

namespace Fase2.src.views
{
    public class VisualizacionServicios : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private ComboBoxText _comboBox;
        private TreeView treeView;
        private ListStore? listStore;
        private readonly UserSession _userSession;
        public VisualizacionServicios(Window contextParent, DatasManager datasManager, UserSession userSession) : base("Visualización de Servicios | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            _userSession = userSession;
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
            treeView.AppendColumn("ID Repuesto", new CellRendererText(), "text", 1);
            treeView.AppendColumn("ID Vehiculo", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Descripción", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Precio", new CellRendererText(), "text", 4);
            treeView.Margin = 5;

            DefaultView();

            vbox.PackStart(_comboBox, false, false, 15);
            vbox.PackStart(treeView, true, false, 0);
            Add(vbox);
            ShowAll();
        }

        public void OnComboBoxChanged(object? sender, System.EventArgs e)
        {
            var user = _userSession.GetUser();
            if (user == null)
            {
                PopError("Usuario no encontrado.");
                return;
            }
            try
            {
                var vehiculos = _datasManager._vehiculoService.GetVehiculoByUserId(user.ID);
                var active = _comboBox.ActiveText;
                if (active == "PRE-ORDERN")
                {
                    var servicios = _datasManager._servicioService.PreOrderFilterByUserVehicles(vehiculos);
                    listStore = GetListStore(servicios);
                    treeView.Model = listStore;
                    treeView.ShowAll();
                }
                else if (active == "POST-ORDERN")
                {
                    var servicios = _datasManager._servicioService.PostOrderFilterByUserVehicles(vehiculos);
                    listStore = GetListStore(servicios);
                    treeView.Model = listStore;
                    treeView.ShowAll();
                }
                else if (active == "IN-ORDERN")
                {
                    var servicios = _datasManager._servicioService.InOrderFilterByUserVehicles(vehiculos);
                    listStore = GetListStore(servicios);
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
            var user = _userSession.GetUser();
            if (user == null)
            {
                PopError("Usuario no encontrado.");
                return;
            }
            try
            {
                var vehiculos = _datasManager._vehiculoService.GetVehiculoByUserId(user.ID);
                var servicios = _datasManager._servicioService.PreOrderFilterByUserVehicles(vehiculos);
                listStore = GetListStore(servicios);
                treeView.Model = listStore;
                treeView.ShowAll();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }

        private ListStore GetListStore(List<Servicio> servicios)
        {
            var listStore = new ListStore(typeof(int), typeof(int), typeof(int), typeof(string), typeof(float));
            foreach (var servicio in servicios)
            {
                listStore.AppendValues(servicio.ID, servicio.IdRespuesto, servicio.IdVehicle, servicio.Detalles, servicio.Costo);
            }
            return listStore;
        }
    }
}