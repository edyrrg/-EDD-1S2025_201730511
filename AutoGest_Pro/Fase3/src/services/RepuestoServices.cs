using System.Net;
using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3.src.services
{
    public class RepuestoService
    {
        public static RepuestoService? _instance;
        private AVLTree _repuestos { get; } = new AVLTree();

        public static RepuestoService Instance
        {
            get
            {
                _instance ??= new RepuestoService();
                return _instance;
            }
        }

        public void InsertRepuesto(Repuestos repuesto)
        {
            if (_repuestos.Search(repuesto.ID))
            {
                var id = repuesto.ID;
                throw new Exception($"El repuesto con id {id} ya existe.");
            }
            _repuestos.Insert(repuesto);
        }

        public Repuestos FindRepuestoByID(int id)
        {
            var AVLNode = _repuestos.Find(id);
            if (AVLNode == null)
            {
                throw new Exception($"El repuesto con id {id} no existe.");
            }
            return AVLNode.Data;
        }

        public void UpdateRepuesto(Repuestos repuesto)
        {
            if (!_repuestos.Update(repuesto))
            {
                var id = repuesto.ID;
                throw new Exception($"El repuesto con id {id} no existe y no se puede actualizar.");
            }
        }

        public void GenerateReport()
        {
            if (!_repuestos.GenerateReport())
            {
                throw new Exception("No hay repuestos para generar el reporte.");
            }
        }

        public List<Repuestos> GetRepuestosInOrder()
        {
            var listResult = _repuestos.InOrder();
            if (listResult.Count == 0)
            {
                throw new Exception("No hay repuestos para mostrar.");
            }
            return listResult;
        }

        public List<Repuestos> GetRepuestosPreOrder()
        {
            var listResult = _repuestos.PreOrder();
            if (listResult.Count == 0)
            {
                throw new Exception("No hay repuestos para mostrar.");
            }
            return listResult;
        }

        public List<Repuestos> GetRepuestosPostOrder()
        {
            var listResult = _repuestos.PostOrder();
            if (listResult.Count == 0)
            {
                throw new Exception("No hay repuestos para mostrar.");
            }
            return listResult;
        }

        public bool SearchRepuestoById(int id)
        {
            return _repuestos.Search(id);
        }

        public void SaveBackup()
        {
            if (!_repuestos.SaveBackup())
            {
                throw new Exception("No se pudo guardar el backup porque no hay datos que guardar.");
            }
        }
    }
}