namespace Fase1.src.models
{
    public class Repuesto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }

        public Repuesto(int id, string nombre, string detalles, double costo)
        {
            ID = id;
            Nombre = nombre;
            Detalles = detalles;
            Costo = costo;
        }

    }
}