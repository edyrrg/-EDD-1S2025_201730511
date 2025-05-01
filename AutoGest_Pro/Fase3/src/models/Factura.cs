using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace Fase3.src.models
{
    public enum MetodoPago
    {
        Efectivo,
        Tarjeta,
        Transferencia,
        Pendiente
    }
    public class Factura(int Id, int IdServicio, float total, MetodoPago metodoPago = MetodoPago.Pendiente)
    {
        public int Id { get; set; } = Id;
        public int IdServicio { get; set; } = IdServicio;
        public float Total { get; set; } = total;
        public string? Fecha { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public MetodoPago MetodoPago { get; set; } = metodoPago;

        public string GetHash()
        {
            string json = JsonSerializer.Serialize(this);

            byte[] jsonByte = Encoding.UTF8.GetBytes(json);

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(jsonByte);
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

        }
    }
}