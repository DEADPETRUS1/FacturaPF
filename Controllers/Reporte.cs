using Microsoft.AspNetCore.Http;
using FacturaPF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaPF.Controllers
{
    public class Reporte : Controller
    {
        // GET: HomeController1
        private readonly string cadenaSql;

        public Reporte(IConfiguration configuration)
        {
            cadenaSql = configuration.GetConnectionString("CadenaSQL");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BusquedaProducto(string busqueda)
        {

            List<Busqueda> Lista = new List<Busqueda>();

            using (var conexion = new SqlConnection(cadenaSql))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_busqueda_productos", conexion);
                cmd.Parameters.AddWithValue("busqueda", busqueda);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new Busqueda()
                        {
                            value = Convert.ToInt32(dr["facIdFactura"]),
                            label = dr["Texto"].ToString(),
                            facNumeroDocumento = dr["facNumeroDocumento"].ToString(),
                            facFechaIngreso = dr["facFechaIngreso"].ToString(),
                            facRazonSocial = dr["facRazonSocial"].ToString(),
                            dfoNombre = dr["dfoNombre"].ToString(),
                            cliNombre = dr["cliNombre"].ToString(),
                            cliCorreo = dr["cliCorreo"].ToString(),
                            cliDireccion = dr["cliDireccion"].ToString(),
                            facTotal = Convert.ToDecimal(dr["facTotal"])
                        });
                    }
                }
            }

            return Json(Lista);
        }

        [HttpGet]
        public JsonResult BusquedaProductodf(string busquedadf)
        {

            List<Busquedadf> Lista = new List<Busquedadf>();

            using (var conexion = new SqlConnection(cadenaSql))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_busqueda_productosdf", conexion);
                cmd.Parameters.AddWithValue("busquedadf", busquedadf);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new Busquedadf()
                        {
                            value = Convert.ToInt32(dr["dfaIdDetalleFactura"]),
                            label = dr["Texto"].ToString(),
                            dfaProducto = dr["dfaProducto"].ToString(),
                            dfaCantidad = Convert.ToInt32(dr["dfaCantidad"]),
                            dfaTotal = Convert.ToDecimal(dr["dfaTotal"]),
                       
                        });
                    }
                }
            }

            return Json(Lista);
        }

        public IActionResult Print()
        {
            return new ViewAsPdf("Index", new Busqueda())
            {
            };
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
