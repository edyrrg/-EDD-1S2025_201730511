namespace Fase1.src.models
{
    public class Servicio
    {
        public required int ID { get; set; }
        public required int IdRepuesto { get; set; }
        public required int IdVehiculo { get; set; }
        public required string Detalles { get; set; }
        public required float Costo { get; set; }
    }
}