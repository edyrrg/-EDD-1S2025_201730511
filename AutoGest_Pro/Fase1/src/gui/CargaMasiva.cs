using Gtk;

namespace Fase1.src.gui
{
    public class CargaMasiva : MyWindow
    {
        private readonly Window _contextMain;

        public CargaMasiva(Window contextMain) : base("Carga Masiva | AutoGest Pro")
        {
            // Pasando referencia de la ventana principal
            _contextMain = contextMain;

            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            var vbox = new Box(Orientation.Vertical, 15)
            {
                Halign = Align.Center,
                Valign = Align.Center
            };

            var comboBox = new ComboBoxText();

            comboBox.AppendText("Carga de Usuarios");
            comboBox.AppendText("Carga de Vehiculos");
            comboBox.AppendText("Carga de Repuestos");
            comboBox.Active = 0;

            var btnCargar = new Button("Cargar");
            btnCargar.Clicked += OnCargarClicked;

            vbox.PackStart(comboBox, false, false, 30);
            vbox.PackStart(btnCargar, false, false, 30);

            Add(vbox);
            ShowAll();
        }

        private void OnCargarClicked(object? sender, System.EventArgs e)
        {
            return;
        }

        public void OnDeleteEvent()
        {
            _contextMain.ShowAll();
            Destroy();
        }
    }
}