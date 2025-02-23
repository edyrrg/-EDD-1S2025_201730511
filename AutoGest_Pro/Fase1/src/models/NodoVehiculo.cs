namespace Fase1.src.models
{
    public unsafe struct NodoVehiculo <T> where T: unmanaged
    {
        public T ID;
        public int IdUsuario;
        public string Marca;
        public string Modelo;
        public string Placa;
        public NodoVehiculo<T>* Next;

    }
}