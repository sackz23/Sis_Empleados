using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class TurnoEmpleado
{
    public int IdTurnoEmpleado { get; set; }

    public int IdTurno { get; set; }

    public int IdEmpleado { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Turno IdTurnoNavigation { get; set; } = null!;
}
