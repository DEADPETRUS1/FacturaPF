using FacturaPF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using Rotativa;
using Rotativa.AspNetCore;

namespace FacturaPF.Controllers
{
    public class HomeController : Controller
    {
        private readonly string cadenaSql;
        public HomeController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("CadenaSQL");
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]

        public JsonResult GuardarFactura([FromBody] Factura body)
        {

            XElement Factura = new XElement("Factura",
                new XElement("facNumeroDocumento", body.facNumeroDocumento),
                new XElement("facRazonSocial", body.facRazonSocial),
                new XElement("facTotal", body.facTotal),
                new XElement("facIdFactura", body.facIdFactura),
                new XElement("facFechaIngreso", body.facFechaIngreso)
            );



            XElement oDetalleFactura = new XElement("DetalleFactura");
            foreach (DetalleFactura item in body.lstDetalleFactura)
            {
                oDetalleFactura.Add(new XElement("Item",
                    new XElement("dfaProducto", item.dfaProducto),
                    new XElement("dfaPrecio", item.dfaPrecio),
                    new XElement("dfaCantidad", item.dfaCantidad),
                    new XElement("dfaTotal", item.dfaTotal),
                    new XElement("dfaFechaIngreso", item.dfaFechaIngreso)
                    ));
            }
            Factura.Add(oDetalleFactura);


            using (SqlConnection oconexion = new SqlConnection(cadenaSql))
            {
                oconexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarFactura", oconexion);
                cmd.Parameters.Add("@Factura_xml", SqlDbType.Xml).Value = Factura.ToString();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }

            return Json(new { respuesta = true });
        }


        [HttpPost]
        public JsonResult GuardarClientes([FromBody] Clientes body)
        {

            XElement Clientes = new XElement("Clientes",
                new XElement("cliNombre", body.cliNombre),
                new XElement("cliCorreo", body.cliCorreo),
                new XElement("cliDireccion", body.cliDireccion),
                new XElement("cliFechaIngreso", body.cliFechaIngreso)
            );

            using (SqlConnection oconexion = new SqlConnection(cadenaSql))
            {
                oconexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarClientes", oconexion);
                cmd.Parameters.Add("@Clientes_xml", SqlDbType.Xml).Value = Clientes.ToString();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
            return Json(new { respuesta = true });

        }

        [HttpPost]
        public JsonResult GuardarFotografos([FromBody] Fotografos body)
        {

            XElement Fotografos = new XElement("Fotografos",
                new XElement("dfoNombre", body.dfoNombre),
                new XElement("dfoFechaIngreso", body.dfoFechaIngreso)
            );

            using (SqlConnection oconexion = new SqlConnection(cadenaSql))
            {
                oconexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarFotografos", oconexion);
                cmd.Parameters.Add("@Fotografos_xml", SqlDbType.Xml).Value = Fotografos.ToString();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
            return Json(new { respuesta = true });

        }
        public IActionResult Privacy()
        {
            return View();
        }


        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
