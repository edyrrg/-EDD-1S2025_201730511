namespace Fase1.src.models
{
    public unsafe struct NodoVehiculo<T> where T : unmanaged
    {
        public T ID;
        public int IdUsuario;
        public string Marca;
        public int Modelo;
        public string Placa;
        public NodoVehiculo<T>* Next;
        public NodoVehiculo<T>* Prev;

    }
}