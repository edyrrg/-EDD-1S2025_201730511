using System.Diagnostics;
using System.Text;
using Fase3.src.models;

namespace Fase3.src.adts
{
    public class BTree
    {
        private BNode root { get; set; } = new BNode();
        private const int ORDER = 5;
        private const int MAX_KEYS = ORDER - 1;
        private const int MIN_KEYS = (ORDER / 2) - 1;

        // Método para insertar un nuevo Factura
        public void Insertar(Factura data)
        {
            // Si la raíz está llena, se crea una nueva raíz
            if (root.EstaLleno())
            {
                BNode newRoot = new BNode();
                newRoot.isLeaf = false;
                newRoot.Childs.Add(root);
                DividirHijo(newRoot, 0);
                root = newRoot;
            }

            InsertarNoLleno(root, data);
        }

        // Divide un hijo cuando está lleno durante la inserción
        private void DividirHijo(BNode parent, int indexChild)
        {
            BNode childComplete = parent.Childs[indexChild];
            BNode nuevoHijo = new BNode();
            nuevoHijo.isLeaf = childComplete.isLeaf;

            // Factura del medio que se promoverá al padre
            Factura FacturaMedio = childComplete.Keys[MIN_KEYS];

            // Mover la mitad de las claves al nuevo hijo
            for (int i = MIN_KEYS + 1; i < MAX_KEYS; i++)
            {
                nuevoHijo.Keys.Add(childComplete.Keys[i]);
            }

            // Si no es hoja, mover también los hijos correspondientes
            if (!childComplete.isLeaf)
            {
                for (int i = (ORDER / 2); i < ORDER; i++)
                {
                    nuevoHijo.Childs.Add(childComplete.Childs[i]);
                }
                childComplete.Childs.RemoveRange((ORDER / 2), childComplete.Childs.Count - (ORDER / 2));
            }

            // Eliminar las claves movidas del hijo original
            childComplete.Keys.RemoveRange(MIN_KEYS, childComplete.Keys.Count - MIN_KEYS);

            // Insertar el nuevo hijo en el padre
            parent.Childs.Insert(indexChild + 1, nuevoHijo);

            // Insertar la clave media en el padre
            int j = 0;
            while (j < parent.Keys.Count && parent.Keys[j].Id < FacturaMedio.Id)
            {
                j++;
            }
            parent.Keys.Insert(j, FacturaMedio);
        }

        // Inserta un Factura en un nodo que no está lleno
        private void InsertarNoLleno(BNode nodo, Factura Factura)
        {
            int i = nodo.Keys.Count - 1;

            // Si es hoja, simplemente inserta el Factura en orden
            if (nodo.isLeaf)
            {
                // Buscar la posición correcta para insertar
                while (i >= 0 && Factura.Id < nodo.Keys[i].Id)
                {
                    i--;
                }
                nodo.Keys.Insert(i + 1, Factura);
            }
            else
            {
                // Encuentra el hijo donde debe estar el Factura
                while (i >= 0 && Factura.Id < nodo.Keys[i].Id)
                {
                    i--;
                }
                i++;

                // Si el hijo está lleno, divídelo primero
                if (nodo.Childs[i].EstaLleno())
                {
                    DividirHijo(nodo, i);
                    if (Factura.Id > nodo.Keys[i].Id)
                    {
                        i++;
                    }
                }
                InsertarNoLleno(nodo.Childs[i], Factura);
            }
        }

        public Factura? BuscarPorServicio(int idServicio)
        {
            return BuscarPorServicioRecursivo(root, idServicio);
        }

        public Factura? BuscarPorServicioRecursivo(BNode nodo, int idServicio)
        {
            if (nodo == null)
                return null;

            for (int i = 0; i < nodo.Keys.Count; i++)
            {
                if (nodo.Keys[i].IdServicio == idServicio)
                {
                    return nodo.Keys[i];
                }
            }

            // Si no es hoja, buscar en los hijos
            if (!nodo.isLeaf)
            {
                for (int i = 0; i <= nodo.Keys.Count; i++)
                {
                    Factura? resultado = BuscarPorServicioRecursivo(nodo.Childs[i], idServicio);
                    if (resultado != null)
                        return resultado;
                }
            }

            return null;
        }  

