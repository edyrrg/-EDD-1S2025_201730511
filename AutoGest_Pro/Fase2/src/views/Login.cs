using Fase2.src.auth;
using Fase2.src.models;
using Fase2.src.services;
using Gtk;


namespace Fase2.src.views
{
    public class Login : CustomWindow
    {
        private readonly DatasManager _dataManager;
        private Entry entryUserName;
        private Entry entryPassword;
        private readonly UserSession _userSession;
        private readonly LogHistorySessionService _logHistorySessionService;
        public Login(
                    DatasManager datasManager,
                    UserSession userSession,
                    LogHistorySessionService logHistoryService
                    ) : base("Login | AutoGest Pro")
        {
            // Inyectando las dependencias necesarias a Login
            _dataManager = datasManager;
            _userSession = userSession;
            _logHistorySessionService = logHistoryService;

            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => CloseApp();

            // Aplicando estilos
            AplicarEstilos();

            var grid = new Grid
            {
                ColumnSpacing = 8,
                RowSpacing = 8
            };

            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var lblUserName = new Label("Username");
            entryUserName = new Entry();

            var lblPassword = new Label("Password");
            entryPassword = new Entry { Visibility = false };
            entryPassword.KeyReleaseEvent += OnKeyRelease;

            var btnLogin = new Button("Login");
            btnLogin.Clicked += OnLoginClicked;

            btnLogin.StyleContext.AddClass("button_style");

            grid.Attach(lblUserName, 0, 0, 1, 1);
            grid.Attach(entryUserName, 1, 0, 1, 1);
            grid.Attach(lblPassword, 0, 1, 1, 1);
            grid.Attach(entryPassword, 1, 1, 1, 1);
            grid.Attach(btnLogin, 0, 2, 2, 1);
            vbox.Add(grid);
            Add(vbox);

            ShowAll();
        }

        private void OnKeyRelease(object sender, KeyReleaseEventArgs args)
        {
            if (args.Event.Key == Gdk.Key.Return || args.Event.Key == Gdk.Key.KP_Enter)
            {
                OnLoginClicked(sender, EventArgs.Empty);
            }
        }

        private void OnLoginClicked(object? sender, EventArgs e)
        {
            string username = entryUserName.Text;
            string password = entryPassword.Text;

            (var auth, var userRole) = AuthService.Login(username, password, _dataManager._userService);

            if (auth && userRole == UserRole.Admin)
            {
                PopSucess($"Bienvenido {username}");
                Hide();
                var dateNow = DateTime.UtcNow;
                string utcFormattedDate = dateNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); // Formato ISO 8601 en UTC
                var log = new LogHistorySession("Admin", utcFormattedDate); // Nuevo Registro de Log
                var adminMenu = new AdminMenu(
                                            this,
                                            _dataManager,
                                            log,
                                            _logHistorySessionService
                                            );
                adminMenu.ShowAll();
            }
            else if (auth && userRole == UserRole.User)
            {
                PopSucess($"Bienvenido {username}");
                var user = _dataManager._userService.FindUserByEmail(username);
                try
                {
                    _userSession.SetUser(user);
                }
                catch (Exception ex)
                {
                    PopError(ex.Message);
                }
                Hide();
                var dateNow = DateTime.UtcNow;
                string utcFormattedDate = dateNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                if (user == null)
                {
                    PopError("Usuario no encontrado");
                    return;
                }
                var log = new LogHistorySession(user.Nombres, utcFormattedDate);
                var userMenu = new UserMenu(
                                            this,
                                            _dataManager,
                                            _userSession,
                                            log,
                                            _logHistorySessionService
                                            );
                userMenu.ShowAll();
            }
            else
            {
                PopError("Usuario o contraseña incorrectos");
                entryUserName.GrabFocus();
                return;
            }

            ClearTxtFields();
        }

        private void ClearTxtFields()
        {
            entryUserName.Text = "";
            entryPassword.Text = "";
        }

        private void CloseApp()
        {
            try
            {
                var filename = "LogHistorySessions.json";
                _logHistorySessionService.ExportToJsonAndSave(filename);
                Console.WriteLine($"Archivo JSON exportado correctamente: ./Reportes/{filename}");
            }
            catch (Exception ex)
            {
                PopError($"Error al exportar el historial de sesiones: {ex.Message}");
            }
            Application.Quit();
        }
    }
}