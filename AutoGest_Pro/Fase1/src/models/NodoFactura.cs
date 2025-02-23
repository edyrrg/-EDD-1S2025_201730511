namespace Fase1.src.models
{
    public unsafe struct NodoFactura <T> where T: unmanaged
    {
        public T ID;
        public int IdOrden;
        public string Total;
        public NodoFactura<T>* Next;

    }
}