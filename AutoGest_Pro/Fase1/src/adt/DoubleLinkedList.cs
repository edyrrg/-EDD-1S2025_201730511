using Fase1.src.models;
using System;
using System.Runtime.InteropServices;

namespace Fase1.src.adt
{
    public unsafe class DoubleLinkedList<T> where T : unmanaged
    {
        private NodoVehiculo<int>* head = null;
        private NodoVehiculo<int>* tail = null;

        public void Insert(int id, int idUsuario, string marca, string modelo, string placa)
        {
            NodoVehiculo<int>* newNode = (NodoVehiculo<int>*)Marshal.AllocHGlobal(sizeof(NodoVehiculo<int>));
            newNode->ID = id;
            newNode->IdUsuario = idUsuario;
            newNode->Marca = marca;
            newNode->Modelo = modelo;
            newNode->Placa = placa;
            newNode->Next = null;
            newNode->Prev = null;

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail->Next = newNode;
                newNode->Prev = tail;
                tail = newNode;
            }
        }

        public int Search(int id)
        {
            if (head == null) return 0;
            NodoVehiculo<int>* current = head;
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

        public List<Vehiculo>? SearchVehiclesByUserId(int idUsuario)
        {
            if (head == null) return null;
            var vehicles = new List<Vehiculo>();

            NodoVehiculo<int>* current = head;
            while (current != null)
            {
                if (current->IdUsuario == idUsuario)
                {
                    var ID = current->ID;
                    var IDUsuario = current->IdUsuario;
                    var marca = current->Marca;
                    var modelo = current->Modelo;
                    var placa = current->Placa;
                    var vehiculo = new Vehiculo { ID = ID, ID_Usuario = IDUsuario, Marca = marca, Modelo = modelo, Placa = placa };
                    vehicles.Add(vehiculo);
                }
                current = current->Next;
            }
            return vehicles;
        }

        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("Lista vacia");
                return;
            }
            NodoVehiculo<int>* current = head;
            while (current != null)
            {
                Console.WriteLine("ID: " + current->ID);
                Console.WriteLine("ID Usuario: " + current->IdUsuario);
                Console.WriteLine("Marca: " + current->Marca);
                Console.WriteLine("Modelo: " + current->Modelo);
                Console.WriteLine("Placa: " + current->Placa);
                Console.WriteLine();
                current = current->Next;
            }
        }

        public int Delete(int id)
        {
            if (head == null) return 0;

            NodoVehiculo<int>* current = head;
            NodoVehiculo<int>* prev = null;
            while (current != null)
            {
                if (current->ID == id)
                {
                    if (prev == null)
                    {
                        head = current->Next;
                        if (head != null)
                        {
                            head->Prev = null;
                        }
                        else
                        {
                            tail = null;
                        }

                    }
                    else
                    {
                        prev->Next = current->Next;
                        if (current->Next != null)
                        {
                            current->Next->Prev = prev;
                        }
                        else
                        {
                            tail = prev;
                        }
                    }
                    Marshal.FreeHGlobal((IntPtr)current);
                    return 1;
                }
                prev = current;
                current = current->Next;
            }
            return 0;
        }
        ~DoubleLinkedList()
        {
            if (head == null) return;
            NodoVehiculo<int>* current = head;
            while (current != null)
            {
                NodoVehiculo<int>* tmp = current;
                current = current->Next;
                Marshal.FreeHGlobal((IntPtr)tmp);
            }
        }
    }
}