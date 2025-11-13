using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;

namespace Sis_Empleados.Controllers
{
    public class TipoDeduccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoDeduccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index()
        {
            var tipos = _context.TipoDeducciones.ToList();
            return View(tipos);
        }

        // CREAR GET
        public IActionResult Create()
        {
            return View();
        }

        // CREAR POST
        [HttpPost]
        public IActionResult Create(Tipo_Deducciones modelo)
        {
            if (ModelState.IsValid)
            {
                _context.TipoDeducciones.Add(modelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelo);
        }

        // EDITAR GET
        public IActionResult Edit(int id)
        {
            var tipo = _context.TipoDeducciones.Find(id);
            return tipo == null ? NotFound() : View(tipo);
        }

        // EDITAR POST
        [HttpPost]
        public IActionResult Edit(Tipo_Deducciones modelo)
        {
            if (ModelState.IsValid)
            {
                _context.TipoDeducciones.Update(modelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        // ELIMINAR GET
        public IActionResult Delete(int id)
        {
            var tipo = _context.TipoDeducciones.Find(id);
            return tipo == null ? NotFound() : View(tipo);
        }

        // ELIMINAR POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var tipo = _context.TipoDeducciones.Find(id);

            if (tipo != null)
            {
                _context.TipoDeducciones.Remove(tipo);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

