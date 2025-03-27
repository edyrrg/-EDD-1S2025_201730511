namespace Fase2.src.models
{
    public class Vehiculo(int ID, int ID_Usuario, string Marca, int Modelo, string Placa)
    {
        public int ID { get; set; } = ID;
        public int ID_Usuario { get; set; } = ID_Usuario;
        public string Marca { get; set; } = Marca;
        public int Modelo { get; set; } = Modelo;
        public string Placa { get; set; } = Placa;
    }
}