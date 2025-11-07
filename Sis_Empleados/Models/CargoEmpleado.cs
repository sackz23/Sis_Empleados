using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class CargoEmpleado
{
    public int IdCargoEmpleado { get; set; }

    public int IdDepartamento { get; set; }

    public string CargoDeEmpleado { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
