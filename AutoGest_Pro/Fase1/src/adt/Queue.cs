using Fase1.src.models;
using System.Runtime.InteropServices;
//Importar paquetes para graphviz
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
//Importar paquete para ejecutar el comando (este ya viene instalado)
using System.Diagnostics;

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

        public bool GenerarReporte()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado Servicios")
                                .Directed()
                                .WithRankDir(DotRankDir.LR)
                                .WithLabel("Listado de Servicios");

            if (head == null) return false;
            NodoServicio<int>* current = head;

            while (current != null)
            {
                DotNode node1 = new DotNode()
                                .WithIdentifier(current->ID.ToString())
                                .WithShape(DotNodeShape.Box)
                                .WithLabel($"ID: {current->ID}\nID Repuesto: {current->IdRepuesto}\nID Vehiculo: {current->IdVehiculo}\nDetalles: {current->Detalles}\nCosto: {current->Costo}")
                                .WithFillColor(DotColor.Azure)
                                .WithFontColor(DotColor.Black);
                graph.Elements.Add(node1);
                if (current->Next != null)
                {
                    NodoServicio<int>* next = current->Next;
                    DotNode node2 = new DotNode()
                                    .WithIdentifier(next->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {next->ID}\nID Repuesto: {next->IdRepuesto}\nID Vehiculo: {next->IdVehiculo}\nDetalles: {next->Detalles}\nCosto: {next->Costo}")
                                    .WithFillColor(DotColor.Azure)
                                    .WithFontColor(DotColor.Black)
                                    .WithWidth(0.5)
                                    .WithHeight(0.5)
                                    .WithPenWidth(1.5)
                                    .WithWidth(0.5)
                                    .WithHeight(0.5)
                                    .WithPenWidth(1.5);

                    DotEdge edge = new DotEdge()
                                    .From(node1)
                                    .To(node2)
                                    .WithArrowHead(DotEdgeArrowType.Normal)
                                    .WithColor(DotColor.Black)
                                    .WithFontColor(DotColor.Black)
                                    .WithPenWidth(1.5);
                    graph.Elements.Add(node2);
                    graph.Elements.Add(edge);
                }
                current = current->Next;
            }

            using var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            graph.CompileAsync(context);

            var result = writer.GetStringBuilder().ToString();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase1/reportes/ListadoServicios.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase1/reportes/ListadoServicios.dot -o ../../AutoGest_Pro/Fase1/reportes/ListadoServicios.png";

            Process.Start(startInfo);
            return true;
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

        ~Queue()
        {
            while (head != null)
            {
                Dequeue();
            }
        }
    }
}
