using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Si no hay sesión activa, redirige al login
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios
                .Where(u => u.Id_Usuario == usuarioId)
                .Select(u => new { u.Nombre_Usuario, RolNombre = u.Rol.Nombre_Rol })
                .FirstOrDefault();

            if (usuario != null)
            {
                ViewBag.Usuario = usuario.Nombre_Usuario;
                ViewBag.Rol = usuario.RolNombre;
            }

            // Si ya está logueado, muestra la página principal
            return View();
        }

        public IActionResult Privacy()
        {
            // Verifica si hay sesión activa
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Obtiene los datos de sesión
            ViewBag.Usuario = HttpContext.Session.GetString("NombreUsuario");
            ViewBag.RolId = HttpContext.Session.GetInt32("RolId");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
