using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using Sis_Empleados.Models;
using System.Drawing;


namespace Sis_Empleados.Controllers
{
    public class PlanillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanillaController(ApplicationDbContext context)
        {
            ExcelPackage.License.SetNonCommercialPersonal("<SAC>");
            _context = context;
        }


        private List<PlanillaFila> ObtenerPlanilla(int idPeriodo)
        {
            var empleados = _context.Empleados
                .Include(e => e.CargoEmpleado)
                .Where(e => e.Activo)
                .ToList();

            var salarios = _context.EmpleadoSalarios
                .Where(s => s.Id_Periodo == idPeriodo)
                .ToList();

            var detalles = _context.DetalleDeducciones
                .Include(d => d.TipoDeduccion)
                .ToList();

            var lista = new List<PlanillaFila>();

            foreach (var emp in empleados)
            {
                var sal = salarios.FirstOrDefault(s => s.Id_Empleado == emp.Id_Empleado);

                if (sal == null)
                    continue; // El empleado no tiene salario este periodo

                decimal totalDeducciones = 0;

                foreach (var ded in detalles)
                {
                    totalDeducciones += ded.Deduccion;
                }

                lista.Add(new PlanillaFila()
                {
                    Empleado = emp.Nombre,
                    SalarioBase = sal.Salario_Base,
                    TotalDeducciones = totalDeducciones,
                    SalarioNeto = sal.Salario_Base - totalDeducciones
                });
            }

            return lista;
        }

        public IActionResult ExportarExcel(int idPeriodo)
        {
            var datos = ObtenerPlanilla(idPeriodo);

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Planilla");

            // ENCABEZADOS
            ws.Cells["A1"].Value = "Empleado";
            ws.Cells["B1"].Value = "Salario Base";
            ws.Cells["C1"].Value = "Total Deducciones";
            ws.Cells["D1"].Value = "Salario Neto";

            using (var range = ws.Cells["A1:D1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                range.Style.Font.Color.SetColor(Color.White);
            }

            int row = 2;

            foreach (var fila in datos)
            {
                ws.Cells[row, 1].Value = fila.Empleado;
                ws.Cells[row, 2].Value = fila.SalarioBase;
                ws.Cells[row, 3].Value = fila.TotalDeducciones;
                ws.Cells[row, 4].Value = fila.SalarioNeto;

                row++;
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            var stream = new MemoryStream(package.GetAsByteArray());

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Planilla_{idPeriodo}.xlsx"
            );
        }

        // Mostrar pantalla para seleccionar período
        public IActionResult Index()
        {
            ViewBag.Periodos = _context.Periodos.ToList();
            return View();
        }

        // Generar planilla
        public IActionResult Generar(int idPeriodo)
        {
            ViewBag.IdPeriodo = idPeriodo;

            var empleados = _context.Empleados
                .Include(e => e.CargoEmpleado)
                .Include(e => e.CargoEmpleado.Departamento)
                .Where(e => e.Activo == true)
                .ToList();

            var salarios = _context.EmpleadoSalarios
                .Include(s => s.Periodo)
                .Where(s => s.Id_Periodo == idPeriodo)
                .ToList();

            var detalles = _context.DetalleDeducciones
                .Include(d => d.TipoDeduccion)
                .ToList();

            // Construir la tabla de planilla
            var lista = new List<PlanillaFila>();

            foreach (var emp in empleados)
            {
                var salario = salarios.FirstOrDefault(s => s.Id_Empleado == emp.Id_Empleado);

                if (salario == null)
                    continue;

                decimal totalDeducciones = 0;

                foreach (var det in detalles)
                {
                    var monto = (salario.Salario_Base * det.Deduccion) / 100m;
                    totalDeducciones += monto;
                }

                lista.Add(new PlanillaFila
                {
                    Empleado = emp.Nombre,
                    Departamento = emp.CargoEmpleado.Departamento.Departamento_De_Trabajo,
                    Cargo = emp.CargoEmpleado.Cargo_De_Empleado,
                    SalarioBase = salario.Salario_Base,
                    TotalDeducciones = totalDeducciones,
                    SalarioNeto = salario.Salario_Base - totalDeducciones
                });
            }

            return View(lista);
        }
    }
}
