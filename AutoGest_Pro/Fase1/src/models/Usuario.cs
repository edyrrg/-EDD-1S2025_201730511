namespace Fase1.src.models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }

        public Usuario(int id, string nombres, string apellidos, string correo, string contrasenia)
        {
            ID = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Contrasenia = contrasenia;
        }
    }
}