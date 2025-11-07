using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class Periodo
{
    public int IdPeriodo { get; set; }

    public string PeriodoDePago { get; set; } = null!;

    public virtual ICollection<EmpleadoSalario> EmpleadoSalarios { get; set; } = new List<EmpleadoSalario>();
}
