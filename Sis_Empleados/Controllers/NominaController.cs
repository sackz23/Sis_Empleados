using Microsoft.AspNetCore.Mvc;

namespace Sis_Empleados.Controllers
{
    public class NominaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
