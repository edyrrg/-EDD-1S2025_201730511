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

        public void ActualizarUsuario(int id, string nombres, string apellidos, string correo)
        {
            if (ListadoUsuarios.Update(id, nombres, apellidos, correo) == 0)
            {
                throw new Exception($"Este ID '{id}', no existe en la lista de usuarios");
            }
        }

        public void EliminarUsuario(int id)
        {
            if (ListadoUsuarios.Delete(id) == 0)
            {
                throw new Exception($"El usuario con ID {id} no existe.");
            }
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

        public List<Vehiculo> BuscarVehiculoPorUsuario(int idUsuario)
        {
            var vehiculos = ListadoVehiculos.SearchVehiclesByUserId(idUsuario);
            if (vehiculos == null)
            {
                throw new Exception($"El usuario con ID {idUsuario} no tiene vehículos registrados.");
            }
            return vehiculos;
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

        public RepuestoModel BuscarRepuestoPorId(int id)
        {
            var repuesto = ListadoRepuestos.Find(id);
            if (repuesto == null)
            {
                throw new Exception($"El repuesto con ID {id} no existe.");
            }
            return repuesto;
        }

        public void CrearServicio(Servicio servicio)
        {
            if (ColaServicio.Search(servicio.ID) == 1)
            {
                var id = servicio.ID;
                throw new Exception($"El ID {id} ya existe en la cola de servicios\nNo se puede generar el servicio");
            }
            if (ListadoRepuestos.Search(servicio.IdRepuesto) == 0)
            {
                var id = servicio.IdRepuesto;
                throw new Exception($"El ID {id} no existe en la lista de repuestos\nNo se puede generar el servicio");
            }
            if (ListadoVehiculos.Search(servicio.IdVehiculo) == 0)
            {
                var id = servicio.IdVehiculo;
                throw new Exception($"El ID {id} no existe en la lista de vehículos\nNo se puede generar el servicio");
            }
            ColaServicio.Enqueue(servicio.ID, servicio.IdRepuesto, servicio.IdVehiculo, servicio
                                    .Detalles, servicio.Costo);
            Console.WriteLine("Servicio creado correctamente");
            ColaServicio.Print();
        }

        public void CrearFactura(Factura factura)
        {
            if (ColaServicio.IsEmpty())
            {
                throw new Exception("No hay servicios en la cola para generar la factura");
            }
            PilaFactura.Push(factura.IdOrden, factura.Total);
            Console.WriteLine("Factura creada correctamente");
            PilaFactura.Print();
        }
        public Factura? CancelarFactura()
        {
            if (PilaFactura.IsEmpty())
            {
                throw new Exception("No existen Facturas pendientes por cancelar");
            }
            return PilaFactura.Pop();
        }
    }
}