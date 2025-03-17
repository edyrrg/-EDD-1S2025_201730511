using Fase1.src.models;
using System;
using System.Runtime.InteropServices;
//Importar paquetes para graphviz
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
//Importar paquete para ejecutar el comando (este ya viene instalado)
using System.Diagnostics;

namespace Fase1.src.adt
{
    public unsafe class DoubleLinkedList<T> where T : unmanaged
    {
        private NodoVehiculo<int>* head = null;
        private NodoVehiculo<int>* tail = null;

        public void Insert(int id, int idUsuario, string marca, int modelo, string placa)
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

        public bool GenerarReporte()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado de Vehiculos")
                                .Directed()
                                .WithRankDir(DotRankDir.LR)
                                .WithLabel("Listado de Vehiculos");

            if (head == null) return false;
            NodoVehiculo<int>* current = head;

            while (current != null)
            {
                if (current->Next != null)
                {
                    NodoVehiculo<int>* next = current->Next;
                    DotNode node1 = new DotNode()
                                    .WithIdentifier(current->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {current->ID}\nID Usuario: {current->IdUsuario}\nMarca: {current->Marca}\nModelo: {current->Modelo}\nPlaca: {current->Placa}")
                                    .WithFillColor(DotColor.Azure)
                                    .WithFontColor(DotColor.Black);
                
                    DotNode node2 = new DotNode()
                                    .WithIdentifier(next->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {next->ID}\nID Usuario: {next->IdUsuario}\nMarca: {next->Marca}\nModelo: {next->Modelo}\nPlaca: {next->Placa}")
                                    .WithFillColor(DotColor.Azure)
                                    .WithFontColor(DotColor.Black)
                                    .WithWidth(0.5)
                                    .WithHeight(0.5)
                                    .WithPenWidth(1.5)
                                    .WithWidth(0.5)
                                    .WithHeight(0.5)
                                    .WithPenWidth(1.5);

                    DotEdge edge1 = new DotEdge()
                                    .From(node1)
                                    .To(node2)
                                    .WithArrowHead(DotEdgeArrowType.Normal)
                                    .WithColor(DotColor.Black)
                                    .WithFontColor(DotColor.Black)
                                    .WithPenWidth(1.5);
                    
                    DotEdge edge2 = new DotEdge()
                                    .From(node2)
                                    .To(node1)
                                    .WithArrowHead(DotEdgeArrowType.Normal)
                                    .WithColor(DotColor.Black)
                                    .WithFontColor(DotColor.Black)
                                    .WithPenWidth(1.5);
                    
                    graph.Elements.Add(node1);
                    graph.Elements.Add(node2);
                    graph.Elements.Add(edge1);
                    graph.Elements.Add(edge2);
                }
                current = current->Next;
            }

            using var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            graph.CompileAsync(context);

            var result = writer.GetStringBuilder().ToString();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase1/reportes/ListadoVehiculos.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase1/reportes/ListadoVehiculos.dot -o ../../AutoGest_Pro/Fase1/reportes/ListadoVehiculos.png";

            Process.Start(startInfo);
            return true;
        }

        public void TopCincoVehiculosMasAntiguos()
        {
            if (head == null)
            {
                Console.WriteLine("Lista vacia");
                return;
            }

            List<Vehiculo> vehiculos = new List<Vehiculo>();
            NodoVehiculo<int>* current = head;

            while (current != null)
            {
                var vehiculo = new Vehiculo 
                { 
                    ID = current->ID, 
                    ID_Usuario = current->IdUsuario, 
                    Marca = current->Marca, 
                    Modelo = current->Modelo, 
                    Placa = current->Placa 
                };
                vehiculos.Add(vehiculo);
                current = current->Next;
            }

            var topCinco = vehiculos.OrderBy(v => v.Modelo).Take(5).ToList();

            Console.WriteLine("Top 5 Vehículos Más Antiguos:");
            foreach (var vehiculo in topCinco)
            {
                Console.WriteLine($"ID: {vehiculo.ID}, Marca: {vehiculo.Marca}, Modelo: {vehiculo.Modelo}, Placa: {vehiculo.Placa}");
            }
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