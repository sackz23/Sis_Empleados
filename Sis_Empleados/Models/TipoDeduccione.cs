using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class TipoDeduccione
{
    public int IdTipoDeducciones { get; set; }

    public string NombreDeduccion { get; set; } = null!;

    public virtual ICollection<DetalleDeduccion> DetalleDeduccions { get; set; } = new List<DetalleDeduccion>();
}
