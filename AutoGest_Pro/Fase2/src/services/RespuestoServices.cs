using System.Net;
using Fase2.src.adts;
using Fase2.src.models;

namespace Fase2.src.services
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
                throw new WebException($"El repuesto con id {id} ya existe.");
            }
            _repuestos.Insert(repuesto);
        }

        public void UpdateRepuesto(Repuestos repuesto)
        {
            if (!_repuestos.Update(repuesto))
            {
                var id = repuesto.ID;
                throw new WebException($"El repuesto con id {id} no existe y no se puede actualizar.");
            }
        }
    }
}