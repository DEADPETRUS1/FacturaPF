using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaPF.Models
{
    public class DetalleFactura
    {
        public int dfaIdDetalleFactura { get; set; }
        public string dfaProducto { get; set; }
        public decimal dfaPrecio { get; set; }
        public int dfaCantidad { get; set; }
        public decimal dfaTotal { get; set; }
        public string dfaFechaIngreso { get; set; }
    }
}
