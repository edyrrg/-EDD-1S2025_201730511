using Fase2.src.models;

namespace Fase2.src.adts
{
    public class SimpleLinkedList<T>
    {
        private Node<T>? _head;

        public void Insert(T Data)
        {
            var newNode = new Node<T>(Data);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public bool Update(T Data)
        {
            if (_head == null)
            {
                return false;
            }
            if ((_head.Data as Usuario)?.Id == (Data as Usuario)?.Id)
            {
                _head.Data = Data;
                return true;
            }
            var current = _head;
            while (current != null)
            {
                if ((current.Data as Usuario)?.Id == (Data as Usuario)?.Id)
                {
                    current.Data = Data;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

    }
}