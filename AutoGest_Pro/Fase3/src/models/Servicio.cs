namespace Fase3.src.models
{
    public class Servicio(int id, int idRespuesto, int idVehicle, string detalles, float costo)
    {
        public int ID { get; set; } = id;
        public int IdRespuesto { get; set; } = idRespuesto;
        public int IdVehicle { get; set; } = idVehicle;
        public string Detalles { get; set; } = detalles;
        public float Costo { get; set; } = costo;

        public string ToGraphvizNode()
        {
            return $"\"{ID}\" [label=\"ID:{ID}\\nRespuesto: {IdRespuesto} | Vehiculo: {IdVehicle}\\n{Detalles}\\nCosto: ${Costo}\"]";
        }
    }
}