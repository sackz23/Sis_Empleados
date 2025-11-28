using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class RolesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index(string buscar, int pagina = 1, int tamanoPagina = 10)
        {
            // Si no hay sesión activa, redirige al login
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var roles = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                roles = roles.Where(r => r.Nombre_Rol.Contains(buscar));
            }

            int totalRegistros = roles.Count();

            var rolesPagina = roles
                .OrderBy(r => r.Nombre_Rol)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

            return View(rolesPagina);
        }

        // CREAR (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Add(rol);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rol);
        }


        // EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null) return NotFound();
            return View(rol);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Update(rol);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        // ELIMINAR (GET)
        public IActionResult Delete(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null) return NotFound();
            return View(rol);
        }

        // ELIMINAR (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null) return NotFound();

            _context.Roles.Remove(rol);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
