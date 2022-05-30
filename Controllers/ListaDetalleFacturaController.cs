using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FacturaPF.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace FacturaPF.Controllers
{
    public class ListaDetalleFacturaController : Controller
    {
        private readonly string cadenaSql;
        public ListaDetalleFacturaController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("cadenaSql");
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ListaDetalleFactura()
        {
            List<DetalleFactura> lista = new List<DetalleFactura>();

            using (var conexion = new SqlConnection(cadenaSql))
            {

                conexion.Open();
                var cmd = new SqlCommand("sp_lista_DetalleFactura", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new DetalleFactura()
                        {
                            dfaIdDetalleFactura = Convert.ToInt32(dr["dfaIdDetalleFactura"]),
                            dfaProducto = dr["dfaProducto"].ToString(),
                            dfaPrecio = Convert.ToInt32(dr["dfaPrecio"]),
                            dfaTotal = Convert.ToInt32(dr["dfaTotal"]),
                            dfaFechaIngreso = dr["dfaFechaIngreso"].ToString()
                        });
                    }
                }
            }


            return Json(new { data = lista });
        }

        // GET: ListaDetalleFacturaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListaDetalleFacturaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaDetalleFacturaController/Create
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

        // GET: ListaDetalleFacturaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaDetalleFacturaController/Edit/5
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

        // GET: ListaDetalleFacturaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaDetalleFacturaController/Delete/5
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
