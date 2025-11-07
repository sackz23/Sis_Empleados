using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdEmpleado { get; set; }

    public int IdRol { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public byte[] Contraseña { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
