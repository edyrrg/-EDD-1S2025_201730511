using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3.src.services
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

        public void GenerateReport()
        {
            if (!_servicios.GenerateReport())
            {
                throw new Exception("No hay servicios para generar reporte.");
            }
        }

        public List<Servicio> PostOrderFilterByUserVehicles(List<Vehiculo> vehiculos)
        {
            var serviciosPostOrder = PostOrder();
            var serviciosFiltrados = new List<Servicio>();
            foreach (var servicio in serviciosPostOrder)
            {
                if (vehiculos.Exists(v => v.ID == servicio.IdVehicle))
                {
                    serviciosFiltrados.Add(servicio);
                }
            }

            if (serviciosFiltrados.Count == 0)
            {
                throw new Exception("No hay servicios registrados para los vehiculos del usuarios");
            }
            return serviciosFiltrados;
        }

        public List<Servicio> PreOrderFilterByUserVehicles(List<Vehiculo> vehiculos)
        {
            var serviciosPreOrder = PreOrder();
            var serviciosFiltrados = new List<Servicio>();
            foreach (var servicio in serviciosPreOrder)
            {
                if (vehiculos.Exists(v => v.ID == servicio.IdVehicle))
                {
                    serviciosFiltrados.Add(servicio);
                }
            }
            if (serviciosFiltrados.Count == 0)
            {
                throw new Exception("No hay servicios registrados para los vehiculos del usuarios");
            }
            return serviciosFiltrados;
        }

        public List<Servicio> InOrderFilterByUserVehicles(List<Vehiculo> vehiculos)
        {
            var serviciosInOrder = InOrder();
            var serviciosFiltrados = new List<Servicio>();
            foreach (var servicio in serviciosInOrder)
            {
                if (vehiculos.Exists(v => v.ID == servicio.IdVehicle))
                {
                    serviciosFiltrados.Add(servicio);
                }
            }
            if (serviciosFiltrados.Count == 0)
            {
                throw new Exception("No hay servicios registrados para los vehiculos del usuarios");
            }
            return serviciosFiltrados;
        }
    }
}


