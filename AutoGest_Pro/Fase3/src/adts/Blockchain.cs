using System.Diagnostics;
using System.Text;
using Fase3.src.models;
using Fase3.src.utils;

namespace Fase3.src.adts
{
    public class Blockchain
    {
        public Block? Head { get; private set; } = null;
        public Block? Tail { get; private set; } = null;
        public int Size { get; private set; } = 0;
        // --- Constantes para la Minería ---
        private const int Difficulty = 4; // Dificultad fija
        private const string ProofOfWorkPrefix = "0000"; // Prefijo requerido (4 ceros) 
        private void MineBlock(Block block)
        {
            string hash = block.CalculateHash();
            // Bucle de minería hasta encontrar un hash válido
            // Usamos Nonce.Value porque Nonce es nullable int (int?)
            while (!hash.StartsWith(ProofOfWorkPrefix))
            {
                // Incrementa el Nonce. Si es null (aunque lo inicializas a 0), empieza desde 0.
                block.Nonce = (block.Nonce ?? -1) + 1;
                hash = block.CalculateHash(); // Recalcula el hash con el nuevo Nonce
                // Opcional: Mostrar progreso (puede ralentizar si hay muchos datos)
                //Console.WriteLine($"Mining... Nonce: {block.Nonce}, Hash: {hash}");
            }
            // Hash válido encontrado, asignarlo al bloque
            block.Hash = hash;
            Console.WriteLine($"Block Mined! Index: {block.Index}, Nonce: {block.Nonce}, Hash: {block.Hash}");
        }

        public void AddBlock(Usuario data)
        {
            string previousHash = Tail?.Hash ?? "0000";
            Block newBlock = new Block(Size, data, previousHash);
            MineBlock(newBlock);
            if (Head == null)
            {
                Head = newBlock;
                Tail = newBlock;
            }
            else
            {
                Tail.next = newBlock;
                newBlock.previous = Tail;
                Tail = newBlock;
            }
            Size++;
            Console.WriteLine($"Block added: {newBlock}");
        }

