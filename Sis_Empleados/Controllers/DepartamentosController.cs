using Microsoft.AspNetCore.Mvc;
using Sis_Empleados.Models;
using Microsoft.EntityFrameworkCore;

namespace Sis_Empleados.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index(string buscar, int pagina = 1, int tamanoPagina = 10)
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            var departamentos = _context.Departamentos.AsQueryable();

            // Filtro por nombre
            if (!string.IsNullOrEmpty(buscar))
            {
                departamentos = departamentos.Where(d => d.Departamento_De_Trabajo.Contains(buscar));
            }

            //  Total de registros
            int totalRegistros = departamentos.Count();

            //  Paginación
            var departamentosPagina = departamentos
                .OrderBy(d => d.Departamento_De_Trabajo)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            //  Datos para la vista
            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.TamanoPagina = tamanoPagina;

            return View(departamentosPagina);
        }

        //  DETALLES
        public IActionResult Details(int id)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.Id_Departamento == id);
            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // CREAR (GET)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // CREAR (POST)
        [HttpPost]
        public IActionResult Create(Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"❌ Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Departamentos.Add(departamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departamento);
        }

        // EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Edit(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Departamentos.Update(departamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departamento);
        }

        // ELIMINAR (GET)
        public IActionResult Delete(int id)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.Id_Departamento == id);
            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // ELIMINAR (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
