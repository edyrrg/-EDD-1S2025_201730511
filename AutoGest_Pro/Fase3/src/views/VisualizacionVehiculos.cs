using Fase3.src.auth;
using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class VisualizacionVehiculos : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private ListStore? listStore;
        private TreeView treeView;
        private readonly UserSession _userSession;

        public VisualizacionVehiculos(Window contextParent, DatasManager datasManager, UserSession userSession) : base("Visualización de Vehículos | AutoGest Pro", contextParent)
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

            treeView = new TreeView();
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Propietario", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Marca", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Modelo", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Placa", new CellRendererText(), "text", 4);
            treeView.Margin = 5;

            DefaultView();

            vbox.PackStart(treeView, true, false, 0);
            Add(vbox);
            ShowAll();
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
                listStore = GetListStore(vehiculos);
                treeView.Model = listStore;
                treeView.ShowAll();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        private ListStore GetListStore(List<Vehiculo> vehiculos)
        {
            var listStore = new ListStore(typeof(int), typeof(int), typeof(string), typeof(int), typeof(string));
            foreach (var vehiculo in vehiculos)
            {
                listStore.AppendValues(vehiculo.ID, vehiculo.ID_Usuario, vehiculo.Marca, vehiculo.Modelo, vehiculo.Placa);
            }
            return listStore;
        }
    }
}