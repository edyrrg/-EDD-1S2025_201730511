using Fase1.src.models;
using System;
using System.Runtime.InteropServices;

namespace Fase1.src.adt
{
    public unsafe class SimpleList<T> where T : unmanaged
    {
        public NodoUsuario<int>* head = null;

        /** 
         *  Inserta un nodo en la lista simplemente enlazada
         *  @param id
         *  @param nombres
         *  @param apellidos
         *  @param correo
         *  @param Contrasenia
         *  @return void
         */
        public void Insert(int id, string nombres, string apellidos, string correo, string Contrasenia)
        {
            NodoUsuario<int>* newNode = (NodoUsuario<int>*)Marshal.AllocHGlobal(sizeof(NodoUsuario<int>));
            newNode->ID = id;
            newNode->Nombres = nombres;
            newNode->Apellidos = apellidos;
            newNode->Correo = correo;
            newNode->Contrasenia = Contrasenia;
            newNode->Next = null;

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                NodoUsuario<int>* current = head;
                while (current->Next != null)
                {
                    current = current->Next;
                }
                current->Next = newNode;
            }

        }

        /** 
         *  Busca un nodo de la lista simplemente enlazada por medio de su id
         *  @param id
         *  @return void
         */

        public int Udpate(int id, string nombres, string apellidos, string correo, string Contrasenia)
        {
            if (head == null) return 0;
            if (head->ID == id)
            {
                head->Nombres = nombres;
                head->Apellidos = apellidos;
                head->Correo = correo;
                head->Contrasenia = Contrasenia;
                return 1;
            }
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                if (current->ID == id)
                {
                    current->Nombres = nombres;
                    current->Apellidos = apellidos;
                    current->Correo = correo;
                    current->Contrasenia = Contrasenia;
                    break;
                }
                current = current->Next;
            }
            return 1;
        }

        public int Search(int id)
        {
            if (head == null) return 0;
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                if (current->ID == id)
                {
                    Console.WriteLine("ID: " + current->ID);
                    Console.WriteLine("Nombres: " + current->Nombres);
                    Console.WriteLine("Apellidos: " + current->Apellidos);
                    Console.WriteLine("Correo: " + current->Correo);
                    Console.WriteLine("Contrasenia: " + current->Contrasenia);
                    return 1;
                }
                current = current->Next;
            }
            return 0;
        }

        /** 
         *  Elimina un nodo de la lista simplemente enlazada por medio de su id
         *  @param id
         *  @return 1 si se elimino el nodo, 0 si no se encontro
         */

        public int Delete(int id)
        {
            if (head == null) return 0;

            if (head->ID == id)
            {
                NodoUsuario<int>* temp = head;
                head = head->Next;
                Marshal.FreeHGlobal((IntPtr)temp);
                return 1;
            }

            NodoUsuario<int>* current = head;
            while (current->Next != null)
            {
                if (current->Next->ID == id)
                {
                    NodoUsuario<int>* temp = current->Next;
                    current->Next = current->Next->Next;
                    Marshal.FreeHGlobal((IntPtr)temp);
                    break;
                }
                current = current->Next;
            }
            return 1;

        }

        public void Print()
        {
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                Console.WriteLine("ID: " + current->ID);
                Console.WriteLine("Nombres: " + current->Nombres);
                Console.WriteLine("Apellidos: " + current->Apellidos);
                Console.WriteLine("Correo: " + current->Correo);
                Console.WriteLine("Contrasenia: " + current->Contrasenia);
                Console.WriteLine();
                current = current->Next;
            }
        }

        /** 
         *  Libera la memoria de los nodos de la lista simplemente enlazada
         *  @return void
         */
        ~SimpleList()
        {
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                NodoUsuario<int>* next = current->Next;
                Marshal.FreeHGlobal((IntPtr)current);
                current = next;
            }
        }


    }
}