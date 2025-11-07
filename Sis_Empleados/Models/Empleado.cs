using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdCargoEmpleado { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<EmpleadoSalario> EmpleadoSalarios { get; set; } = new List<EmpleadoSalario>();

    public virtual CargoEmpleado IdCargoEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<TurnoEmpleado> TurnoEmpleados { get; set; } = new List<TurnoEmpleado>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
