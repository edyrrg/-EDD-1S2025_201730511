using Fase3.src.models;

namespace Fase3.src.adts
{
    public class NodeGraph<T> : Node<T>
    {
        public List<Repuestos> Repuestos { get; set; } = [];
        public NodeGraph(T data) : base(data) { }

        public bool SearchRepuesto(Repuestos repuesto)
        {
            return Repuestos.Any(r => r.ID == repuesto.ID);
        }
    }
}