namespace Fase2.src.adts
{
    public class DoubleLinkedList<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public void Insert(T Data)
        {
            var newNode = new Node<T>(Data);
            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                if (_tail != null)
                {
                    _tail.Next = newNode;
                    newNode.Previous = _tail;
                }
                _tail = newNode;
            }
        }


    }
}