namespace Fase2.src.views
{
    public class Servicio(int id, int idRespuesto, int idVehicle, string detalles, double costo)
    {
        public int ID { get; set; } = id;
        public int IdRespuesto { get; set; } = idRespuesto;
        public int IdVehicle { get; set; } = idVehicle;
        public string Detalles { get; set; } = detalles;
        public double Costo { get; set; } = costo;

        public string ToGraphvizNode()
        {
            return $"\"{ID}\" [label=\"ID:{ID}\\nRespuesto: {IdRespuesto} | Vehiculo: {IdVehicle}\\n{Detalles}\\nCosto: ${Costo}\"]";
        }
    }
}