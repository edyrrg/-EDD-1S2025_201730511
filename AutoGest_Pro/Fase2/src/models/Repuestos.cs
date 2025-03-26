namespace Fase2.src.models
{
    public class Repuestos(int Id, string NombreRepuesto, string Detalles, float Costo)
    {
        public int ID { get; set; } = Id;
        public string Repuesto { get; set; } = NombreRepuesto;
        public string Detalles { get; set; } = Detalles;
        public float Costo { get; set; } = Costo;

    }
}