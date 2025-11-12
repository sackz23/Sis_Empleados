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

        // 🟢 LISTAR
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            var departamentos = _context.Departamentos.ToList();
            return View(departamentos);
        }

        // 🟢 DETALLES
        public IActionResult Details(int id)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.Id_Departamento == id);
            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // 🟢 CREAR (GET)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // 🟢 CREAR (POST)
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

        // 🟢 EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // 🟢 EDITAR (POST)
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

        // 🟢 ELIMINAR (GET)
        public IActionResult Delete(int id)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.Id_Departamento == id);
            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // 🟢 ELIMINAR (POST)
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
