using Gtk;
using Fase1.src.auth;
using System.Runtime.CompilerServices;
using Fase1.src.services;

namespace Fase1.src.gui
{
    public class Login : MyWindow
    {
        private readonly DataService _DataService;
        private Entry entryUserName;
        private Entry entryPassword;
        public Login(DataService dataService) : base("Login | AutoGest Pro")
        {
            _DataService = dataService;
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => Application.Quit();

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

            if (AuthService.Login(username, password))
            {
                PopSucess($"Bienvenido {username}");
                Hide();
                var mainWindow = new MainWindow(_DataService);
                mainWindow.ShowAll();
            }
            else
            {
                PopError("Usuario o contraseña incorrectos");
            }
        }
    }
}