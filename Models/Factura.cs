using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaPF.Models
{
    public class Factura
    {
        public int facIdFactura { get; set; }
        public string facNumeroDocumento { get; set; }
        public string facRazonSocial { get; set; }
        public decimal facTotal { get; set; }
        public string facFechaIngreso { get; set; }
        public List<Fotografos> lstFotografos { get; set; }
        public List<Clientes> lstClientes { get; set; }
        public List<DetalleFactura> lstDetalleFactura { get; set; }
    }
}
