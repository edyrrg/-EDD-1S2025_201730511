namespace Fase1.src.models
{
    public class Vehiculo    
    {
        public required int Id { get; set; }
        public required int IdUsuario { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required string Placa { get; set; }
    
    }
}