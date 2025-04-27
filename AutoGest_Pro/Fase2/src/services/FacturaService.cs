using Fase2.src.adts;
using Fase2.src.models;

namespace Fase2.src.services
{
    public class FacturaService
    {
        private static int controlId = 0;
        private static FacturaService? _instance;

        private BTree _facturas = new BTree();

        private FacturaService() { }
        public static FacturaService Instance
        {
            get
            {
                _instance ??= new FacturaService();
                return _instance;
            }
        }

        public void InsertFactura(int idServicio, float total)
        {
            controlId++;
            var factura = new Factura(controlId, idServicio, total);
            if (_facturas.Search(factura.Id))
            {
                var id = factura.Id;
                throw new Exception($"La factura con id {id} ya existe y no se pudo crear.");
            }
            _facturas.Insertar(factura);
        }

        public void CancelarFactura(int id)
        {
            if (!_facturas.Search(id))
            {
                throw new Exception($"La factura con id {id} no existe y no se puede cancelar.");
            }
            _facturas.Eliminar(id);
        }

        public List<Factura> ObtenerFacturasUsuario(List<Servicio> servicios)
        {
            var tmpListFacturas = new List<Factura>();

            foreach (var servicio in servicios)
            {
                var currentFactura = _facturas.BuscarPorServicio(servicio.ID);
                if (currentFactura != null)
                {
                    tmpListFacturas.Add(currentFactura);
                }
            }

            if (tmpListFacturas.Count == 0)
            {
                throw new Exception("No hay facturas para mostrar.");
            }
            return tmpListFacturas;
        }

        public void GenerateReport()
        {
            _facturas.GenerateReport();
        }

        public void EliminarFactura(int id)
        {
            if (!_facturas.Search(id))
            {
                throw new Exception($"La factura con id {id} no existe y no se puede eliminar.");
            }
            _facturas.Eliminar(id);
        }
    }
}