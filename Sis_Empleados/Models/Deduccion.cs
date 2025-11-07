using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class Deduccion
{
    public int IdTotalDeducciones { get; set; }

    public int IdDetalleDeduccion { get; set; }

    public int IdEmpleadoSalario { get; set; }

    public int MontoDeduccion { get; set; }

    public virtual DetalleDeduccion IdDetalleDeduccionNavigation { get; set; } = null!;

    public virtual EmpleadoSalario IdEmpleadoSalarioNavigation { get; set; } = null!;
}