        public bool Search(int id)
        {
            return BuscarRecursivo(root, id) != null;
        }  

        // Busca un Factura por su Id
        public Factura? Buscar(int id)
        {
            return BuscarRecursivo(root, id);
        }

        private Factura? BuscarRecursivo(BNode nodo, int id)
        {
            int i = 0;
            // Buscar la primera clave mayor o igual que id
            while (i < nodo.Keys.Count && id > nodo.Keys[i].Id)
            {
                i++;
            }

            // Si encontramos el id, devolvemos el Factura
            if (i < nodo.Keys.Count && id == nodo.Keys[i].Id)
            {
                return nodo.Keys[i];
            }

            // Si es una hoja y no encontramos el id, no existe
            if (nodo.isLeaf)
            {
                return null;
            }

            // Si no es hoja, buscamos en el hijo correspondiente
            return BuscarRecursivo(nodo.Childs[i], id);
        }

        // Método para eliminar un Factura por su Id
        public void Eliminar(int id)
        {
            EliminarRecursivo(root, id);

            // Si la raíz quedó vacía pero tiene hijos, el primer hijo se convierte en la nueva raíz
            if (root.Keys.Count == 0 && !root.isLeaf)
            {
                root = root.Childs[0];
            }
        }

        private void EliminarRecursivo(BNode nodo, int id)
        {
            int indice = EncontrarIndice(nodo, id);

            // Caso 1: La clave está en este nodo
            if (indice < nodo.Keys.Count && nodo.Keys[indice].Id == id)
            {
                // Si es hoja, simplemente eliminamos
                if (nodo.isLeaf)
                {
                    nodo.Keys.RemoveAt(indice);
                }
                else
                {
                    // Si no es hoja, usamos estrategias más complejas
                    EliminarDeNodoInterno(nodo, indice);
                }
            }
            else
            {
                // Caso 2: La clave no está en este nodo
                if (nodo.isLeaf)
                {
                    Console.WriteLine($"El Factura con Id {id} no existe en el árbol");
                    return;
                }

                // Determinar si el último hijo fue visitado
                bool ultimoHijo = indice == nodo.Keys.Count;

                // Si el hijo tiene el mínimo de claves, rellenarlo
                if (!nodo.Childs[indice].TieneMinimoClaves())
                {
                    RellenarHijo(nodo, indice);
                }

                // Si el último hijo se fusionó, recurrimos al hijo anterior
                if (ultimoHijo && indice > nodo.Childs.Count - 1)
                {
                    EliminarRecursivo(nodo.Childs[indice - 1], id);
                }
                else
                {
                    EliminarRecursivo(nodo.Childs[indice], id);
                }
            }
        }

        // Encuentra el índice de la primera clave mayor o igual a id
        private int EncontrarIndice(BNode nodo, int id)
        {
            int indice = 0;
            while (indice < nodo.Keys.Count && nodo.Keys[indice].Id < id)
            {
                indice++;
            }
            return indice;
        }

        // Elimina un Factura de un nodo interno
        private void EliminarDeNodoInterno(BNode nodo, int indice)
        {
            Factura clave = nodo.Keys[indice];

            // Caso 2a: Si el hijo anterior tiene más del mínimo de claves
            if (nodo.Childs[indice].Childs.Count > MIN_KEYS)
            {
                // Reemplazar clave con el predecesor
                Factura predecesor = ObtenerPredecesor(nodo, indice);
                nodo.Keys[indice] = predecesor;
                EliminarRecursivo(nodo.Childs[indice], predecesor.Id);
            }
            // Caso 2b: Si el hijo siguiente tiene más del mínimo de claves
            else if (nodo.Childs[indice + 1].Keys.Count > MIN_KEYS)
            {
                // Reemplazar clave con el sucesor
                Factura sucesor = ObtenerSucesor(nodo, indice);
                nodo.Keys[indice] = sucesor;
                EliminarRecursivo(nodo.Childs[indice + 1], sucesor.Id);
            }
            // Caso 2c: Si ambos hijos tienen el mínimo de claves
            else
            {
                // Fusionar el hijo actual con el siguiente
                FusionarNodos(nodo, indice);
                EliminarRecursivo(nodo.Childs[indice], clave.Id);
            }
        }

