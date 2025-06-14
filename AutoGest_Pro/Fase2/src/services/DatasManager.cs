using Fase2.src.models;
using Gtk;

namespace Fase2.src.services
{
    public class DatasManager
    {
        private static DatasManager? _instance;
        public UserService _userService { get; } = UserService.Instance;
        public VehiculoService _vehiculoService { get; } = VehiculoService.Instance;
        public RepuestoService _repuestoService { get; } = RepuestoService.Instance;
        public ServicioService _servicioService { get; } = ServicioService.Instance;
        public FacturaService _facturaService { get; } = FacturaService.Instance;
        private DatasManager()
        {
            LoadData();
        }
        public static DatasManager Instance
        {
            get
            {
                _instance ??= new DatasManager();
                return _instance;
            }
        }

        public void LoadData()
        {
            _userService.InsertUser(new Usuario(101, "Edy", "Rojas", "ryuk@mail.com", 26, "123"));
        }
    }
}