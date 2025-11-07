using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class EmpleadoSalario
{
    public int IdEmpleadoSalario { get; set; }

    public int IdEmpleado { get; set; }

    public int IdPeriodo { get; set; }

    public int SalarioBase { get; set; }

    public virtual ICollection<Deduccion> Deduccions { get; set; } = new List<Deduccion>();

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Periodo IdPeriodoNavigation { get; set; } = null!;
}
