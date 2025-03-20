namespace Fase2.src.models
{
    public class Repuesto(int Id, string NombreRepuesto, string Detalles, float Costo)
    {
        public int Id { get; set; } = Id;
        public string NombreRepuesto { get; set; } = NombreRepuesto;
        public string Detalles { get; set; } = Detalles;
        public float Costo { get; set; } = Costo;

    }
}