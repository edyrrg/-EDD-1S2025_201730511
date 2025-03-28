using Fase2.src.adts;
using Fase2.src.views;

namespace Fase2.src.services
{
    public class ServicioService
    {
        private static ServicioService? _instance;
        private BinaryTree _servicios = new BinaryTree();
        private ServicioService() { }
        public static ServicioService Instance
        {
            get
            {
                _instance ??= new ServicioService();
                return _instance;
            }
        }
        public void InsertServicio(Servicio servicio)
        {
            if (_servicios.Search(servicio.ID))
            {
                var id = servicio.ID;
                throw new Exception($"El servicio con id {id} ya existe y no se pudo crear.");
            }
            _servicios.Insert(servicio);
        }

        public List<Servicio> InOrder()
        {
            var servicios = _servicios.InOrder();
            if (servicios.Count == 0)
            {
                throw new Exception("No hay servicios para mostrar.");
            }
            return servicios;
        }
        public List<Servicio> PreOrder()
        {
            var servicios = _servicios.PreOrder();
            if (servicios.Count == 0)
            {
                throw new Exception("No hay servicios para mostrar.");
            }
            return servicios;
        }

        public List<Servicio> PostOrder()
        {
            var servicios = _servicios.PostOrder();
            if (servicios.Count == 0)
            {
                throw new Exception("No hay servicios para mostrar.");
            }
            return servicios;
        }

        public void GenerateReport(){
            if(!_servicios.GenerateReport()){
                throw new Exception("No hay servicios para generar reporte.");
            }
        }
    }
}


