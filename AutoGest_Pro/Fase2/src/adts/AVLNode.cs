using Fase2.src.models;

namespace Fase2.src.adts
{
    public class AVLNode(Repuestos data, AVLNode? left = null, AVLNode? right = null, int height = 0)
    {
        public Repuestos Data { get; set; } = data;
        public AVLNode? Left { get; set; } = left;
        public AVLNode? Right { get; set; } = right;
        public int Height { get; set; } = height;
    }
}