namespace Fase2.src.models
{
    public class Factura(int Id, int IdServicio, float total)
    {
        public int Id { get; set; } = Id;
        public int IdServicio { get; set; } = IdServicio;
        public float Total { get; set; } = total;
    }
}