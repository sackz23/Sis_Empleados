using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string DepartamentoDeTrabajo { get; set; } = null!;

    public virtual ICollection<CargoEmpleado> CargoEmpleados { get; set; } = new List<CargoEmpleado>();
}
