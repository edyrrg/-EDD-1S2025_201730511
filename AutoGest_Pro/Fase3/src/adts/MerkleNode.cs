using System.Security.Cryptography;
using System.Text;
using Fase3.src.models;

namespace Fase3.src.adts
{
    public class MerkleNode
    {
        public Factura? Data { get; set; }
        public string Hash { get; set; }
        public MerkleNode? Left { get; set; }
        public MerkleNode? Right { get; set; }


        public MerkleNode(Factura data)
        {
            Data = data;
            Hash = Data.GetHash();
            Left = null;
            Right = null;
        }
        public MerkleNode(MerkleNode left, MerkleNode? right)
        {
            Data = null;
            Left = left;
            Right = right;
            Hash = CalculateHash(left.Hash, right?.Hash);
        }

        private string CalculateHash(string leftHash, string? rightHash)
        {
            string combinedHash = leftHash + (rightHash ?? "");

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedHash));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}