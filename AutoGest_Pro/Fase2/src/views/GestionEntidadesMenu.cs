using Fase2.src.services;
using Gtk;

namespace Fase2.src.views
{
    public class GestionEntidadesMenu : CustomWindow
    {
        private readonly DatasManager _datasManager;
        public GestionEntidadesMenu(
                                    Window contextParent,
                                    DatasManager datasManager
                                    ) : base("Menu de Gestion de Entidades | AutoGest Pro", contextParent)
        {
            // Inyectamos dependencias
            _datasManager = datasManager;

            SetDefaultSize(400, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();


            var vbox = new Box(Orientation.Vertical, 0)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            // var lblTitulo = new Label("Gestión de Entidades");
            // lblTitulo.StyleContext.AddClass("title");
            

            var btnIngresoUsuario = new Button("Gestionar Usuarios");
            btnIngresoUsuario.Clicked += OnGestionUsuariosClicked;

            var btnIngresoVehiculo = new Button("Gestionar Vehiculos");
            btnIngresoVehiculo.Clicked += OnGestionVehiculosClicked;

            // vbox.PackStart(lblTitulo, false, false, 10);
            vbox.PackStart(btnIngresoUsuario, false, false, 10);
            vbox.PackStart(btnIngresoVehiculo, false, false, 10);

            Add(vbox);
            ShowAll();
        }
        private void OnGestionUsuariosClicked(object? sender, EventArgs e)
        {
            var gestionUsuarios = new GestionUsuarios(this, _datasManager);
            gestionUsuarios.Show();
            Hide();
        }
        private void OnGestionVehiculosClicked(object? sender, EventArgs e)
        {
            var gestionVehiculos = new GestionVehiculos(this, _datasManager);
            gestionVehiculos.Show();
            Hide();
        }
    }
}