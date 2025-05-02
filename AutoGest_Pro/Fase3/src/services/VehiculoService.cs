using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3.src.services
{
    public class VehiculoService
    {
        public static VehiculoService? _instance;
        private DoubleLinkedList<Vehiculo> _vehiculos { get; } = new DoubleLinkedList<Vehiculo>();

        private VehiculoService() { }

        public static VehiculoService Instance
        {
            get
            {
                _instance ??= new VehiculoService();
                return _instance;
            }
        }

        public void InsertVehiculo(Vehiculo vehiculo)
        {
            // Check if the vehiculo already exists
            // If it does, throw an exception
            // The check if the User is existent is do in the controller -> IMPORTANT!!!
            if (_vehiculos.SearchById(vehiculo.ID))
            {
                var id = vehiculo.ID;
                throw new Exception($"El vehiculo con id {id} ya existe.");
            }
            _vehiculos.Insert(vehiculo);
        }

        public void UpdateVehiculo(Vehiculo vehiculo)
        {
            if (!_vehiculos.Update(vehiculo))
            {
                var id = vehiculo.ID;
                throw new Exception($"El vehiculo con id {id} no existe.");
            }
        }

        public void DeleteVehiculo(int id)
        {
            if (!_vehiculos.Delete(id))
            {
                throw new Exception($"El vehiculo con id {id} no existe.");
            }
        }

        public Vehiculo FindVehiculoByID(int id)
        {
            var vehiculo = _vehiculos.FindById(id);
            if (vehiculo == null)
            {
                throw new Exception($"El vehiculo con id {id} no existe.");
            }
            return vehiculo;
        }

        public bool SearchVehiculoById(int id)
        {
            return _vehiculos.SearchById(id);
        }

        public List<Vehiculo> GetVehiculoByUserId(int id)
        {
            var vehiculosByUser = _vehiculos.GetVehiculosByUser(id);
            if (vehiculosByUser.Count == 0)
            {
                throw new Exception($"No hay vehiculos asociados al usuario con id {id}.");
            }
            return vehiculosByUser;
        }
        public void GenerateReport()
        {
            if (!_vehiculos.GenerateReport())
            {
                throw new Exception("No hay vehículos registrados para generar reporte.");
            }
        }

        public void SaveBackup()
        {
            if (!_vehiculos.SaveBackup())
            {
                throw new Exception("No se pudo guardar el backup porque no hay datos que guardar.");
            }
        }
    }
}