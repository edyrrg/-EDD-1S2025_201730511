using System.Text.Json;
using Fase3.src.builders;
using Fase3.src.models;
using Fase3.src.services;
using Gtk;

namespace Fase3.src.views
{
    public class CargaMasiva : CustomWindow
    {
        private readonly DatasManager _datasManager;
        private ComboBoxText _comboBox;
        private ErrorReportBuilder _errorReportBuilder;
        public CargaMasiva(
                            Window contextParent,
                            DatasManager datasManager
                        ) : base("Carga Masiva | AutoGest Pro", contextParent)
        {
            // Inyección de dependencias
            _datasManager = datasManager;

            _errorReportBuilder = new ErrorReportBuilder();

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

            var btnCargarBackup = new Button("Cargar Backup");
            btnCargarBackup.Clicked += OnCargarBackupClicked;

            vbox.PackStart(_comboBox, false, false, 30);
            vbox.PackStart(btnCargar, false, false, 5);
            vbox.PackStart(btnCargarBackup, false, false, 5);

            Add(vbox);
            ShowAll();
        }

        private void OnCargarBackupClicked(object? sender, System.EventArgs e)
        {
            try
            {
                _datasManager._userService.RestoreBackup();
                PopSucess("Backup de usuarios restaurado exitosamente");
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
        }
        private void OnCargarClicked(object? sender, System.EventArgs e)
        {
            using var dialog = new FileChooserDialog("Seleccione el archivo JSON a cargar",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept);
            dialog.SetSizeRequest(600, 450);
            dialog.SelectMultiple = false;
            var filter = new FileFilter { Name = "Archivos JSON" };
            filter.AddPattern("*.json");
            dialog.Filter = filter;
            // dialog.SetCurrentFolder("/mnt/c/Users/edyrr/Downloads");

            try
            {
                if (dialog.Run() == (int)ResponseType.Accept)
                {
                    var filename = dialog.Filename;

                    string jsonText = File.ReadAllText(filename);
                    //Console.WriteLine(jsonText);
                    ProcessFileByType(jsonText);
                }
                else
                {
                    PopError("No se seleccionó ningún archivo");
                }
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
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
            List<Usuario>? usuarios = null;
            try
            {
                usuarios = JsonSerializer.Deserialize<List<Usuario>>(jsonText);
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            if (usuarios is null)
            {
                PopError("No se pudo deserializar el archivo JSON");
                return;
            }
            // Reset builder
            _errorReportBuilder.Reset();

            var successCount = 0;
            foreach (var usuario in usuarios)
            {
                try
                {
                    var userService = _datasManager._userService;
                    userService.InsertUser(usuario);
                    successCount++;
                }
                catch (Exception ex)
                {
                    _errorReportBuilder.AddDuplicateEntity(EntityType.Usuario, ex.Message);
                }
            }
            var errorReport = _errorReportBuilder.Build();
            if (errorReport.DuplicateEntities.Count > 0)
            {
                PopError(errorReport.GenerateReport());
            }
            else if (successCount > 0)
            {
                PopSucess("Usuarios cargados exitosamente");
            }
            //_datasManager._userService.Print();
        }
        private void DeserializarVehiculos(string jsonText)
        {
            List<Vehiculo>? vehiculos = null;
            try
            {
                vehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(jsonText);
                // Console.WriteLine(vehiculos?.ToString());
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }

            if (vehiculos is null)
            {
                PopError("No se pudo deserializar el archivo JSON");
                return;
            }
            // Reset builder
            _errorReportBuilder.Reset();
            var successCount = 0;
            foreach (var vehiculo in vehiculos)
            {
                try
                {
                    var vehiculoService = _datasManager._vehiculoService;
                    vehiculoService.InsertVehiculo(vehiculo);
                    successCount++;
                }
                catch (Exception ex)
                {
                    _errorReportBuilder.AddDuplicateEntity(EntityType.Vehiculo, ex.Message);
                }
            }

            var errorReport = _errorReportBuilder.Build();
            if (errorReport.DuplicateEntities.Count > 0)
            {
                PopError(errorReport.GenerateReport());
            }
            else if (successCount > 0)
            {
                PopSucess("Vehiculos cargados exitosamente");
            }
        }

        private void DeserializarRepuestos(string jsonText)
        {
            List<Repuestos>? repuestos = null;
            try
            {
                // Console.WriteLine(jsonText);
                repuestos = JsonSerializer.Deserialize<List<Repuestos>>(jsonText);
            }
            catch (Exception ex)
            {
                PopError(ex.Message);
            }
            if (repuestos is null)
            {
                PopError("No se pudo deserializar el archivo JSON");
                return;
            }
            // Reset builder
            _errorReportBuilder.Reset();
            var successCount = 0;
            foreach (var respuesto in repuestos)
            {
                try
                {
                    var repuestoService = _datasManager._repuestoService;
                    repuestoService.InsertRepuesto(respuesto);
                    successCount++;
                }
                catch (Exception ex)
                {
                    _errorReportBuilder.AddDuplicateEntity(EntityType.Repuesto, ex.Message);
                }
            }
            var errorReport = _errorReportBuilder.Build();
            if (errorReport.DuplicateEntities.Count > 0)
            {
                PopError(errorReport.GenerateReport());
            }
            else if (successCount > 0)
            {
                PopSucess("Repuestos cargados exitosamente");
            }
        }
    }
}