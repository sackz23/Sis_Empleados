using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class EmpleadoSalarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoSalarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR + BÚSQUEDA + PAGINACIÓN
        public IActionResult Index(string buscar, int pagina = 1, int tamanoPagina = 10)
        {
            var query = _context.EmpleadoSalarios
                .Include(s => s.Empleado)
                .Include(s => s.Periodo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(s =>
                    s.Empleado.Nombre.Contains(buscar) ||
                    s.Periodo.Periodo_De_Pago.Contains(buscar)
                );
            }

            int totalRegistros = query.Count();

            var lista = query
                .OrderBy(s => s.Periodo.Periodo_De_Pago)
                .ThenBy(s => s.Empleado.Nombre)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

            return View(lista);
        }

        // CREAR GET
        public IActionResult Create()
        {
            ViewBag.Empleados = _context.Empleados.Where(e => e.Activo).ToList();
            ViewBag.Periodos = _context.Periodos.ToList();
            return View();
        }

        // CREAR POST
        [HttpPost]
        public IActionResult Create(Empleado_Salario modelo)
        {
            if (ModelState.IsValid)
            {
                _context.EmpleadoSalarios.Add(modelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.Periodos = _context.Periodos.ToList();
            return View(modelo);
        }

        // EDITAR GET
        public IActionResult Edit(int id)
        {
            var item = _context.EmpleadoSalarios.Find(id);
            if (item == null)
                return NotFound();

            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.Periodos = _context.Periodos.ToList();
            return View(item);
        }

        // EDITAR POST
        [HttpPost]
        public IActionResult Edit(Empleado_Salario modelo)
        {
            if (ModelState.IsValid)
            {
                _context.EmpleadoSalarios.Update(modelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.Periodos = _context.Periodos.ToList();
            return View(modelo);
        }

        // ELIMINAR GET
        public IActionResult Delete(int id)
        {
            var item = _context.EmpleadoSalarios
                .Include(s => s.Empleado)
                .Include(s => s.Periodo)
                .FirstOrDefault(s => s.Id_EmpleadoSalario == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // ELIMINAR POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _context.EmpleadoSalarios.Find(id);
            if (item != null)
            {
                _context.EmpleadoSalarios.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
