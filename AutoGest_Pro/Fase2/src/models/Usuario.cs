namespace Fase2.src.models
{
    public class Usuario(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
    {
        public int ID { get; set; } = id;
        public string Nombres { get; set; } = nombres;
        public string Apellidos { get; set; } = apellidos;
        public string Correo { get; set; } = correo;
        public int Edad { get; set; } = edad;
        public string Contrasenia { get; set; } = contrasenia;

        public override string ToString()
        {
            return $"User\nId: {ID}, Nombres: {Nombres}, Apellidos: {Apellidos}, Correo: {Correo}, Edad: {Edad}, Contrasenia: {Contrasenia}\n";
        }
    }
}
