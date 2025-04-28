using System.Diagnostics;
using System.Text;
using Fase3.src.models;

namespace Fase3.src.adts
{
    public class Graph
    {
        private NodeGraph<Vehiculo>? _head;

        public void AddNode(Vehiculo vehiculo, Repuestos repuesto)
        {
            NodeGraph<Vehiculo> newNode = new(vehiculo);

            if (_head == null)
            {
                _head = newNode;
                _head.Repuestos.Add(repuesto);
            }
            else
            {
                var currentNode = _head;
                while (currentNode != null)
                {
                    if (currentNode.Data.ID == vehiculo.ID)
                    {
                        if (!currentNode.SearchRepuesto(repuesto))
                        {
                            currentNode.Repuestos.Add(repuesto);
                        }
                        return;
                    }
                    currentNode = currentNode.Next as NodeGraph<Vehiculo>;
                }
                newNode.Repuestos.Add(repuesto);
                newNode.Next = _head;
                _head = newNode;
            }
        }

        public string GenerateGraphvizDot()
        {
            var dot = new StringBuilder();
            dot.AppendLine("digraph Grafo {");
            dot.AppendLine("label=\"Grafo de Vehiculos y Repuestos\";");
            dot.AppendLine("labelloc=\"t\";");
            dot.AppendLine("fontsize=20;");
            dot.AppendLine("rankdir=LR;");
            dot.AppendLine("node [shape=circle];");
            dot.AppendLine("edge [arrowhead=none];");
            var currentNode = _head;
            while (currentNode != null)
            {
                string id_vehiculo = "V" + currentNode.Data.ID;
                dot.AppendLine($"{id_vehiculo} [label=\"{id_vehiculo}\"];");
                foreach (var repuesto in currentNode.Repuestos)
                {
                    string id_repuesto = "R" + repuesto.ID;
                    dot.AppendLine($"{id_repuesto} [label=\"{id_repuesto}\"];");
                    dot.AppendLine($"{id_vehiculo} -> {id_repuesto};");
                }
                currentNode = currentNode.Next as NodeGraph<Vehiculo>;
            }

            dot.AppendLine("}");
            // Console.WriteLine(dot.ToString());
            return dot.ToString();
        }

        public bool GenerateReport()
        {
            if (_head == null) return false;

            var result = GenerateGraphvizDot();

            File.WriteAllText("../../AutoGest_Pro/Fase3/Reportes/GrafoNoDirigidoVyR.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase3/Reportes/GrafoNoDirigidoVyR.dot -o ../../AutoGest_Pro/Fase3/Reportes/GrafoNoDirigidoVyR.png";

            Process.Start(startInfo);
            return true;
        }
    }
}