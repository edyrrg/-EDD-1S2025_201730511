using Fase3.src.models;
using System.Text.Json;
using System.IO;

namespace Fase3.src.services
{   /**
     * Clase para el servicio de historial de sesiones
     */
    public class LogHistorySessionService
    {
        private static LogHistorySessionService? _instance;
        private readonly List<LogHistorySession> _logHistorySessions;

        private LogHistorySessionService()
        {
            _logHistorySessions = new List<LogHistorySession>();
        }

        public static LogHistorySessionService Instance => _instance ??= new LogHistorySessionService();
        /**
         * Método para agregar una sesión al historial de sesiones
         */
        public void AddLogHistorySession(LogHistorySession logHistorySession)
        {
            _logHistorySessions.Add(logHistorySession);
        }
        /**
         * Método para obtener el historial de sesiones
         */
        public List<LogHistorySession> GetLogHistorySessions()
        {
            return _logHistorySessions;
        }

        /**
         * Método para exportar el historial de sesiones a un archivo JSON
         */
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