namespace Fase1.src.models {
    public class Servicio
    {
        public int ID { get; set; }
        public int Id_Repuesto { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }

        public Servicio(int id, int idRepuesto, int idVehiculo, string detalles, double costo)
        {
            ID = id;
            Id_Repuesto = idRepuesto;
            Id_Vehiculo = idVehiculo;
            Detalles = detalles;
            Costo = costo;
        }
    }
}