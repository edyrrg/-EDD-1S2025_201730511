namespace Fase2.src.models
{
    public class Repuestos(int ID, string Repuesto, string Detalles, float Costo)
    {
        public int ID { get; set; } = ID;
        public string Repuesto { get; set; } = Repuesto;
        public string Detalles { get; set; } = Detalles;
        public float Costo { get; set; } = Costo;
        public string ToGraphvizNode()
        {
            return $"\"{ID}\" [label=\"ID: {ID}\\nDetalles: {Detalles}\\nRespuesto: {Repuesto}\\nPrecio: {Costo}\"]";
        }
    }
}