using Fase3.src.models;

namespace Fase3.src.adts
{
    public class Block(Usuario data, string previousHash)
    {
        public static int Count { get; set; } = 0;
        public int Index { get; set; } = Count++;
        public Usuario Data { get; set; } = data;
        public string PreviousHash { get; set; } = previousHash;
        public string? Hash { get; set; }
        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public int? Nonce { get; set; }
        public Block? next { get; set; }
        public Block? previous { get; set; }

    }
}
