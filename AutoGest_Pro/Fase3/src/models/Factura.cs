namespace Fase3.src.models
{
    public class Factura(int Id, int IdServicio, float total, DateTime? fecha = null, string metodoPago = "")
    {
        public int Id { get; set; } = Id;
        public int IdServicio { get; set; } = IdServicio;
        public float Total { get; set; } = total;
        public DateTime? Fecha { get; set; } = fecha;
        public string MetodoPago { get; set; } = metodoPago;
    }
}