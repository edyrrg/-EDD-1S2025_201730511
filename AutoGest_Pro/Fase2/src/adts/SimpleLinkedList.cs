using Fase2.src.models;

using System;
using System.Runtime.InteropServices;
//Importar paquetes para graphviz
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
//Importar paquete para ejecutar el comando (este ya viene instalado)
using System.Diagnostics;

namespace Fase2.src.adts
{
    public class SimpleLinkedList<T>
    {
        private Node<T>? _head;

        public void Insert(T Data)
        {
            var newNode = new Node<T>(Data);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public bool Update(T Data)
        {
            if (_head == null)
            {
                return false;
            }
            if ((_head.Data as Usuario)?.ID == (Data as Usuario)?.ID)
            {
                _head.Data = Data;
                return true;
            }
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.ID == (Data as Usuario)?.ID)
                {
                    current.Data = Data;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public bool SearchByID(int id)
        {
            if (_head == null) return false;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.ID == id)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        public bool SearchByEmail(string email)
        {
            if (_head == null) return false;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.Correo == email)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public T? FindByEmail(string email)
        {
            if (_head == null) return default;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.Correo == email)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return default;

        }

        public T? Find(int id)
        {
            if (_head == null) return default;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.ID == id)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return default;

        }

        public bool Delete(T Data)
        {
            if (_head == null) return false;

            if ((_head.Data as Usuario)?.ID == (Data as Usuario)?.ID)
            {
                _head = _head.Next;
                return true;
            }

            var current = _head;
            while (current.Next != null)
            {
                if ((current.Next.Data as Usuario)?.ID == (Data as Usuario)?.ID)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        public void Print()
        {
            var current = _head;
            while (current != null)
            {
                System.Console.WriteLine(current.Data?.ToString());
                current = current.Next;
            }
        }
        public bool DeleteById(int id)
        {
            if (_head == null) return false;

            if ((_head.Data as Usuario)?.ID == id)
            {
                _head = _head.Next;
                return true;
            }

            var current = _head;
            while (current.Next != null)
            {
                if ((current.Next.Data as Usuario)?.ID == id)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public bool SearchByEmailAndPass(string email, string pass)
        {
            if (_head == null) return false;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.Correo == email && (current.Data as Usuario)?.Contrasenia == pass)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public bool GenerateReport()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado Usuarios")
                                .Directed()
                                .WithRankDir(DotRankDir.LR)
                                .WithLabel("Listado de Usuarios");

            if (_head == null) return false;
            var current = _head;

            while (current != null)
            {
                var currUser = current.Data as Usuario;
                DotNode node1 = new DotNode()
                                .WithIdentifier(currUser?.ID.ToString())
                                .WithShape(DotNodeShape.Box)
                                .WithLabel($"ID: {currUser?.ID.ToString()}\nNombres: {currUser?.Nombres}\nApellidos: {currUser?.Apellidos}\nCorreo: {currUser?.Correo}\nEdad: {currUser?.Edad}")
                                .WithFillColor(DotColor.Azure)
                                .WithFontColor(DotColor.Black);
                graph.Elements.Add(node1);
                if (current.Next != null)
                {
                    var nextUser = current.Next.Data as Usuario;
                    DotNode node2 = new DotNode()
                                    .WithIdentifier(nextUser?.ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {nextUser?.ID.ToString()}\nNombres: {nextUser?.Nombres}\nApellidos: {nextUser?.Apellidos}\nCorreo: {nextUser?.Correo}\nEdad: {nextUser?.Edad}")
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
                current = current.Next;
            }

            using var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            graph.CompileAsync(context);

            var result = writer.GetStringBuilder().ToString();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase2/Reportes/ListadoUsuarios.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase2/Reportes/ListadoUsuarios.dot -o ../../AutoGest_Pro/Fase2/Reportes/ListadoUsuarios.png";

            Process.Start(startInfo);
            return true;
        }
    }
}