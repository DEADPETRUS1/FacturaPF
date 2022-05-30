namespace FacturaPF.Models
{
    public class Usuario
    {
        public int usuId { get; set; }
        public string usuCorreo { get; set; }
        public string usuClave { get; set; }

        public string ConfirmarClave { get; set; }
    }
}
