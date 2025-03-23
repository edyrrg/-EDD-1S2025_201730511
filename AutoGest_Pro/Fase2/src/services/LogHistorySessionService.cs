using Fase2.src.models;
using System.Text.Json;
using System.IO;

namespace Fase2.src.services
{
    public class LogHistorySessionService
    {
        private static LogHistorySessionService? _instance;
        private readonly List<LogHistorySession> _logHistorySessions;

        private LogHistorySessionService()
        {
            _logHistorySessions = new List<LogHistorySession>();
        }

        public static LogHistorySessionService Instance => _instance ??= new LogHistorySessionService();

        public void AddLogHistorySession(LogHistorySession logHistorySession)
        {
            _logHistorySessions.Add(logHistorySession);
        }

        public List<LogHistorySession> GetLogHistorySessions()
        {
            return _logHistorySessions;
        }

        // Método para exportar a JSON
        public void ExportToJsonAndSave(string fileName)
        {
            // Ruta de la carpeta Reportes
            string reportesPath = Path.Combine(".", "Reportes");

            // Crear la carpeta si no existe
            if (!Directory.Exists(reportesPath))
            {
                Directory.CreateDirectory(reportesPath);
            }

            // Ruta completa del archivo JSON
            string filePath = Path.Combine(reportesPath, fileName);

            // Convertir el listado a JSON
            string jsonContent = JsonSerializer.Serialize(_logHistorySessions, new JsonSerializerOptions
            {
                WriteIndented = true // Formato legible
            });

            // Guardar el JSON en el archivo
            File.WriteAllText(filePath, jsonContent);
        }
    }
}