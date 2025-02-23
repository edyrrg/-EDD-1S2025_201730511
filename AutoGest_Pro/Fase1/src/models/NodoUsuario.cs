using System.Data;

namespace Fase1.src.models
{
    public unsafe struct NodoUsuario <T> where T: unmanaged
    {   
        public T ID;
        public string Nombres;
        public string Apellidos;
        public string Correo;
        public string Contrasenia;
        public NodoUsuario<T>* Next;

    }
}