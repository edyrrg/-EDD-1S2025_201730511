namespace Fase2.src.models
{
    public class Usuario(int id, string nombres, string apellidos, string correo, string edad, string contrasenia)
    {
        public int Id { get; set; } = id;
        public string Nombres { get; set; } = nombres;
        public string Apellidos { get; set; } = apellidos;
        public string Correo { get; set; } = correo;
        public string Edad { get; set; } = edad;
        public string Contrasenia { get; set; } = contrasenia;
    }
}
