using Fase3.src.models;
using Gdk;

namespace Fase3.src.adts
{
    public class BNode
    {
        private const int ORDEN = 5;
        private const int MAX_CLAVES = ORDEN - 1;
        private const int MIN_CLAVES = (ORDEN / 2) - 1;

        public List<Factura> Keys { get; set; }
        public List<BNode> Childs { get; set; }
        public bool isLeaf { get; set; }

        public BNode()
        {
            Keys = new List<Factura>(MAX_CLAVES);
            Childs = new List<BNode>(ORDEN);
            isLeaf = true;
        }

        // Verifica si el nodo está lleno
        public bool EstaLleno()
        {
            return Keys.Count >= MAX_CLAVES;
        }

        // Verifica si el nodo tiene el mínimo de claves requerido
        public bool TieneMinimoClaves()
        {
            return Keys.Count >= MIN_CLAVES;
        }

    }
}