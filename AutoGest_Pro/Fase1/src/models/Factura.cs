namespace Fase1.src.models
{
    public class Factura
    {
        public int ID { get; set; }
        public int ID_Orden { get; set; }
        public double Total { get; set; }

        public Factura(int id, int idOrden, double total)
        {
            ID = id;
            ID_Orden = idOrden;
            Total = total;
        }
    }
}