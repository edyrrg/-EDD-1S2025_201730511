using System;
using System.Runtime.InteropServices;
using Fase1.src.models;

namespace Fase1.src.adt


{
    public unsafe class HeaderList<T> where T : unmanaged
    {
        public NodoHeader<int>* primero;
        public NodoHeader<int>* ultimo;
        public string tipo;
        public int size;

        public HeaderList(string tipo)
        {
            primero = null;
            ultimo = null;
            this.tipo = tipo;
            size = 0;
        }

        public void Add_NodoHeader(int id)
        {

            //Creacion del nodo
            NodoHeader<int>* nuevo = (NodoHeader<int>*)Marshal.AllocHGlobal(sizeof(NodoHeader<int>));
            if (nuevo == null)
            {
                throw new InvalidOperationException("No se pudo asignar memoria para el nuevo nodo.");
            }

            //Asignar valores, metodo 1
            nuevo->id = id;
            nuevo->siguiente = null;
            nuevo->anterior = null;
            nuevo->acceso = null;

            size = size + 1;

            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                //Insercion en orden
                //Verificar si el nuevo es menor que el primero
                if (nuevo->id < primero->id)
                {
                    nuevo->siguiente = primero;
                    primero->anterior = nuevo;
                    primero = nuevo;
                }
                //Verificamos si el nuevo es mayor al ultimo
                else if (nuevo->id > ultimo->id)
                {
                    ultimo->siguiente = nuevo;
                    nuevo->anterior = ultimo;
                    ultimo = nuevo;
                }
                else
                {
                    //Sino, recorremos la lista para buscar donde acomodarnos, entre el primro y el ultimo
                    NodoHeader<int>* tmp = primero;
                    while (tmp != null)
                    {
                        if (nuevo->id < tmp->id)
                        {
                            nuevo->siguiente = tmp;
                            nuevo->anterior = tmp->anterior;
                            tmp->anterior->siguiente = nuevo;
                            tmp->anterior = nuevo;
                            break;
                        }
                        else if (nuevo->id > tmp->id)
                        {
                            tmp = tmp->siguiente;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }





        }

        public void Show()
        {

            if (primero == null)
            {
                Console.WriteLine("Lista vacía.");
                return;
            }

            NodoHeader<int>* tmp = primero;
            while (tmp != null)
            {
                Console.WriteLine("Encabezado " + tipo + " " + Convert.ToString(tmp->id));
                tmp = tmp->siguiente;
            }
        }

        public NodoHeader<int>* getHead(int id)
        {
            NodoHeader<int>* tmp = primero;
            while (tmp != null)

            {

                if (id == tmp->id)
                {

                    return tmp;
                }
                tmp = tmp->siguiente;
            }

            return null;

        }

        ~HeaderList()
        {
            if (primero == null) return;

            while (primero != null)
            {
                NodoHeader<int>* tmp = primero;
                primero = primero->siguiente;
                Marshal.FreeHGlobal((IntPtr)tmp);

            }


        }
    }
}