using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class PeriodosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeriodosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var periodos = _context.Periodos.ToList();
            return View(periodos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _context.Periodos.Add(periodo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periodo);
        }

        public IActionResult Edit(int id)
        {
            var per = _context.Periodos.Find(id);
            return per == null ? NotFound() : View(per);
        }

        [HttpPost]
        public IActionResult Edit(Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _context.Periodos.Update(periodo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periodo);
        }

        public IActionResult Delete(int id)
        {
            var per = _context.Periodos.Find(id);
            return per == null ? NotFound() : View(per);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var periodo = _context.Periodos.Find(id);

            if (periodo != null)
            {
                _context.Periodos.Remove(periodo);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
