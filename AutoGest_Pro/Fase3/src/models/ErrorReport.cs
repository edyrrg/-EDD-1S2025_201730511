using System.Text;

namespace Fase3.src.models
{
    public class ErrorReport(List<MyError> duplicateEntities)
    {
        public List<MyError> DuplicateEntities { get;} = duplicateEntities;

        public string GenerateReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Reporte de errores:");
            foreach (var myError in DuplicateEntities)
            {
                sb.AppendLine($"Entidad: {myError.Entity} - Error: {myError.Reason}");
            }
            return sb.ToString();
        }
    }
}