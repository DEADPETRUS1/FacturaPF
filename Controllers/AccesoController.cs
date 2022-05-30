using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using FacturaPF.Models;

namespace FacturaPF.Controllers
{
    public class AccesoController : Controller
    {

        private readonly string cadenaSql;
        public AccesoController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("CadenaSQL");
        }
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if (oUsuario.usuClave == oUsuario.ConfirmarClave)
            {


                oUsuario.usuClave = (oUsuario.usuClave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadenaSql))
            {

                

                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("usuCorreo", oUsuario.usuCorreo);
                string clave = oUsuario.usuClave + ".-,";//123.,
                MD5 md5 = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(clave);
                byte[] hash = md5.ComputeHash(inputBytes);
                string claveMd5 = BitConverter.ToString(hash).Replace("-", "");
                cmd.Parameters.AddWithValue("usuClave", claveMd5);
                cmd.Parameters.Add("usuRegistrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("usuMensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["usuRegistrado"].Value);
                mensaje = cmd.Parameters["usuMensaje"].Value.ToString();


            }
            
            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Registrar", "Acceso");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
    

            using (SqlConnection cn = new SqlConnection(cadenaSql))
            {

                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("usuCorreo", oUsuario.usuCorreo);

                string clave = oUsuario.usuClave + ".-,";//123.,
                MD5 md5 = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(clave);
                byte[] hash = md5.ComputeHash(inputBytes);
                string claveMd5 = BitConverter.ToString(hash).Replace("-", "");
                cmd.Parameters.AddWithValue("usuClave", claveMd5);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.usuId = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (oUsuario.usuId != 0)
            {
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }


        }


            
        
    }
}
    
