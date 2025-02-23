namespace Fase1.src.models
{
    public unsafe struct NodoServicio <T> where T: unmanaged
    {
        public T ID;
        public int IdRepuesto;
        public int IdVehiculo;
        public string Detalles;
        public string Costo;
        public NodoServicio<T>* Next;

    }
}