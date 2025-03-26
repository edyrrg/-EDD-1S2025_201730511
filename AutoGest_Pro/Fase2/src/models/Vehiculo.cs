namespace Fase2.src.models
{
    public class Vehiculo(int Id, int IdUsuario, string Marca, int Modelo, string Placa)
    {
        public int ID { get; set; } = Id;
        public int IDUsuario { get; set; } = IdUsuario;
        public string Marca { get; set; } = Marca;
        public int Modelo { get; set; } = Modelo;
        public string Placa { get; set; } = Placa;
    }
}