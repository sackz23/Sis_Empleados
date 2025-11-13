using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class DetalleDeduccionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleDeduccionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index()
        {
            var detalles = _context.DetalleDeducciones
                .Include(d => d.TipoDeduccion)
                .ToList();

            return View(detalles);
        }

        // CREAR GET
        public IActionResult Create()
        {
            ViewBag.Tipos = _context.TipoDeducciones.ToList();
            return View();
        }

        // CREAR POST
        [HttpPost]
        public IActionResult Create(Detalle_Deduccion modelo)
        {
            if (ModelState.IsValid)
            {
                _context.DetalleDeducciones.Add(modelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipos = _context.TipoDeducciones.ToList();
            return View(modelo);
        }

        // EDITAR GET
        public IActionResult Edit(int id)
        {
            var detalle = _context.DetalleDeducciones.Find(id);
            if (detalle == null)
                return NotFound();

            ViewBag.Tipos = _context.TipoDeducciones.ToList();
            return View(detalle);
        }

        // EDITAR POST
        [HttpPost]
        public IActionResult Edit(Detalle_Deduccion modelo)
        {
            if (ModelState.IsValid)
            {
                _context.DetalleDeducciones.Update(modelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipos = _context.TipoDeducciones.ToList();
            return View(modelo);
        }

        // ELIMINAR GET
        public IActionResult Delete(int id)
        {
            var detalle = _context.DetalleDeducciones
                .Include(d => d.TipoDeduccion)
                .FirstOrDefault(d => d.Id_DetalleDeduccion == id);

            return detalle == null ? NotFound() : View(detalle);
        }

        // ELIMINAR POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var detalle = _context.DetalleDeducciones.Find(id);

            if (detalle != null)
            {
                _context.DetalleDeducciones.Remove(detalle);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
