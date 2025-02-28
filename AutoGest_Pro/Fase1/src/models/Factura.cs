namespace Fase1.src.models
{
    public class Factura
    {
        public int? ID { get; set; }
        public int IdOrden { get; set; }
        public float Total { get; set; }
    }
}