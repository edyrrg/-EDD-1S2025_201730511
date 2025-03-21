namespace Fase2.src.services {
    public class DatasManager {
        private static DatasManager? _instance;
        public UserService _userService { get; } = UserService.Instance;

        private DatasManager() { }
        public static DatasManager Instance {
            get {
                _instance ??= new DatasManager();
                return _instance;
            }
        }

    }
}