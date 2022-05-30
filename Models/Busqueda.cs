using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaPF.Models
{
    public class Busqueda
    {
        public int value { get; set; }
        public string label { get; set; }
        public string facNumeroDocumento { get; set; }
        public string facFechaIngreso { get; set; }
        public string facRazonSocial { get; set; }
        public string dfoNombre { get; set; }
        public string cliNombre { get; set; }
        public string cliCorreo { get; set; }
        public string cliDireccion { get; set; }
        public decimal facTotal { get; set; }
    }
}
