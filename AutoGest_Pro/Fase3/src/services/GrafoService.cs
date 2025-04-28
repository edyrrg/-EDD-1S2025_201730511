using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3.src.services
{
    public class GrafoService
    {
        public static GrafoService? _instance;
        private Graph _graph { get; } = new Graph();
        private GrafoService() { }
        public static GrafoService Instance
        {
            get
            {
                _instance ??= new GrafoService();
                return _instance;
            }
        }

        public void InsertarRepuesto(Vehiculo vehiculo, Repuestos repuesto)
        {
            _graph.AddNode(vehiculo, repuesto);
        }

        public void GenerarGrafo()
        {
            Console.WriteLine(_graph.GenerateGraphvizDot());
        }

        public void GenerateReport()
        {
            if (!_graph.GenerateReport())
            {
                throw new Exception("No hay datos para generar el reporte.");
            }
        }
    }
}