namespace Fase3.src.adts
{
    public class Blockchain
    {
        public Block? Head { get; set; } = null;
        public Block? Tail { get; set; } = null;
        public int Size { get; set; } = 0;
        public void AddBlock(Usuario data)
        {
            string previousHash = Tail?.Hash ?? "0000";

            
        }
    }
}