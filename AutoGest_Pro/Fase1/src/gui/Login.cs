using Gtk;
using Fase1.src.auth;

namespace Fase1.src.gui
{
    public class Login : Window
    {
        private Entry entryUserName;
        private Entry entryPassword;
        public Login() : base("Login | AutoGest Pro")
        {
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => Application.Quit();

            var grid = new Grid
            {
                ColumnSpacing = 5,
                RowSpacing = 5
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

                var dialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Info,
                    ButtonsType.Ok,
                    "Bienvenido");
                dialog.Run();
                dialog.Destroy();
                Hide();
                var mainWindow = new MainWindow();
                mainWindow.ShowAll();
            }
            else
            {
                var dialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Warning,
                    ButtonsType.Close,
                    "Usuario o contraseña incorrectos");
                dialog.Run();
                dialog.Destroy();
            }
        }
    }
}