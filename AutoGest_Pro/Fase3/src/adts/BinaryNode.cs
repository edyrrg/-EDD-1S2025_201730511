using Fase3.src.models;

namespace Fase3.src.adts
{
    public class BinaryNode(Servicio data, BinaryNode? left = null, BinaryNode? right = null)
    {
        public Servicio Data { get; set; } = data;
        public BinaryNode? Left { get; set; } = left;
        public BinaryNode? Right { get; set; } = right;
    }
}