        // Obtiene el predecesor de una clave (la clave más grande en el subárbol izquierdo)
        private Factura ObtenerPredecesor(BNode nodo, int indice)
        {
            BNode actual = nodo.Childs[indice];
            while (!actual.isLeaf)
            {
                actual = actual.Childs[actual.Keys.Count];
            }
            return actual.Keys[actual.Keys.Count - 1];
        }

        // Obtiene el sucesor de una clave (la clave más pequeña en el subárbol derecho)
        private Factura ObtenerSucesor(BNode nodo, int indice)
        {
            BNode actual = nodo.Childs[indice + 1];
            while (!actual.isLeaf)
            {
                actual = actual.Childs[0];
            }
            return actual.Keys[0];
        }

        // Rellena un hijo que tiene menos del mínimo de claves
        private void RellenarHijo(BNode nodo, int indice)
        {
            // Si el hermano izquierdo existe y tiene más del mínimo de claves
            if (indice > 0 && nodo.Childs[indice - 1].Keys.Count > MIN_KEYS)
            {
                TomaPrestadoDelAnterior(nodo, indice);
            }
            // Si el hermano derecho existe y tiene más del mínimo de claves
            else if (indice < nodo.Keys.Count && nodo.Childs[indice + 1].Childs.Count > MIN_KEYS)
            {
                TomaPrestadoDelSiguiente(nodo, indice);
            }
            // Si no se puede tomar prestado, fusionar con un hermano
            else
            {
                if (indice < nodo.Keys.Count)
                {
                    FusionarNodos(nodo, indice);
                }
                else
                {
                    FusionarNodos(nodo, indice - 1);
                }
            }
        }

        // Toma prestado una clave del hermano anterior
        private void TomaPrestadoDelAnterior(BNode nodo, int indice)
        {
            BNode hijo = nodo.Childs[indice];
            BNode hermano = nodo.Childs[indice - 1];

            // Desplazar todas las claves e hijos para hacer espacio para la nueva clave
            hijo.Keys.Insert(0, nodo.Keys[indice - 1]);

            // Si no es hoja, mover también el hijo correspondiente
            if (!hijo.isLeaf)
            {
                hijo.Childs.Insert(0, hermano.Childs[hermano.Keys.Count]);
                hermano.Childs.RemoveAt(hermano.Keys.Count);
            }

            // Actualizar la clave del padre
            nodo.Keys[indice - 1] = hermano.Keys[hermano.Keys.Count - 1];
            hermano.Keys.RemoveAt(hermano.Keys.Count - 1);
        }

        // Toma prestado una clave del hermano siguiente
        private void TomaPrestadoDelSiguiente(BNode nodo, int indice)
        {
            BNode hijo = nodo.Childs[indice];
            BNode hermano = nodo.Childs[indice + 1];

            // Añadir la clave del padre al hijo
            hijo.Keys.Add(nodo.Keys[indice]);

            // Si no es hoja, mover también el hijo correspondiente
            if (!hijo.isLeaf)
            {
                hijo.Childs.Add(hermano.Childs[0]);
                hermano.Childs.RemoveAt(0);
            }

            // Actualizar la clave del padre
            nodo.Keys[indice] = hermano.Keys[0];
            hermano.Keys.RemoveAt(0);
        }

