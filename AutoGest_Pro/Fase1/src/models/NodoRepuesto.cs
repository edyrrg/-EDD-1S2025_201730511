namespace Fase1.src.models
{
    public unsafe struct NodoRepuesto <T> where T: unmanaged
    {
        public T ID;
        public string Repuesto;
        public string Detalles;
        public float Costo;
        public NodoRepuesto<T>* Next;

    }
}