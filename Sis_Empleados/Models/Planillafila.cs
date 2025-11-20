using System;

namespace Sis_Empleados.Models
{
    public class PlanillaFila
    {
        public string Empleado { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public decimal SalarioBase { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal SalarioNeto { get; set; }

    }
}