        public bool SearchByEmailAndPass(string email, string password)
        {
            var encryptedPassword = Encrypt.GetSHA256(password);
            Block? current = Head;
            while (current != null)
            {
                if (current.Data.Correo == email && current.Data.Contrasenia == encryptedPassword)
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }

        public Usuario? FindUserById(int id)
        {
            Block? current = Head;
            while (current != null)
            {
                if (current.Data.ID == id)
                {
                    return current.Data;
                }
                current = current.next;
            }
            return null;
        }

        public bool SearchByID(int id)
        {
            Block? current = Head;
            while (current != null)
            {
                if (current.Data.ID == id)
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }

        public bool SearchByEmail(string email)
        {
            Block? current = Head;
            while (current != null)
            {
                if (current.Data.Correo == email)
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }

        public string GenerateGraphvizDot()
        {
            var dotBuilder = new StringBuilder();

            // Encabezado del grafo DOT
            dotBuilder.AppendLine(@"digraph Blockchain {
                    label=""Blockchain - Usuarios""; 
                    labelloc=t;
                    fontsize=20;
                    rankdir=LR;
                    bgcolor=""#f8f9fa"";

                    node [
                        shape=none,
                        margin=0
                    ];

                    edge [
                        color=""#7e57c2"",
                        arrowhead=normal,
                        penwidth=2
                    ];
                            ");

            // Colores para alternar en los nodos (opcional)
            string[] colors = {
                "#e3f2fd", "#bbdefb", "#90caf9", "#64b5f6",
                "#42a5f5", "#2196f3", "#1e88e5", "#1976d2"
            };

            var nodesBuilder = new StringBuilder();
            var connectionsBuilder = new StringBuilder();
            Block? current = Head;

            // Recorrer la cadena para generar nodos y conexiones
            while (current != null)
            {
                int colorIndex = current.Index % colors.Length;
                string nodeColor = colors[colorIndex];

                // Obtener hashes de forma segura y acortarlos para visualización
                string safeHash = current.Hash != null
                                  ? current.Hash.Substring(0, Math.Min(current.Hash.Length, 12))
                                  : "N/A";
                // El PreviousHash del bloque génesis será "0" según AddBlock
                string safePrevHash = current.PreviousHash != null
                                      ? current.PreviousHash.Substring(0, Math.Min(current.PreviousHash.Length, 12))
                                      : "N/A";

                // Formatear los datos del Usuario para mostrarlos (ajusta según necesites)
                // Escapar caracteres HTML si los datos pudieran contenerlos (ej. '&' -> "&amp;")
                string userDataHtml = $"ID: {current.Data.ID}<br/>" +
                                      $"Nombre: {System.Security.SecurityElement.Escape(current.Data.Nombres)}<br/>" +
                                      $"Correo: {System.Security.SecurityElement.Escape(current.Data.Correo)}<br/>";
                // Definición del nodo usando formato HTML-like de Graphviz
                nodesBuilder.AppendFormat(
                    @"    block{0} [label=<
                        <table border=""0"" cellborder=""1"" cellspacing=""0"" cellpadding=""5"" style=""rounded"" bgcolor=""{1}"">
                            <tr><td colspan=""2"" bgcolor=""#1565c0"" align=""center""><font color=""white""><b>Bloque #{0}</b></font></td></tr>
                            <tr><td align=""right""><b>Timestamp:</b></td><td>{2}</td></tr>
                            <tr><td align=""right""><b>Nonce:</b></td><td>{3}</td></tr>
                            <tr><td align=""right"" valign=""top""><b>Data (Usuario):</b></td><td align=""left"">{4}</td></tr>
                            <tr><td align=""right""><b>Hash:</b></td><td><font face=""Courier New"" point-size=""10"">{5}...</font></td></tr>
                            <tr><td align=""right""><b>PrevHash:</b></td><td><font face=""Courier New"" point-size=""10"">{6}...</font></td></tr>
                        </table>
                    >];{7}",
                    current.Index,                  // {0}
                    nodeColor,                      // {1}
                    current.Timestamp,              // {2}
                    current.Nonce?.ToString() ?? "N/A", // {3} - Manejo de Nonce nullable
                    userDataHtml,                   // {4} - Datos del usuario formateados
                    safeHash,                       // {5}
                    safePrevHash,                   // {6}
                    Environment.NewLine             // {7} - Salto de línea
                );

                // Definición de las conexiones (flechas)
                // Usamos 'previous' en minúscula como en tu Block.cs
                if (current.previous != null)
                {
                    // Flecha hacia adelante (del previo al actual)
                    connectionsBuilder.AppendFormat(
                        "    block{0} -> block{1};{2}",
                        current.previous.Index, // {0}
                        current.Index,          // {1}
                        Environment.NewLine     // {2}
                    );
                    // Flecha hacia atrás (del actual al previo) - punteada
                    connectionsBuilder.AppendFormat(
                        "    block{1} -> block{0};{2}",
                        current.previous.Index, // {0}
                        current.Index,          // {1}
                        Environment.NewLine     // {2}
                    );
                }

                // Avanzar al siguiente bloque
                // Usamos 'next' en minúscula como en tu Block.cs
                current = current.next;
            }

            // Ensamblar el código DOT final
            dotBuilder.Append(nodesBuilder.ToString());
            dotBuilder.Append(connectionsBuilder.ToString());
            dotBuilder.AppendLine("}"); // Cerrar el grafo

            return dotBuilder.ToString();
        }

        public bool GenerateReport()
        {
            var result = GenerateGraphvizDot();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase3/Reportes/Blockchain-usuarios.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase3/Reportes/Blockchain-usuarios.dot -o ../../AutoGest_Pro/Fase3/Reportes/Blockchain-usuarios.png";

            Process.Start(startInfo);
            return true;
        }
    }
}