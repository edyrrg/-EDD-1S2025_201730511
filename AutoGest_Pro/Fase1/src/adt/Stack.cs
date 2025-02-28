using Fase1.src.models;
using System;
using System.Runtime.InteropServices;

namespace Fase1.src.adt
{
    public unsafe class Stack<T> where T : unmanaged
    {
        private int countId = 0;
        private NodoFactura<int>* top = null;
        public void Push(int idOrder, float total)
        {
            NodoFactura<int>* newNode = (NodoFactura<int>*)Marshal.AllocHGlobal(sizeof(NodoFactura<T>));
            newNode->ID = ++countId;
            newNode->IdOrden = idOrder;
            newNode->Total = total;
            newNode->Next = top;
            top = newNode;
        }

        public Factura? Pop()
        {
            if (top == null) return null;
            NodoFactura<int>* tmp = top;
            top = top->Next;
            var Factura = new Factura{
                ID = tmp->ID,
                IdOrden = tmp->IdOrden,
                Total = tmp->Total
            };
            Marshal.FreeHGlobal((IntPtr)tmp);
            return Factura;
        }

        public bool IsEmpty()
        {
            return top == null;
        }

        public void Print()
        {
            if (top == null) return;
            NodoFactura<int>* current = top;
            while (current != null)
            {
                Console.WriteLine($"ID: {current->ID}");
                Console.WriteLine($"ID Orden: {current->IdOrden}");
                Console.WriteLine($"Total: {current->Total}");
                current = current->Next;
            }
        }
    }
}