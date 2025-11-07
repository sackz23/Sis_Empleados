using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string TipoTurno { get; set; } = null!;

    public virtual ICollection<TurnoEmpleado> TurnoEmpleados { get; set; } = new List<TurnoEmpleado>();
}
