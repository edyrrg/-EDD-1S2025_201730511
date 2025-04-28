using System.Diagnostics;
using System.Text;
using Fase3.src.models;

namespace Fase3.src.adts
{
    public class MerkleTree
    {
        public MerkleNode? Root { get; set; } = null;
        public List<MerkleNode> Leaves { get; set; } = [];

        public void Add(Factura data)
        {
            foreach (var leaf in Leaves)
            {
                if (leaf.Data?.Id == data.Id)
                {
                    return;
                }
            }

            var newLeaf = new MerkleNode(data);
            Leaves.Add(newLeaf);
            BuildTree();
        }

        public void BuildTree()
        {
            if (Leaves.Count == 0)
            {
                return;
            }

            List<MerkleNode> currentLevel = Leaves;

            while (currentLevel.Count > 1)
            {
                List<MerkleNode> nextLevel = new List<MerkleNode>();

                for (int i = 0; i < currentLevel.Count; i += 2)
                {
                    MerkleNode left = currentLevel[i];
                    MerkleNode? right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : null;
                    MerkleNode parent = new MerkleNode(left, right);
                    nextLevel.Add(parent);
                }
                currentLevel = nextLevel;
            }
            Root = currentLevel[0];
        }

        public void Delete(Factura data)
        {
            for (int i = 0; i < Leaves.Count; i++)
            {
                if (Leaves[i].Data?.Id == data.Id)
                {
                    Leaves.RemoveAt(i);
                    break;
                }
            }
            BuildTree();
        }

        public Factura? Find(int id)
        {
            foreach (var leaf in Leaves)
            {
                if (leaf.Data?.Id == id)
                {
                    return leaf.Data;
                }
            }
            return null;
        }

        public bool Verify(Factura data)
        {
            foreach (var leaf in Leaves)
            {
                if (leaf.Data?.Id == data.Id)
                {
                    return leaf.Hash == data.GetHash();
                }
            }
            return false;
        }

        public Factura? BuscarPorServicio(int idServicio)
        {
            foreach (var leaf in Leaves)
            {
                if (leaf.Data?.IdServicio == idServicio)
                {
                    return leaf.Data;
                }
            }
            return null;
        }

        public bool Search(int id)
        {
            foreach (var leaf in Leaves)
            {
                if (leaf.Data?.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateMetodoPago(int id, MetodoPago metodoPago)
        {
            foreach (var leaf in Leaves)
            {
                if (leaf.Data?.Id == id)
                {
                    leaf.Data.MetodoPago = metodoPago;
                    return true;
                }
            }
            return false;
        }


        public void GenerarDotRecursive(MerkleNode node, StringBuilder dot, Dictionary<string, int> nodeIds, ref int idCounter)
        {

            if (node == null) return;

            if (!nodeIds.ContainsKey(node.Hash))
            {
                nodeIds[node.Hash] = idCounter++;
            }

            int nodeId = nodeIds[node.Hash];

            string label;
            if (node.Data != null)
            {
                label = $"\"ID Factura {node.Data.Id}\\nID Servicio: {node.Data.IdServicio}\\nFecha: {node.Data.Fecha}\\nTotal: {node.Data.Total}\\nMétodo de Pago: {node.Data.MetodoPago.ToString()}\\nHash: {node.Hash.Substring(0, 8)}...\"";
            }
            else
            {
                label = $"\"Hash: {node.Hash.Substring(0, 8)}...\"";
            }
            dot.AppendLine($"  node{nodeId} [label={label}];");

            if (node.Left != null)
            {
                if (!nodeIds.ContainsKey(node.Left.Hash))
                {
                    nodeIds[node.Left.Hash] = idCounter++;
                }
                int leftId = nodeIds[node.Left.Hash];
                dot.AppendLine($"  node{nodeId} -> node{leftId};");
                GenerarDotRecursive(node.Left, dot, nodeIds, ref idCounter);

            }

            if (node.Right != null)
            {
                if (!nodeIds.ContainsKey(node.Right.Hash))
                {
                    nodeIds[node.Right.Hash] = idCounter++;
                }
                int rightId = nodeIds[node.Right.Hash];
                dot.AppendLine($"  node{nodeId} -> node{rightId};");
                GenerarDotRecursive(node.Right, dot, nodeIds, ref idCounter);

            }

        }
        public string GenerateGraphvizDot()
        {
            var dot = new StringBuilder();
            dot.AppendLine("digraph MerkleTree {");
            dot.AppendLine("  node [shape=record];");
            dot.AppendLine("  graph [rankdir=TB];");
            dot.AppendLine("  subgraph cluster_0 {");
            dot.AppendLine("    label=\"Facturas\";");

            Dictionary<string, int> nodeIds = new Dictionary<string, int>();
            int idCounter = 0;
            if (Root != null) { GenerarDotRecursive(Root, dot, nodeIds, ref idCounter); }

            dot.AppendLine("  }");
            dot.AppendLine("}");
            Console.WriteLine(dot.ToString());
            return dot.ToString();
        }

        public bool GenerateReport()
        {
            if (Root == null) return false;

            var dot = GenerateGraphvizDot();
            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase3/Reportes/Arbol-Merkle-Facturas.dot", dot);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase3/Reportes/Arbol-Merkle-Facturas.dot -o ../../AutoGest_Pro/Fase3/Reportes/Arbol-Merkle-Facturas.png";

            Process.Start(startInfo);
            return true;
        }
    }
}

