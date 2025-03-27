using Fase2.src.adts;
using Fase2.src.models;

namespace Fase2.src.services
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

        public Vehiculo FindVehiculoById(int id)
        {
            var vehiculo = _vehiculos.FindById(id);
            if (vehiculo == null)
            {
                throw new Exception($"El vehiculo con id {id} no existe.");
            }
            return vehiculo;
        }
    }
}