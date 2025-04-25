using Gtk;
using Fase2.src.services;
using Fase2.src.auth;

namespace Fase2.src.views
{
    public class CancelarFactura : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private readonly UserSession _userSession;

        private Entry _txtID;
        private Entry _txtIdServicio;
        private Entry _txtTotal;

        public CancelarFactura(Window contextParent, DatasManager datasManager, UserSession userSession) : base("Cancelar Factura | AutoGest Pro", contextParent)
        {
            _datasManager = datasManager;
            _userSession = userSession;

            SetDefaultSize(450, 400);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 20)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var grid = new Grid()
            {
                ColumnSpacing = 10,
                RowSpacing = 10
            };

            var lblID = new Label("ID:") { Halign = Align.Start };
            var lblIdServicio = new Label("Orden:") { Halign = Align.Start };
            var lblTotal = new Label("Total:") { Halign = Align.Start };

            _txtID = new Entry();
            _txtID.StyleContext.AddClass("txt-lbl");
            _txtIdServicio = new Entry()
            {
                Sensitive = false
            };
            _txtIdServicio.StyleContext.AddClass("txt-lbl");
            _txtTotal = new Entry()
            {
                Sensitive = false
            };
            _txtTotal.StyleContext.AddClass("txt-lbl");

            var btnBuscar = new Button("Buscar");
            btnBuscar.Clicked += OnBuscarClicked;
            var btnEliminar = new Button("Pagar Factura");
            btnEliminar.Clicked += OnEliminarClicked;

            grid.Attach(lblID, 0, 0, 1, 1);
            grid.Attach(_txtID, 1, 0, 1, 1);
            grid.Attach(btnBuscar, 2, 0, 1, 1);
            grid.Attach(lblIdServicio, 0, 1, 1, 1);
            grid.Attach(_txtIdServicio, 1, 1, 2, 1);
            grid.Attach(lblTotal, 0, 2, 1, 1);
            grid.Attach(_txtTotal, 1, 2, 2, 1);
            grid.Attach(btnEliminar, 1, 3, 2, 1);

            vbox.Add(grid);

            Add(vbox);

            ShowAll();
        }

        private void OnBuscarClicked(object? sender, EventArgs e)
        {
            var user = _userSession.GetUser();
            if (user == null)
            {
                PopError("Usuario no encontrado.");
                return;
            }

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
                var vehiculos = _datasManager._vehiculoService.GetVehiculoByUserId(user.ID);
                var servicios = _datasManager._servicioService.PreOrderFilterByUserVehicles(vehiculos);
                var facturas = _datasManager._facturaService.ObtenerFacturasUsuario(servicios);

                var factura = facturas.FirstOrDefault(f => f.Id == idInt);

                if (factura == null)
                {
                    PopError("Factura no encontrada.");
                    return;
                }
                _txtIdServicio.Text = factura.IdServicio.ToString();
                _txtTotal.Text = factura.Total.ToString();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }

        }
        private void OnEliminarClicked(Object? e, EventArgs args)
        {
            var user = _userSession.GetUser();
            if (user == null)
            {
                PopError("Usuario no encontrado.");
                return;
            }

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
                var vehiculos = _datasManager._vehiculoService.GetVehiculoByUserId(user.ID);
                var servicios = _datasManager._servicioService.PreOrderFilterByUserVehicles(vehiculos);
                var facturas = _datasManager._facturaService.ObtenerFacturasUsuario(servicios);

                if (facturas.Count == 0)
                {
                    PopError("No hay facturas para mostrar.");
                    return;
                }
                var factura = facturas.FirstOrDefault(f => f.Id == idInt);

                if (factura == null)
                {
                    PopError("Factura no encontrada.");
                    return;
                }

                _datasManager._facturaService.EliminarFactura(factura.Id);
                PopSucess("Factura pagada con éxito.");
                ClearEntries();
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }

        }
        private void ClearEntries()
        {
            _txtID.Text = string.Empty;
            _txtIdServicio.Text = string.Empty;
            _txtTotal.Text = string.Empty;
        }
    }
}