        // Fusiona dos nodos hijo
        private void FusionarNodos(BNode nodo, int indice)
        {
            BNode hijo = nodo.Childs[indice];
            BNode hermano = nodo.Childs[indice + 1];

            // Añadir la clave del padre al hijo
            hijo.Keys.Add(nodo.Keys[indice]);

            // Añadir todas las claves del hermano al hijo
            for (int i = 0; i < hermano.Keys.Count; i++)
            {
                hijo.Keys.Add(hermano.Keys[i]);
            }

            // Si no es hoja, mover también los hijos
            if (!hijo.isLeaf)
            {
                for (int i = 0; i < hermano.Childs.Count; i++)
                {
                    hijo.Childs.Add(hermano.Childs[i]);
                }
            }

            // Remover la clave y el hijo del nodo padre
            nodo.Keys.RemoveAt(indice);
            nodo.Childs.RemoveAt(indice + 1);
        }

        // Recorrido InOrden del árbol
        public List<Factura> RecorridoInOrden()
        {
            List<Factura> resultado = new List<Factura>();
            RecorridoInOrdenRecursivo(root, resultado);
            return resultado;
        }

        private void RecorridoInOrdenRecursivo(BNode nodo, List<Factura> resultado)
        {
            if (nodo == null)
                return;

            int i;
            for (i = 0; i < nodo.Keys.Count; i++)
            {
                // Recorrer el hijo izquierdo
                if (!nodo.isLeaf)
                    RecorridoInOrdenRecursivo(nodo.Childs[i], resultado);

                // Agregar la clave actual
                resultado.Add(nodo.Keys[i]);
            }

            // Recorrer el último hijo
            if (!nodo.isLeaf)
                RecorridoInOrdenRecursivo(nodo.Childs[i], resultado);
        }

        // Método para generar el archivo .dot para Graphviz
        public string GraficarGraphviz()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph BTree {");
            sb.AppendLine("    node [shape=record];");

            int contadorNodos = 0;
            GraficarGraphvizRecursivo(root, sb, ref contadorNodos);

            sb.AppendLine("}");
            // Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        private void GraficarGraphvizRecursivo(BNode nodo, StringBuilder sb, ref int contadorNodos)
        {
            if (nodo == null)
                return;

            int nodoActual = contadorNodos++;

            // Construir la etiqueta del nodo
            StringBuilder etiquetaNodo = new StringBuilder();
            etiquetaNodo.Append($"node{nodoActual} [label=\"");

            for (int i = 0; i < nodo.Keys.Count; i++)
            {
                if (i > 0)
                    etiquetaNodo.Append("|");
                etiquetaNodo.Append($"<f{i}> |Id: {nodo.Keys[i].Id}, Total: {nodo.Keys[i].Total}|");
            }

            // Añadir un puerto más para el último hijo
            if (nodo.Keys.Count > 0)
                etiquetaNodo.Append($"<f{nodo.Keys.Count}>");

            etiquetaNodo.Append("\"];");
            sb.AppendLine(etiquetaNodo.ToString());

            // Graficar los hijos y sus conexiones
            if (!nodo.isLeaf)
            {
                for (int i = 0; i <= nodo.Keys.Count; i++)
                {
                    int hijoPosicion = contadorNodos;
                    GraficarGraphvizRecursivo(nodo.Childs[i], sb, ref contadorNodos);
                    sb.AppendLine($"    node{nodoActual}:f{i} -> node{hijoPosicion};");
                }
            }
        }
        public bool GenerateReport()
        {

            // GraficarGraphviz();
            // using var writer = new StringWriter();
            // var context = new CompilationContext(writer, new CompilationOptions());
            // graph.CompileAsync(context);

            var result = GraficarGraphviz();

            // Save it to a file
            File.WriteAllText("../../AutoGest_Pro/Fase3/Reportes/ArbolB-5-Facturas.dot", result);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot");

            startInfo.Arguments = $"-Tpng ../../AutoGest_Pro/Fase3/Reportes/ArbolB-5-Facturas.dot -o ../../AutoGest_Pro/Fase3/Reportes/ArbolB-5-Facturas.png";

            Process.Start(startInfo);
            return true;
        }
    }
}