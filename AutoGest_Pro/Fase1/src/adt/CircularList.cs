using System.Runtime.InteropServices;
using Fase1.src.models;

namespace Fase1.src.adt
{
    public unsafe class CircularList<T> where T : unmanaged
    {
        private NodoRepuesto<int>* head = null;
        public void Insert(int id, string repuesto, string detalles, float costo)
        {
            NodoRepuesto<int>* newNode = (NodoRepuesto<int>*)Marshal.AllocHGlobal(sizeof(NodoRepuesto<T>));
            newNode->ID = id;
            newNode->Repuesto = repuesto;
            newNode->Detalles = detalles;
            newNode->Costo = costo;
            newNode->Next = null;

            if (head == null)
            {
                head = newNode;
                newNode->Next = head;
            }
            else
            {
                NodoRepuesto<int>* current = head;
                while (current->Next != head)
                {
                    current = current->Next;
                }
                current->Next = newNode;
                newNode->Next = head;
            }
        }

        public int Search(int id)
        {
            if (head == null) return 0;

            NodoRepuesto<int>* current = head;
            do
            {
                if (current->ID == id) return 1;
                current = current->Next;
            } while (current != head);

            return 0;
        }

        public RepuestoModel? Find(int id)
        {
            if (head == null) return null;

            NodoRepuesto<int>* current = head;
            do
            {
                if (current->ID == id)
                {
                    return new RepuestoModel
                    {
                        ID = current->ID,
                        Repuesto = current->Repuesto,
                        Detalles = current->Detalles,
                        Costo = current->Costo
                    };
                }
                current = current->Next;
            } while (current != head);

            return null;
        }

        public int Delete(int id)
        {
            if (head == null) return 0;

            if (head->ID == id && head->Next == head)
            {
                Marshal.FreeHGlobal((IntPtr)head);
                head = null;
                return 1;
            }

            NodoRepuesto<int>* current = head;
            NodoRepuesto<int>* prev = null;
            do
            {
                if (current->ID == id)
                {
                    if (prev != null)
                    {
                        prev->Next = current->Next;
                    }
                    else
                    {
                        NodoRepuesto<int>* last = head;
                        while (last->Next != head)
                        {
                            last = last->Next;
                        }
                        head = current->Next;
                        last->Next = head;
                    }
                    Marshal.FreeHGlobal((IntPtr)current);
                    return 1;
                }
                prev = current;
                current = current->Next;
            } while (current != head);

            return 0;
        }
        public void Print()
        {
            if (head == null) return;

            NodoRepuesto<int>* current = head;
            do
            {
                System.Console.WriteLine($"ID: {current->ID}, Repuesto: {current->Repuesto}, Detalles: {current->Detalles}, Costo: {current->Costo}");
                current = current->Next;
            } while (current != head);
        }

        ~CircularList()
        {
            if (head == null) return;

            NodoRepuesto<int>* current = head;
            do
            {
                NodoRepuesto<int>* tmp = current;
                current = current->Next;
                Marshal.FreeHGlobal((IntPtr)tmp);
            } while (current != head);
        }
    }
}