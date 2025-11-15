using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;
using System.Security.Cryptography;
using System.Text;

namespace Sis_Empleados.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index(string buscar, int pagina = 1, int tamanoPagina = 10)
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            var usuarios = _context.Usuarios
                .Include(u => u.Empleado)
                .Include(u => u.Rol)
                .AsQueryable();

            // Búsqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                usuarios = usuarios.Where(u =>
                    u.Nombre_Usuario.Contains(buscar) ||
                    u.Empleado.Nombre.Contains(buscar) ||
                    u.Rol.Nombre_Rol.Contains(buscar)
                );
            }

            // Total de registros filtrados
            int totalRegistros = usuarios.Count();

            // Paginación
            var usuariosPagina = usuarios
                .OrderBy(u => u.Nombre_Usuario)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            // Enviar datos a la vista
            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

            return View(usuariosPagina);
        }


        // DETALLES
        public IActionResult Details(int id)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Empleado)
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Id_Usuario == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // CREAR (GET)
        public IActionResult Create()
        {
            ViewBag.Empleados = _context.Empleados
                .Where(e => !_context.Usuarios.Any(u => u.Id_Empleado == e.Id_Empleado)) // evitar duplicados
                .ToList();

            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        public IActionResult Create(Usuario usuario, string password)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"❌ Error: {error.ErrorMessage}");
            }

            if (ModelState.IsValid)
            {
                // Encriptar contraseña SHA256
                using (SHA256 sha = SHA256.Create())
                {
                    usuario.Contraseña = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                }

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        // EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Edit(Usuario usuario, string? password)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    using (SHA256 sha = SHA256.Create())
                    {
                        usuario.Contraseña = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                    }
                }
                else
                {
                    // conservar contraseña anterior
                    usuario.Contraseña = _context.Usuarios.AsNoTracking()
                        .First(u => u.Id_Usuario == usuario.Id_Usuario).Contraseña;
                }

                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        // ELIMINAR (GET)
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Empleado)
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Id_Usuario == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // ELIMINAR (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
