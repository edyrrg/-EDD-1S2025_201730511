using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3.src.services
{
    public class FacturaService
    {
        private static FacturaService? _instance;
        private MerkleTree _facturas = new MerkleTree();
        // private BTree _facturas = new BTree();

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
            var factura = new Factura(idServicio, idServicio, total);
            if (_facturas.Verify(factura))
            {
                var id = factura.Id;
                throw new Exception($"La factura con id {id} ya existe y no se pudo crear.");
            }
            _facturas.Add(factura);
        }

        public void CancelarFactura(int id)
        {
            var factura = _facturas.Find(id);
            if (factura == null)
            {
                throw new Exception($"La factura con id {id} no existe y no se puede cancelar.");
            }
            _facturas.Delete(factura);
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
            if (!_facturas.GenerateReport())
            {
                throw new Exception("No hay datos para generar el reporte.");
            }
        }
    }
}