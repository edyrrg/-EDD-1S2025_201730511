namespace Fase2.src.models
{
    public class LogHistorySession(string userName, string entrada)
    {
        public string UserName { get; set; } = userName;
        public string Entrada { get; set; } = entrada;
        public string? Salida { get; set; }
    }
}
