using System;

namespace Fase1.src.models
{

    public unsafe struct NodoHeader<T> where T : unmanaged
    {
        public T id;
        
        public NodoHeader<T>* siguiente;
        public NodoHeader<T>* anterior;
        public NodoInterno<T>* acceso;

       
    }
}