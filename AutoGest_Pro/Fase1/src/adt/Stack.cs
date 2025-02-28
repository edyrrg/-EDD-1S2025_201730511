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
            var Factura = new Factura
            {
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
        public bool GenerarReporte()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado Facturas")
                                .Directed()
                                .WithRankDir(DotRankDir.TB)
                                .WithLabel("Listado de Facturas");

            if (top == null) return false;
            NodoFactura<int>* current = top;

            while (current != null)
            {
                DotNode node1 = new DotNode()
                                .WithIdentifier(current->ID.ToString())
                                .WithShape(DotNodeShape.Box)
                                .WithLabel($"ID: {current->ID}\nID Orden: {current->IdOrden}\nTotal: {current->Total}")
                                .WithFillColor(DotColor.Azure)
                                .WithFontColor(DotColor.Black);
                graph.Elements.Add(node1);
                if (current->Next != null)
                {
                    NodoFactura<int>* next = current->Next;
                    DotNode node2 = new DotNode()
                                    .WithIdentifier(next->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {next->ID}\nID Orden: {next->IdOrden}\nTotal: {next->Total}")
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
                                    .WithArrowTail(DotEdgeArrowType.Normal)
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
            File.WriteAllText("../../AutoGest_Pro/Fase1/reportes/ListadoFacturas.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase1/reportes/ListadoFacturas.dot -o ../../AutoGest_Pro/Fase1/reportes/ListadoFacturas.png";

            Process.Start(startInfo);
            return true;
        }

        ~Stack()
        {
            while (top != null)
            {
                NodoFactura<int>* tmp = top;
                top = top->Next;
                Marshal.FreeHGlobal((IntPtr)tmp);
            }
        }
    }
}