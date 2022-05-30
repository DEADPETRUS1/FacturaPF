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
using Rotativa;

namespace FacturaPF.Controllers
{
    public class ListaClientesController : Controller
    {
        private readonly string cadenaSql;
        public ListaClientesController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("cadenaSql");
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public JsonResult ListaClientes()
        {
            List<Clientes> lista = new List<Clientes>();

            using (var conexion = new SqlConnection(cadenaSql))
            {

                conexion.Open();
                var cmd = new SqlCommand("sp_lista_Clientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Clientes()
                        {
                            cliIdCliente = Convert.ToInt32(dr["cliIdCliente"]),
                            cliNombre = dr["cliNombre"].ToString(),
                            cliCorreo = dr["cliCorreo"].ToString(),
                            cliDireccion = dr["cliDireccion"].ToString(),
                            cliFechaIngreso = dr["cliFechaIngreso"].ToString()
                        });
                    }
                }
            }


            return Json(new { data = lista });
        }
        

        // GET: ListaClientesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListaClientesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaClientesController/Create
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

        // GET: ListaClientesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaClientesController/Edit/5
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

        // GET: ListaClientesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaClientesController/Delete/5
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
