using Microsoft.AspNetCore.Http;
using FacturaPF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaPF.Controllers
{
    public class ListaFacturaController : Controller
    {
        private readonly string cadenaSql;
        public ListaFacturaController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("cadenaSql");
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ListaFactura()
        {
            List<Factura> lista = new List<Factura>();

            using (var conexion = new SqlConnection(cadenaSql))
            {

                conexion.Open();
                var cmd = new SqlCommand("sp_lista_Factura", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Factura()
                        {
                            facIdFactura = Convert.ToInt32(dr["facIdFactura"]),
                            facRazonSocial = dr["facRazonSocial"].ToString(),
                            facTotal = Convert.ToInt32(dr["facTotal"]),
                            facFechaIngreso = dr["facFechaIngreso"].ToString()
                        });
                    }
                }
            }


            return Json(new { data = lista });
        }

        // GET: ListaFacturaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListaFacturaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaFacturaController/Create
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

        // GET: ListaFacturaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaFacturaController/Edit/5
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

        // GET: ListaFacturaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaFacturaController/Delete/5
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
