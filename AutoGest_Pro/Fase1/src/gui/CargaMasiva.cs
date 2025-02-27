using Gtk;
using Fase1.src.models;
using System.Text.Json;

namespace Fase1.src.gui
{
    public class CargaMasiva : MyWindow
    {
        private ComboBoxText _comboBox;

        public CargaMasiva(Window contextParent) : base("Carga Masiva | AutoGest Pro", contextParent)
        {
            SetDefaultSize(450, 350);
            SetPosition(WindowPosition.Center);
            DeleteEvent += (_, _) => OnDeleteEvent();

            // AplicarEstilos();

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
            using var dialog = new FileChooserDialog("Seleccione el archivo JSON a cargar",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept);
            dialog.SetSizeRequest(600, 400);
            dialog.SelectMultiple = false;
            var filter = new FileFilter { Name = "Archivos JSON" };
            filter.AddPattern("*.json");
            dialog.Filter = filter;
            dialog.SetCurrentFolder("/mnt/c/Users/edyrr/Downloads");

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
                else
                {
                    var dialogError = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se seleccionó ningún archivo");
                    dialogError.Run();
                    dialogError.Destroy();
                }
            }
            finally
            {
                dialog.Destroy();
            }
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