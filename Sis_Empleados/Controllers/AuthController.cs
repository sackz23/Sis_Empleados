using Microsoft.AspNetCore.Mvc;
using Sis_Empleados.Models;
using System.Security.Cryptography;
using System.Text;

namespace Sis_Empleados.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar formulario de login
        public IActionResult Login()
        {
            return View();
        }

        // Procesar login
        [HttpPost]
        public IActionResult Login(string nombreUsuario, string password)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Debe ingresar usuario y contraseña.";
                return View();
            }

            // Hashear la contraseña ingresada
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Buscar usuario con el nombre y contraseña hash
                var usuario = _context.Usuarios
                    .FirstOrDefault(u =>
                        u.Nombre_Usuario == nombreUsuario &&
                        u.Contraseña.SequenceEqual(hashedPassword));

                if (usuario != null)
                {
                    // Guardar datos en sesión
                    HttpContext.Session.SetInt32("UsuarioId", usuario.Id_Usuario);
                    HttpContext.Session.SetString("NombreUsuario", usuario.Nombre_Usuario);
                    HttpContext.Session.SetInt32("RolId", usuario.Id_Rol);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
