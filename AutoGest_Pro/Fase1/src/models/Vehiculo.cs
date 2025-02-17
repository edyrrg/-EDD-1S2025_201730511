namespace Fase1.src.models
{
    public class Vehiculo
    {
        public int ID { get; set; }
        public int ID_Usuario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public Vehiculo(int id, int idUsuario, string marca, string modelo, string placa)
        {
            ID = id;
            ID_Usuario = idUsuario;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
        }
    }
}