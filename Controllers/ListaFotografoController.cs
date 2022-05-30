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
    public class ListaFotografoController : Controller
    {
        private readonly string cadenaSql;
        public ListaFotografoController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("cadenaSql");
        }
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult ListaFotografos()
        {
            List<Fotografos> lista = new List<Fotografos>();

            using (var conexion = new SqlConnection(cadenaSql))
            {

                conexion.Open();
                var cmd = new SqlCommand("sp_lista_Fotografos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Fotografos()
                        {
                            dfoIdFotografo = Convert.ToInt32(dr["dfoIdFotografo"]),
                            dfoNombre = dr["dfoNombre"].ToString(),
                            dfoFechaIngreso = dr["dfoFechaIngreso"].ToString()
                        });
                    }
                }
            }


            return Json(new { data = lista });
        }
        // GET: ListaFotografoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListaFotografoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaFotografoController/Create
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

        // GET: ListaFotografoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaFotografoController/Edit/5
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

        // GET: ListaFotografoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaFotografoController/Delete/5
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
