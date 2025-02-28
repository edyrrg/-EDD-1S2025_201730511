using System.Runtime.InteropServices;
using Fase1.src.models;
//Importar paquetes para graphviz
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
//Importar paquete para ejecutar el comando (este ya viene instalado)
using System.Diagnostics;

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

        public bool GenerarReporte()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado Respuestos")
                                .Directed()
                                .WithRankDir(DotRankDir.LR)
                                .WithLabel("Listado de Respuestos");

            if (head == null) return false;
            NodoRepuesto<int>* current = head;
            DotNode headNode = new DotNode()
                                .WithIdentifier(current->ID.ToString())
                                .WithShape(DotNodeShape.Box)
                                .WithLabel($"ID: {current->ID}\nRepuesto: {current->Repuesto}\nDetalles: {current->Detalles}\nCosto: {current->Costo}")
                                .WithFillColor(DotColor.Azure)
                                .WithFontColor(DotColor.Black);
            graph.Elements.Add(headNode);
            do
            {
                DotNode node1 = new DotNode()
                                    .WithIdentifier(current->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {current->ID}\nRepuesto: {current->Repuesto}\nDetalles: {current->Detalles}\nCosto: {current->Costo}")
                                    .WithFillColor(DotColor.Azure)
                                    .WithFontColor(DotColor.Black);
                graph.Elements.Add(node1);

                if (current->Next != head)
                {

                    NodoRepuesto<int>* next = current->Next;

                    DotNode node2 = new DotNode()
                                    .WithIdentifier(next->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {next->ID}\nRepuesto: {next->Repuesto}\nDetalles: {next->Detalles}\nCosto: {next->Costo}")
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
            } while (current->Next != head);

            DotNode lastNode = new DotNode()
                                .WithIdentifier(current->ID.ToString())
                                .WithShape(DotNodeShape.Box)
                                .WithLabel($"ID: {current->ID}\nRepuesto: {current->Repuesto}\nDetalles: {current->Detalles}\nCosto: {current->Costo}")
                                .WithFillColor(DotColor.Azure)
                                .WithFontColor(DotColor.Black);

            DotEdge finalEdge = new DotEdge()
                                .From(lastNode)
                                .To(headNode)
                                .WithArrowHead(DotEdgeArrowType.Normal)
                                .WithColor(DotColor.Black)
                                .WithFontColor(DotColor.Black)
                                .WithPenWidth(1.5);

            graph.Elements.Add(finalEdge);

            using var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            graph.CompileAsync(context);

            var result = writer.GetStringBuilder().ToString();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase1/reportes/ListadoRepuestos.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase1/reportes/ListadoRepuestos.dot -o ../../AutoGest_Pro/Fase1/reportes/ListadoRespuestos.png";

            Process.Start(startInfo);
            return true;
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