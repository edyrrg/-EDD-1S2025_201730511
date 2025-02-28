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
    public unsafe class SimpleList<T> where T : unmanaged
    {
        public NodoUsuario<int>* head = null;

        /** 
         *  Inserta un nodo en la lista simplemente enlazada
         *  @param id
         *  @param nombres
         *  @param apellidos
         *  @param correo
         *  @param Contrasenia
         *  @return void
         */
        public void Insert(int id, string nombres, string apellidos, string correo, string Contrasenia)
        {
            NodoUsuario<int>* newNode = (NodoUsuario<int>*)Marshal.AllocHGlobal(sizeof(NodoUsuario<int>));
            newNode->ID = id;
            newNode->Nombres = nombres;
            newNode->Apellidos = apellidos;
            newNode->Correo = correo;
            newNode->Contrasenia = Contrasenia;
            newNode->Next = null;

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                NodoUsuario<int>* current = head;
                while (current->Next != null)
                {
                    current = current->Next;
                }
                current->Next = newNode;
            }

        }

        /** 
         *  Busca un nodo de la lista simplemente enlazada por medio de su id
         *  @param id
         *  @return void
         */

        public int Update(int id, string nombres, string apellidos, string correo)
        {
            if (head == null) return 0;
            if (head->ID == id)
            {
                head->Nombres = nombres;
                head->Apellidos = apellidos;
                head->Correo = correo;
                return 1;
            }
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                if (current->ID == id)
                {
                    current->Nombres = nombres;
                    current->Apellidos = apellidos;
                    current->Correo = correo;
                    return 1;
                }
                current = current->Next;
            }
            return 0;
        }
        /** 
         *  Busca un nodo de la lista simplemente enlazada por medio de su id
         *  @param id
         *  @return 1 si se encontro, 0 si no se encontro
         */
        public int Search(int id)
        {
            if (head == null) return 0;
            NodoUsuario<int>* current = head;
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
        /** 
         *  Busca un nodo de la lista simplemente enlazada por medio de su id
         *  @param id
         *  @return Usuario
         */
        public Usuario? Find(int id)
        {
            if (head == null) return null;
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                if (current->ID == id)
                {
                    return new Usuario
                    {
                        ID = current->ID,
                        Nombres = current->Nombres,
                        Apellidos = current->Apellidos,
                        Correo = current->Correo,
                        Contrasenia = current->Contrasenia
                    };
                }
                current = current->Next;
            }
            return null;
        }

        /** 
         *  Elimina un nodo de la lista simplemente enlazada por medio de su id
         *  @param id
         *  @return 1 si se elimino el nodo, 0 si no se encontro
         */

        public int Delete(int id)
        {
            if (head == null) return 0;

            if (head->ID == id)
            {
                NodoUsuario<int>* temp = head;
                head = head->Next;
                Marshal.FreeHGlobal((IntPtr)temp);
                return 1;
            }

            NodoUsuario<int>* current = head;
            while (current->Next != null)
            {
                if (current->Next->ID == id)
                {
                    NodoUsuario<int>* temp = current->Next;
                    current->Next = current->Next->Next;
                    Marshal.FreeHGlobal((IntPtr)temp);
                    break;
                }
                current = current->Next;
            }
            return 1;

        }

        public void Print()
        {
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                Console.WriteLine("ID: " + current->ID);
                Console.WriteLine("Nombres: " + current->Nombres);
                Console.WriteLine("Apellidos: " + current->Apellidos);
                Console.WriteLine("Correo: " + current->Correo);
                Console.WriteLine("Contrasenia: " + current->Contrasenia);
                Console.WriteLine();
                current = current->Next;
            }
        }

        public bool GenerarReporte()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado Usuarios")
                                .Directed()
                                .WithRankDir(DotRankDir.LR)
                                .WithLabel("Listado de Usuarios");

            if (head == null) return false;
            NodoUsuario<int>* current = head;

            while (current != null)
            {
                DotNode node1 = new DotNode()
                                .WithIdentifier(current->ID.ToString())
                                .WithShape(DotNodeShape.Box)
                                .WithLabel($"ID: {current->ID}\nNombres: {current->Nombres}\nApellidos: {current->Apellidos}\nCorreo: {current->Correo}")
                                .WithFillColor(DotColor.Azure)
                                .WithFontColor(DotColor.Black);
                graph.Elements.Add(node1);
                if (current->Next != null)
                {
                    NodoUsuario<int>* next = current->Next;
                    DotNode node2 = new DotNode()
                                    .WithIdentifier(next->ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {next->ID}\nNombres: {next->Nombres}\nApellidos: {next->Apellidos}\nCorreo: {next->Correo}")
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
            File.WriteAllText("../../AutoGest_Pro/Fase1/reportes/ListadoUsuarios.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase1/reportes/ListadoUsuarios.dot -o ../../AutoGest_Pro/Fase1/reportes/ListadoUsuarios.png";

            Process.Start(startInfo);
            return true;
        }

        /** 
         *  Libera la memoria de los nodos de la lista simplemente enlazada
         *  @return void
         */
        ~SimpleList()
        {
            NodoUsuario<int>* current = head;
            while (current != null)
            {
                NodoUsuario<int>* next = current->Next;
                Marshal.FreeHGlobal((IntPtr)current);
                current = next;
            }
        }


    }
}