using System.Xml.Serialization;
using Fase1.src.adt;
using Fase1.src.models;

namespace Fase1.src.services
{
    public class DataService
    {
        // Instancia única de DataService (Singleton)
        private static DataService? _instance;

        // Inicialización de las estructuras de datos
        public SimpleList<int> ListadoUsuarios { get; } = new SimpleList<int>();
        public DoubleLinkedList<int> ListadoVehiculos { get; } = new DoubleLinkedList<int>();
        public CircularList<int> ListadoRepuestos { get; } = new CircularList<int>();
        public adt.Queue<int> ColaServicio { get; } = new adt.Queue<int>();
        public adt.Stack<int> PilaFactura { get; } = new adt.Stack<int>();

        // Constructor privado para evitar la creación de instancias fuera de la clase
        private DataService() { }

        // Propiedad para obtener la instancia única de DataService
        public static DataService Instance
        {
            get
            {
                _instance ??= new DataService();
                return _instance;
            }
        }

        // Métodos para ingresar datos en las estructuras de datos
        public void IngresarUsuario(Usuario usuario)
        {
            if (ListadoUsuarios.Search(usuario.ID) == 1)
            {
                var id = usuario.ID;
                throw new Exception($"El ID {id} ya existe en la lista de usuarios");
            }
            ListadoUsuarios.Insert(usuario.ID, usuario.Nombres, usuario.Apellidos, usuario.Correo, usuario.Contrasenia);
        }

        public Usuario BuscarUsuario(int id)
        {
            Usuario? result = ListadoUsuarios.Find(id);
            if (result == null)
            {
                throw new Exception($"El usuario con ID {id} no existe.");
            }
            return result;
        }

        public void IngresarVehiculo(Vehiculo vehiculo)
        {
            if (ListadoVehiculos.Search(vehiculo.ID) == 1)
            {
                var id = vehiculo.ID;
                throw new Exception($"El ID {id} ya existe en la lista de vehículos");
            }
            ListadoVehiculos.Insert(vehiculo.ID, vehiculo.ID_Usuario, vehiculo.Marca, vehiculo.Modelo, vehiculo.Placa);
        }

        public void IngresarRepuesto(RepuestoModel repuesto)
        {
            if (ListadoRepuestos.Search(repuesto.ID) == 1)
            {
                var id = repuesto.ID;
                throw new Exception($"El ID {id} ya existe en la lista de repuestos");
            }
            ListadoRepuestos.Insert(repuesto.ID, repuesto.Repuesto, repuesto.Detalles, repuesto.Costo);
        }

        public void IngresarServicio(Servicio servicio)
        {
            if (ColaServicio.Search(servicio.ID) == 1)
            {
                var id = servicio.ID;
                throw new Exception($"El ID {id} ya existe en la cola de servicios");
            }
            ColaServicio.Enqueue(servicio.ID, servicio.IdRepuesto, servicio.IdVehiculo, servicio
                                    .Detalles, servicio.Costo);
        }

        public void IngresarServicio(string id, string idRepuesto, string idVehiculo, string detalles, string costo)
        {
            // Lógica para ingresar un servicio en la estructura de datos correspondiente
            // Ejemplo: Queue.Enqueue(new Servicio(id, idRepuesto, idVehiculo, detalles, costo));
        }
    }
}