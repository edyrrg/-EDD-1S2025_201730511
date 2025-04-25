using Fase2.src.auth;
using Fase2.src.models;
using Fase2.src.services;
using Gtk;

namespace Fase2.src.views
{
    public class VisualizacionFacturas : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private ListStore? listStore;
        private TreeView treeView;
        private readonly UserSession _userSession;

        public VisualizacionFacturas(Window contextParent, DatasManager datasManager, UserSession userSession) : base("Visualización de Facturas | AutoGest Pro", contextParent)
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
            treeView.AppendColumn("Orden", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Total", new CellRendererText(), "text", 2);
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
                var servicios = _datasManager._servicioService.PreOrderFilterByUserVehicles(vehiculos);
                var facturas = _datasManager._facturaService.ObtenerFacturasUsuario(servicios);
                listStore = GetListStore(facturas);
                treeView.Model = listStore;
                treeView.ShowAll();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        private ListStore GetListStore(List<Factura> facturas)
        {
            var listStore = new ListStore(typeof(int), typeof(int), typeof(float));
            foreach (var factura in facturas)
            {
                listStore.AppendValues(factura.Id, factura.IdServicio, factura.Total);
            }
            return listStore;
        }
    }
}