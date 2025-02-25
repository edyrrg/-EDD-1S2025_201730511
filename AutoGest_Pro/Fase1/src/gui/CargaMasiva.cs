using GLib;
using Gtk;
using Fase1.src.models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Fase1.src.gui
{
    public class CargaMasiva : MyWindow
    {
        private readonly Window _contextMain;
        private ComboBoxText _comboBox;

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

            _comboBox = new ComboBoxText();

            _comboBox.AppendText("Carga de Usuarios");
            _comboBox.AppendText("Carga de Vehiculos");
            _comboBox.AppendText("Carga de Repuestos");
            _comboBox.Active = 0;

            var btnCargar = new Button("Cargar");
            btnCargar.Clicked += OnCargarClicked;

            vbox.PackStart(_comboBox, false, false, 30);
            vbox.PackStart(btnCargar, false, false, 30);

            Add(vbox);
            ShowAll();
        }

        private void OnCargarClicked(object? sender, System.EventArgs e)
        {
            // Obteniendo la opción seleccionada en el ComboBox Text
            var selected = _comboBox.ActiveText;
            Console.WriteLine($"Opción seleccionada: {selected}");

            using var dialog = new FileChooserDialog("Seleccione el archivo JSON a cargar",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept);
            dialog.SetSizeRequest(500, 400);
            dialog.SelectMultiple = false;
            dialog.SetCurrentFolder("/mnt/c/Users/edyrr/Downloads");

            var filter = new FileFilter { Name = "Archivos JSON" };
            filter.AddPattern("*.json");
            dialog.AddFilter(filter);

            try
            {
                if (dialog.Run() == (int)ResponseType.Accept)
                {
                    var filename = dialog.Filename;
                    try
                    {
                        string jsonText = File.ReadAllText(filename);
                        Console.WriteLine(jsonText);
                        ProcessFileByType(jsonText);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                    }
                }
            }
            finally
            {
                dialog.Destroy();
            }
        }

        public void OnDeleteEvent()
        {
            _contextMain.ShowAll();
            Destroy();
        }

        private void ProcessFileByType(string jsonText)
        {
            switch (_comboBox.ActiveText)
            {
                case "Carga de Usuarios":
                    DeserializarUsuarios(jsonText);
                    break;
                case "Carga de Vehiculos":
                    DeserializarVehiculos();
                    break;
                case "Carga de Repuestos":
                    DeserializarRepuestos();
                    break;
                default:
                    break;
            }
            return;
        }

        private void DeserializarUsuarios(string jsonText)
        {
            var usuarios = JsonSerializer.Deserialize<List<Usuario>>(jsonText);
            if (usuarios is null)
            {
                Console.WriteLine("Error al deserializar el archivo JSON");
                return;
            }
            foreach (var usuario in usuarios)
            {
                Console.WriteLine(usuario);
            }
        }

        private void DeserializarVehiculos()
        {
            return;
        }

        private void DeserializarRepuestos()
        {
            return;
        }
    }
}