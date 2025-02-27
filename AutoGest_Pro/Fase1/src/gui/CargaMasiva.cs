using Gtk;
using Fase1.src.models;
using System.Text.Json;
using Fase1.src.services;

namespace Fase1.src.gui
{
    public class CargaMasiva : MyWindow
    {
        private DataService _DataService;
        private ComboBoxText _comboBox;

        public CargaMasiva(Window contextParent, DataService dataService) : base("Carga Masiva | AutoGest Pro", contextParent)
        {
            // Inyección de dependencias
            _DataService = dataService;

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
                        PopError(ex.Message);
                    }
                }
                else
                {
                    PopError("No se seleccionó ningún archivo");
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
                    DeserializarVehiculos(jsonText);
                    break;
                case "Carga de Repuestos":
                    DeserializarRepuestos(jsonText);
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
                PopError("No se pudo deserializar el archivo JSON");
                return;
            }

            foreach (var usuario in usuarios)
            {
                try
                {
                    _DataService.IngresarUsuario(usuario);
                }
                catch (Exception ex)
                {
                    PopError(ex.Message);
                }
            }
            PopSucess("Usuarios cargados exitosamente");
            _DataService.ListadoUsuarios.Print();
        }

        private void DeserializarVehiculos(string jsonText)
        {
            var vehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(jsonText);
            if (vehiculos is null)
            {
                PopError("No se pudo deserializar el archivo JSON");
                return;
            }

            foreach (var vehiculo in vehiculos)
            {
                try
                {
                    _DataService.IngresarVehiculo(vehiculo);
                }
                catch (Exception ex)
                {
                    PopError(ex.Message);
                }
            }
            PopSucess("Vehiculos cargados exitosamente");
            _DataService.ListadoVehiculos.Print();
        }

        private void DeserializarRepuestos(string jsonText)
        {
            var repuestos = JsonSerializer.Deserialize<List<RepuestoModel>>(jsonText);
            if (repuestos is null)
            {
                PopError("No se pudo deserializar el archivo JSON");
                return;
            }

            foreach (var respuesto in repuestos)
            {
                try
                {
                    _DataService.IngresarRepuesto(respuesto);
                }
                catch (Exception ex)
                {
                    PopError(ex.Message);
                }
            }
            PopSucess("Repuestos cargados exitosamente");
            _DataService.ListadoRepuestos.Print();
        }

        private void PopError(string txtError)
        {
            var dialogError = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, $"{txtError}");
            dialogError.Run();
            dialogError.Destroy();
        }

        private void PopSucess(string txtSuccess)
        {
            var dialogSuccess = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, $"{txtSuccess}");
            dialogSuccess.Run();
            dialogSuccess.Destroy();
        }
    }

}