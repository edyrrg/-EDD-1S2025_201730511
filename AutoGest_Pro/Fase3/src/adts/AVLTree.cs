using System.Diagnostics;
using Fase3.src.models;
using Fase3.src.utils;
using Newtonsoft.Json;

namespace Fase3.src.adts
{
    public class AVLTree
    {
        private AVLNode? Root { get; set; } = null;

        public void Insert(Repuestos data)
        {
            Root = InsertRecursively(data, Root);
        }

        private AVLNode InsertRecursively(Repuestos data, AVLNode? node)
        {
            if (node == null)
            {
                return new AVLNode(data);
            }

            if (data.ID < node.Data.ID)
            {
                // Insert in left subtree
                node.Left = InsertRecursively(data, node.Left);
                if (GetHeight(node.Left) - GetHeight(node.Right) == 2)
                {
                    if (data.ID < node.Left!.Data.ID)
                    {
                        node = RotateRight(node);
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
            }
            if (data.ID > node.Data.ID)
            {
                // Insert in right subtree
                node.Right = InsertRecursively(data, node.Right);
                if (GetHeight(node.Right) - GetHeight(node.Left) == 2)
                {
                    if (data.ID > node.Right!.Data.ID)
                    {
                        node = RotateLeft(node);
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
            }
            node.Height = GetMaxHeight(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            return node;
        }
        private int GetHeight(AVLNode? node)
        {
            return node == null ? -1 : node.Height;
        }

        private int GetMaxHeight(int leftHeight, int rightHeight)
        {
            return leftHeight > rightHeight ? leftHeight : rightHeight;
        }

        private AVLNode RotateRight(AVLNode currentNode)
        {
            AVLNode newRoot = currentNode.Left!;
            currentNode.Left = newRoot.Right;
            newRoot.Right = currentNode;
            currentNode.Height = GetMaxHeight(GetHeight(currentNode.Left), GetHeight(currentNode.Right)) + 1;
            newRoot.Height = GetMaxHeight(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }

        private AVLNode RotateLeft(AVLNode currentNode)
        {
            AVLNode newRoot = currentNode.Right!;
            currentNode.Right = newRoot.Left;
            newRoot.Left = currentNode;
            currentNode.Height = GetMaxHeight(GetHeight(currentNode.Left), GetHeight(currentNode.Right)) + 1;
            newRoot.Height = GetMaxHeight(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }
        private AVLNode RotateLeftRight(AVLNode currentNode)
        {
            currentNode.Right = RotateLeft(currentNode.Right!);
            return RotateRight(currentNode);
        }
        private AVLNode RotateRightLeft(AVLNode currentNode)
        {
            currentNode.Left = RotateRight(currentNode.Left!);
            return RotateLeft(currentNode);
        }

        public AVLNode? Find(int id)
        {
            return FindRecursively(id, Root);
        }

        private AVLNode? FindRecursively(int id, AVLNode? node)
        {
            if (node == null)
            {
                return null;
            }
            if (id == node.Data.ID)
            {
                return node;
            }
            if (id < node.Data.ID)
            {
                return FindRecursively(id, node.Left);
            }
            return FindRecursively(id, node.Right);
        }
        public bool Search(int id)
        {
            return SearchRecursively(id, Root);
        }

        private bool SearchRecursively(int id, AVLNode? node)
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

        public bool Update(Repuestos data)
        {
            return UpdateRecursively(data, Root);
        }

        public bool UpdateRecursively(Repuestos data, AVLNode? node)
        {
            if (node == null)
            {
                return false;
            }
            if (data.ID == node.Data.ID)
            {
                node.Data = data;
                return true;
            }
            if (data.ID < node.Data.ID)
            {
                return UpdateRecursively(data, node.Left);
            }
            return UpdateRecursively(data, node.Right);
        }
        public bool GenerateReport()
        {
            if (Root == null) return false;

            var graph = new List<string>
                {
                    "digraph AVLTree {",
                    "node [shape=box];"
                };

            GenerateGraphvizRecursively(Root, graph);

            graph.Add("}");

            // using var writer = new StringWriter();
            // var context = new CompilationContext(writer, new CompilationOptions());
            // graph.CompileAsync(context);

            var result = string.Join("\n", graph);

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase3/Reportes/ArbolAVLRepuestos.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase3/Reportes/ArbolAVLRepuestos.dot -o ../../AutoGest_Pro/Fase3/Reportes/ArbolAVLRepuestos.png";

            Process.Start(startInfo);
            return true;
        }

        private void GenerateGraphvizRecursively(AVLNode? node, List<string> graph)
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

        public List<Repuestos> InOrder()
        {
            var list = new List<Repuestos>();
            InOrderRecursively(Root, list);
            return list;
        }

        private void InOrderRecursively(AVLNode? node, List<Repuestos> list)
        {
            if (node == null) return;
            InOrderRecursively(node.Left, list);
            list.Add(node.Data);
            InOrderRecursively(node.Right, list);
        }

        public List<Repuestos> PreOrder()
        {
            var list = new List<Repuestos>();
            PreOrderRecursively(Root, list);
            return list;
        }

        private void PreOrderRecursively(AVLNode? node, List<Repuestos> list)
        {
            if (node == null) return;
            list.Add(node.Data);
            PreOrderRecursively(node.Left, list);
            PreOrderRecursively(node.Right, list);
        }

        public List<Repuestos> PostOrder()
        {
            var list = new List<Repuestos>();
            PostOrderRecursively(Root, list);
            return list;
        }

        private void PostOrderRecursively(AVLNode? node, List<Repuestos> list)
        {
            if (node == null) return;
            PostOrderRecursively(node.Left, list);
            PostOrderRecursively(node.Right, list);
            list.Add(node.Data);
        }

        public string GenerarJsonStrings()
        {
            var list = InOrder();
            if (list.Count == 0) return "";
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            return JsonConvert.SerializeObject(list, settings);
        }

        public bool SaveBackup()
        {
            string json = GenerarJsonStrings();
            if (json == "") return false;

            var (compressed, root) = HuffmanCompression.CompressWithTree(json);
            var path = "../../AutoGest_Pro/Fase3/backups/REPUESTOS.edd";
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
