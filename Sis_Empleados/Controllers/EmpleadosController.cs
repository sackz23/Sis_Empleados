using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;
using Sis_Empleados.Controllers; 

namespace Sis_Empleados.Controllers
{
    public class EmpleadosController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        // LISTAR EMPLEADOS
        public IActionResult Index(string buscar, int? idDepartamento, int pagina = 1, int tamanoPagina = 10)
        {

            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.Departamentos = _context.Departamentos.ToList();

            var empleados = _context.Empleados
                .Include(e => e.CargoEmpleado)
                .ThenInclude(c => c.Departamento)
                .AsQueryable();

            // Búsqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                empleados = empleados.Where(e =>
                    e.Nombre.Contains(buscar) ||
                    e.Email.Contains(buscar) ||
                    e.CargoEmpleado.Cargo_De_Empleado.Contains(buscar));
            }

            // Filtro por departamento
            if (idDepartamento.HasValue)
            {
                empleados = empleados.Where(e => e.CargoEmpleado.Departamento.Id_Departamento == idDepartamento);
            }

            // Total de registros
            int totalRegistros = empleados.Count();

            // Paginación
            var empleadosPagina = empleados
                .OrderBy(e => e.Nombre)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            // Pasar datos a la vista
            ViewBag.PaginaActual = pagina;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            ViewBag.Buscar = buscar;
            ViewBag.IdDepartamento = idDepartamento;
            return View(empleadosPagina);
        }


        // DETALLES
        public IActionResult Details(int id)
        {
            var empleado = _context.Empleados
                .Include(e => e.CargoEmpleado)
                .ThenInclude(c => c.Departamento)
                .FirstOrDefault(e => e.Id_Empleado == id);

            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        // CREAR (GET)
        public IActionResult Create()
        { 
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.Cargos = _context.CargosEmpleados
                .Include(c => c.Departamento)
                .Select(c => new
                {
                    c.Id_CargoEmpleado,
                    NombreCompleto = c.Cargo_De_Empleado + " - " + c.Departamento.Departamento_De_Trabajo
                })
                .ToList();

            return View();
        }

        // CREAR (POST)
        [HttpPost]
        public IActionResult Create(Empleado empleado)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"❌ Error: {error.ErrorMessage}");
            }

            if (ModelState.IsValid)
            {
                _context.Empleados.Add(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("❌ ModelState inválido");
            }

            ViewBag.Cargos = _context.CargosEmpleados
                .Include(c => c.Departamento)
                .Select(c => new
                {
                    c.Id_CargoEmpleado,
                    NombreCompleto = c.Cargo_De_Empleado + " - " + c.Departamento.Departamento_De_Trabajo
                })
                .ToList();

            return View(empleado);
        }



        // EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
                return NotFound();

            ViewBag.Cargos = _context.CargosEmpleados.ToList();
            return View(empleado);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Edit(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Empleados.Update(empleado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // ELIMINAR (GET)
        public IActionResult Delete(int id)
        {
            var empleado = _context.Empleados
                .Include(e => e.CargoEmpleado)
                .ThenInclude(c => c.Departamento)
                .FirstOrDefault(e => e.Id_Empleado == id);

            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        // ELIMINAR (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
