using Fase3.src.models;
using System.Security.Cryptography;
using System.Text;

namespace Fase3.src.adts
{
    public class Block(int index, Usuario data, string previousHash)
    {
        public int Index { get; } = index;
        public Usuario Data { get; } = data;
        public string PreviousHash { get; } = previousHash;
        public string Timestamp { get; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string? Hash { get; internal set; }
        public int? Nonce { get; internal set; } = 0;
        public Block? next { get; set; }
        public Block? previous { get; set; }
        public string CalculateHash()
        {
            using SHA256 sha256 = SHA256.Create();
            string str = $"{Index}{Timestamp}{Data}{Nonce}{PreviousHash}";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public override string ToString()
        {
            return $"Index: {Index}\nTimestamp: {Timestamp}\nData: {Data}\nNonce: {Nonce}\nPreviousHash: {PreviousHash}\nHash: {Hash}";
        }
    }
}
