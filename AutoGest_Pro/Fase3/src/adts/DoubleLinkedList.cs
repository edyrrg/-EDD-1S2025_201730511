using Fase3.src.models;
using System.Runtime.InteropServices;
//Importar paquetes para graphviz
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
//Importar paquete para ejecutar el comando (este ya viene instalado)
using System.Diagnostics;
using Newtonsoft.Json;
using Fase3.src.utils;

namespace Fase3.src.adts
{
    public class DoubleLinkedList<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public void Insert(T Data)
        {
            var newNode = new Node<T>(Data);
            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                if (_tail != null)
                {
                    _tail.Next = newNode;
                    newNode.Previous = _tail;
                }
                _tail = newNode;
            }
        }
        public bool Update(T Data)
        {
            if (_head == null)
            {
                return false;
            }
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Vehiculo)?.ID == (Data as Vehiculo)?.ID)
                {
                    current.Data = Data;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        public bool SearchById(int Id)
        {
            if (_head == null) return false;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Vehiculo)?.ID == Id)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public T? FindById(int Id)
        {
            if (_head == null) return default;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Vehiculo)?.ID == Id)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return default;
        }

        public bool Delete(int id)
        {
            if (_head == null) return false;
            if ((_head.Data as Vehiculo)?.ID == id)
            {
                _head = _head.Next;
                return true;
            }
            var current = _head;
            while (current.Next != null)
            {
                if ((current.Next.Data as Vehiculo)?.ID == id)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public List<Vehiculo> GetVehiculosByUser(int id)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            if (_head == null) return vehiculos;
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Vehiculo)?.ID_Usuario == id)
                {
                    var vehiculo = (current.Data as Vehiculo) ?? throw new Exception("No se pudo convertir el  porque es nulo");
                    vehiculos.Add(vehiculo);
                }
                current = current.Next;
            }
            return vehiculos;
        }

        public bool GenerateReport()
        {
            DotGraph graph = new DotGraph()
                                .WithIdentifier("Listado de Vehiculos")
                                .Directed()
                                .WithRankDir(DotRankDir.LR)
                                .WithLabel("Listado de Vehiculos");

            if (_head == null) return false;
            var current = _head;

            while (current != null)
            {
                if (current.Next != null)
                {
                    var next = current.Next;
                    DotNode node1 = new DotNode()
                                    .WithIdentifier((current.Data as Vehiculo)?.ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {(current.Data as Vehiculo)?.ID.ToString()}\nID Usuario: {(current.Data as Vehiculo)?.ID_Usuario.ToString()}\nMarca: {(current.Data as Vehiculo)?.Marca}\nModelo: {(current.Data as Vehiculo)?.Modelo}\nPlaca: {(current.Data as Vehiculo)?.Placa}")
                                    .WithFillColor(DotColor.Azure)
                                    .WithFontColor(DotColor.Black);

                    DotNode node2 = new DotNode()
                                    .WithIdentifier((next.Data as Vehiculo)?.ID.ToString())
                                    .WithShape(DotNodeShape.Box)
                                    .WithLabel($"ID: {(next.Data as Vehiculo)?.ID.ToString()}\nID Usuario: {(next.Data as Vehiculo)?.ID_Usuario.ToString()}\nMarca: {(next.Data as Vehiculo)?.Marca}\nModelo: {(next.Data as Vehiculo)?.Modelo.ToString()}\nPlaca: {(next.Data as Vehiculo)?.Placa}")
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
                current = current.Next;
            }

            using var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            graph.CompileAsync(context);

            var result = writer.GetStringBuilder().ToString();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase3/Reportes/ListadoVehiculos.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase3/Reportes/ListadoVehiculos.dot -o ../../AutoGest_Pro/Fase3/Reportes/ListadoVehiculos.png";

            Process.Start(startInfo);
            return true;
        }

        public string GenerarJsonStrings()
        {
            if (_head == null) return "";
            var vehiculosData = new List<Vehiculo>();
            var current = _head;

            while (current != null)
            {
                var vehiculo = (current.Data as Vehiculo) ?? throw new Exception("No se pudo convertir el  porque es nulo");
                vehiculosData.Add(vehiculo);
                current = current.Next;
            }
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            return JsonConvert.SerializeObject(vehiculosData, settings);
        }

        public bool SaveBackup()
        {
            string json = GenerarJsonStrings();
            if (json == "") return false;

            var (compressed, root) = HuffmanCompression.CompressWithTree(json);
            var path = "../../AutoGest_Pro/Fase3/backups/VEHICULOS.edd";
            string? directoryPath = Path.GetDirectoryName(path);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fileStream))
                {
                    writer.Write(compressed.Length);
                    writer.Write(compressed);
                    writer.Write(root?.ToString() ?? string.Empty);
                }
            }
            return true;
        }
    }
}