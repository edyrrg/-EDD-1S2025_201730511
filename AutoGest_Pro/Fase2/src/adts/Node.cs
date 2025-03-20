namespace Fase2.src.adts
{
    /**
     * Clase que representa un nodo de una lista enlazada simple.
     */
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }
}