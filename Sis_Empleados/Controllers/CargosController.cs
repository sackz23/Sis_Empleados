using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class CargosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CargosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🟢 LISTAR
        public IActionResult Index(string buscar, int pagina = 1, int tamanoPagina = 10)
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            var cargos = _context.CargosEmpleados
                .Include(c => c.Departamento)
                .AsQueryable();

            // 🔍 FILTRO DE BÚSQUEDA
            if (!string.IsNullOrEmpty(buscar))
            {
                cargos = cargos.Where(c =>
                    c.Cargo_De_Empleado.Contains(buscar) ||
                    c.Departamento.Departamento_De_Trabajo.Contains(buscar)
                );
            }

            // 📌 TOTAL REGISTROS
            int totalRegistros = cargos.Count();

            // ⏭ PAGINACIÓN
            var cargosPagina = cargos
                .OrderBy(c => c.Cargo_De_Empleado)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            // 📦 VARIABLES A LA VISTA
            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

            return View(cargosPagina);
        }

        // 🟢 DETALLES
        public IActionResult Details(int id)
        {
            var cargo = _context.CargosEmpleados
                .Include(c => c.Departamento)
                .FirstOrDefault(c => c.Id_CargoEmpleado == id);

            if (cargo == null)
                return NotFound();

            return View(cargo);
        }

        // 🟢 CREAR (GET)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.Departamentos = _context.Departamentos.ToList();
            return View();
        }

        // 🟢 CREAR (POST)
        [HttpPost]
        public IActionResult Create(Cargo_Empleado cargo)
        {
            if (ModelState.IsValid)
            {
                _context.CargosEmpleados.Add(cargo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departamentos = _context.Departamentos.ToList();
            return View(cargo);
        }

        // 🟢 EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var cargo = _context.CargosEmpleados.Find(id);
            if (cargo == null)
                return NotFound();

            ViewBag.Departamentos = _context.Departamentos.ToList();
            return View(cargo);
        }

        // 🟢 EDITAR (POST)
        [HttpPost]
        public IActionResult Edit(Cargo_Empleado cargo)
        {
            if (ModelState.IsValid)
            {
                _context.CargosEmpleados.Update(cargo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departamentos = _context.Departamentos.ToList();
            return View(cargo);
        }

        // 🟢 ELIMINAR (GET)
        public IActionResult Delete(int id)
        {
            var cargo = _context.CargosEmpleados
                .Include(c => c.Departamento)
                .FirstOrDefault(c => c.Id_CargoEmpleado == id);

            if (cargo == null)
                return NotFound();

            return View(cargo);
        }

        // 🟢 ELIMINAR (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cargo = _context.CargosEmpleados.Find(id);
            if (cargo != null)
            {
                _context.CargosEmpleados.Remove(cargo);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

