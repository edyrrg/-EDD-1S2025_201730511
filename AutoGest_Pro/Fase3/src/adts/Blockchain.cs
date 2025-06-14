using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Fase3.src.models;
using Fase3.src.utils;
using Newtonsoft.Json;

namespace Fase3.src.adts
{
    public class Blockchain
    {
        public Block? Head { get; private set; } = null;
        public Block? Tail { get; private set; } = null;
        public int Size { get; private set; } = 0;
        // --- Constantes para la Minería ---
        // private const int Difficulty = 4; // Dificultad fija
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
            // Console.WriteLine($"Block Mined! Index: {block.Index}, Nonce: {block.Nonce}, Hash: {block.Hash}");
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
                if (Tail != null)
                {
                    Tail.next = newBlock;
                }
                newBlock.previous = Tail;
                Tail = newBlock;
            }
            Size++;
            // Console.WriteLine($"Block added: {newBlock}");
        }

        public void Add(Block block)
        {
            if (Head == null)
            {
                Head = block;
                Tail = block;
            }
            else
            {
                if (Tail != null)
                {
                    Tail.next = block;
                }
                block.previous = Tail;
                Tail = block;
            }
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

        public Usuario? FindByEmail(string email)
        {
            Block? current = Head;
            while (current != null)
            {
                if (current.Data.Correo == email)
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
                        color=""#222831"",
                        arrowhead=normal,
                        penwidth=2
                    ];
                            ");

            // Colores para alternar en los nodos (opcional)
            string[] colors = {
                "#FFF2F2", "#A9B5DF", "#7886C7", "#3674B5",
                "#578FCA", "#A1E3F9", "#D1F8EF", "#98D8EF"
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

                string safePassword = current.Data.Contrasenia != null
                                      ? current.Data.Contrasenia.Substring(0, Math.Min(current.Data.Contrasenia.Length, 12))
                                      : "N/A";

                // Formatear los datos del Usuario para mostrarlos (ajusta según necesites)
                // Escapar caracteres HTML si los datos pudieran contenerlos (ej. '&' -> "&amp;")
                string userDataHtml = $"ID: {current.Data.ID}<br/>" +
                                      $"Nombres: {System.Security.SecurityElement.Escape(current.Data.Nombres)}<br/>" +
                                      $"Apellidos: {System.Security.SecurityElement.Escape(current.Data.Apellidos)}<br/>" +
                                      $"Correo: {System.Security.SecurityElement.Escape(current.Data.Correo)}<br/>" +
                                      $"Edad: {System.Security.SecurityElement.Escape(current.Data.Edad.ToString())}<br/>" +
                                      $"Contraseña: {safePassword}...<br/>";
                // Definición del nodo usando formato HTML-like de Graphviz
                nodesBuilder.AppendFormat(
                    @"    block{0} [label=<
                        <table border=""0"" cellborder=""1"" cellspacing=""0"" cellpadding=""5"" style=""rounded"" bgcolor=""{1}"">
                            <tr><td colspan=""2"" bgcolor=""#2D336B"" align=""center""><font color=""white""><b>Bloque #{0}</b></font></td></tr>
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

        public string GenerarBlockToJson()
        {
            var blockchainData = new List<Block>();

            if (Head == null) return "";

            Block? current = Head;

            while (current != null)
            {
                blockchainData.Add(current);
                current = current.next;
            }

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            return JsonConvert.SerializeObject(blockchainData, settings);
        }

        public bool GuardarBackup()
        {
            string json = GenerarBlockToJson();
            if (json == "") return false;

            string filePath = "../../AutoGest_Pro/Fase3/backups/blockchain-users.json";

            // Verificar si el directorio de backups existe, si no, crearlo
            string? directoryPath = Path.GetDirectoryName(filePath);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            // Guardar el JSON en un archivo
            File.WriteAllText(filePath, json);
            return true;
        }

        public bool CargarBackup()
        {
            string filePath = "../../AutoGest_Pro/Fase3/backups/blockchain-users.json";
            if (!File.Exists(filePath))
            {
                return false; // No existe el archivo de backup
            }
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            };

            var blockchainData = JsonConvert.DeserializeObject<List<Block>>(json, options);
            if (blockchainData == null)
            {
                return false; // Error al deserializar el JSON
            }

            foreach (var block in blockchainData)
            {
                Add(block);
            }

            if(IsChainValid() == false)
            {
                Head = null;
                return false;
            }

            return true;
        }

        public bool IsChainValid()
        {
            Block? current = Head;
            if (current == null) return false;  
            if (current.PreviousHash != "0000") return false;

            while (current.next != null)
            {
                Block nextBlock = current.next;
                if (nextBlock.PreviousHash != current.Hash) return false;
                current = nextBlock;
            }

            if(current != Tail) return false;

            return true;
        }
    }
}