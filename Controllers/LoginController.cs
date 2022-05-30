using Microsoft.AspNetCore.Mvc;

namespace FacturaPF.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
