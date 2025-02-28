using Fase1.src.models;
using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Fase1.src.adt
{
    public unsafe class Queue<T> where T : unmanaged
    {
        private NodoServicio<int>* head = null;
        private NodoServicio<int>* tail = null;

        public void Enqueue(int id, int IdRepuesto, int IdVehiculo, string detalles, float costo)
        {
            NodoServicio<int>* newNode = (NodoServicio<int>*)Marshal.AllocHGlobal(sizeof(NodoServicio<T>));
            newNode->ID = id;
            newNode->IdRepuesto = IdRepuesto;
            newNode->IdVehiculo = IdVehiculo;
            newNode->Detalles = detalles;
            newNode->Costo = costo;
            newNode->Next = null;

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail->Next = newNode;
                tail = newNode;
            }
        }
        public void Dequeue()
        {
            if (head == null) return;

            if (head == tail)
            {
                Marshal.FreeHGlobal((IntPtr)head);
                head = null;
                tail = null;
                return;
            }

            NodoServicio<int>* tmp = head;
            head = head->Next;
            Marshal.FreeHGlobal((IntPtr)tmp);
        }

        public int Search(int id)
        {
            if (head == null) return 0;

            NodoServicio<int>* current = head;
            while (current != null)
            {
                if (current->ID == id)
                {
                    return 1;
                }
                current = current->Next;
            }
            return 0;
        }

        public Servicio? Find(int id)
        {
            if (head == null) return null;

            NodoServicio<int>* current = head;
            while (current != null)
            {
                if (current->ID == id)
                {
                    return new Servicio
                    {
                        ID = current->ID,
                        IdRepuesto = current->IdRepuesto,
                        IdVehiculo = current->IdVehiculo,
                        Detalles = current->Detalles,
                        Costo = current->Costo
                    };
                }
                current = current->Next;
            }
            return null;

        }
        public bool IsEmpty()
        {
            return head == null;
        }
        public void Print()
        {
            if (head == null) return;

            NodoServicio<int>* current = head;
            while (current != null)
            {
                Console.WriteLine($"ID: {current->ID}");
                Console.WriteLine($"ID Repuesto: {current->IdRepuesto}");
                Console.WriteLine($"ID Vehiculo: {current->IdVehiculo}");
                Console.WriteLine($"Detalles: {current->Detalles}");
                Console.WriteLine($"Costo: {current->Costo}");
                Console.WriteLine();
                current = current->Next;
            }
        }
    }
}
