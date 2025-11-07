using System;
using System.Collections.Generic;

namespace Sis_Empleados.Models;

public partial class DetalleDeduccion
{
    public int IdDetalleDeduccion { get; set; }

    public int IdTipoDeducciones { get; set; }

    public decimal Deduccion { get; set; }

    public virtual ICollection<Deduccion> Deduccions { get; set; } = new List<Deduccion>();

    public virtual TipoDeduccione IdTipoDeduccionesNavigation { get; set; } = null!;
}
