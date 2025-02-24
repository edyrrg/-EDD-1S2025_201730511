using Fase1.src.models;
using System;
using System.Runtime.InteropServices;

namespace Fase1.src.adt
{
    public unsafe class Stack<T> where T : unmanaged
    {
        private NodoFactura<int>* top = null;
        public void Push(int id, int idOrder, float total)
        {
            NodoFactura<int>* newNode = (NodoFactura<int>*)Marshal.AllocHGlobal(sizeof(NodoFactura<T>));
            newNode->ID = id;
            newNode->IdOrden = idOrder;
            newNode->Total = total;
            newNode->Next = top;
            top = newNode;
        }

        public void Pop()
        {
            if (top == null) return;
            NodoFactura<int>* tmp = top;
            top = top->Next;
            Marshal.FreeHGlobal((IntPtr)tmp);
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