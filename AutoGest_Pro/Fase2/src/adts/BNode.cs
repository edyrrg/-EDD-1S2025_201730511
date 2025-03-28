using Fase2.src.models;

namespace Fase2.src.adts
{
    public class BNode(Factura data, BNode? left = null, BNode? right = null)
    {
        public Factura Data { get; set; } = data;
        public BNode? Left { get; set; } = left;
        public BNode? Right { get; set; } = right;

    }
}