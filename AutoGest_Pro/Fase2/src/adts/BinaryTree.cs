using System.ComponentModel;
using System.Diagnostics;
using Fase2.src.models;

namespace Fase2.src.adts
{
    public class BinaryTree
    {
        private BinaryNode? Root { get; set; } = null;

        public void Insert(Servicio data)
        {
            Root = InsertRecursively(data, Root);
        }

        private BinaryNode InsertRecursively(Servicio data, BinaryNode? node)
        {
            if (node == null)
            {
                return new BinaryNode(data);
            }

            if (data.ID < node.Data.ID)
            {
                // Insert in left subtree
                node.Left = InsertRecursively(data, node.Left);
            }
            else if (data.ID > node.Data.ID)
            {
                // Insert in right subtree
                node.Right = InsertRecursively(data, node.Right);
            }
            else
            {
                // Manejar duplicados (opcional)
                throw new InvalidOperationException($"El ID {data.ID} ya existe en el árbol.");
            }
            return node;
        }

        public bool Search(int id)
        {
            return SearchRecursively(id, Root);
        }

        private bool SearchRecursively(int id, BinaryNode? node)
        {
            if (node == null)
            {
                return false;
            }

            if (id == node.Data.ID)
            {
                return true;
            }

            if (id < node.Data.ID)
            {
                return SearchRecursively(id, node.Left);
            }
            return SearchRecursively(id, node.Right);
        }
        public List<Servicio> InOrder()
        {
            var listResult = new List<Servicio>();
            InOrderRecursively(Root, listResult);
            return listResult;
        }
        public void InOrderRecursively(BinaryNode? node, List<Servicio> listResult)
        {
            if (node == null)
            {
                return;
            }
            InOrderRecursively(node.Left, listResult);
            listResult.Add(node.Data);
            InOrderRecursively(node.Right, listResult);
        }

        public List<Servicio> PreOrder()
        {
            var listResult = new List<Servicio>();
            PreOrderRecursively(Root, listResult);
            return listResult;
        }
        public void PreOrderRecursively(BinaryNode? node, List<Servicio> listResult)
        {
            if (node == null)
            {
                return;
            }
            listResult.Add(node.Data);
            PreOrderRecursively(node.Left, listResult);
            PreOrderRecursively(node.Right, listResult);
        }
        public List<Servicio> PostOrder()
        {
            var listResult = new List<Servicio>();
            PostOrderRecursively(Root, listResult);
            return listResult;
        }
        public void PostOrderRecursively(BinaryNode? node, List<Servicio> listResult)
        {
            if (node == null)
            {
                return;
            }
            PostOrderRecursively(node.Left, listResult);
            PostOrderRecursively(node.Right, listResult);
            listResult.Add(node.Data);
        }

        public bool GenerateReport()
        {
            if (Root == null) return false;

            var graph = new List<string>
                {
                    "digraph BinaryTree {",
                    "node [shape=ellipse];"
                };

            GenerateGraphvizRecursively(Root, graph);

            graph.Add("}");

            // using var writer = new StringWriter();
            // var context = new CompilationContext(writer, new CompilationOptions());
            // graph.CompileAsync(context);

            var result = string.Join("\n", graph);

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase2/Reportes/ArbolBinarioServicios.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase2/Reportes/ArbolBinarioServicios.dot -o ../../AutoGest_Pro/Fase2/Reportes/ArbolBinarioServicios.png";

            Process.Start(startInfo);
            return true;
        }
        private void GenerateGraphvizRecursively(BinaryNode? node, List<string> graph)
        {
            if (node == null) return;

            // Agregar el nodo actual
            graph.Add(node.Data.ToGraphvizNode());

            // Agregar las conexiones con los hijos
            if (node.Left != null)
            {
                graph.Add($"\"{node.Data.ID}\" -> \"{node.Left.Data.ID}\";");
                GenerateGraphvizRecursively(node.Left, graph);
            }
            if (node.Right != null)
            {
                graph.Add($"\"{node.Data.ID}\" -> \"{node.Right.Data.ID}\";");
                GenerateGraphvizRecursively(node.Right, graph);
            }
        }
    }
}