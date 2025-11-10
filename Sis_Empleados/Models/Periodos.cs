using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Periodos")]
    public class Periodo
    {
        [Key]
        public int Id_Periodo { get; set; }

        [Required, MaxLength(50)]
        public string Periodo_De_Pago { get; set; }

        public virtual ICollection<Empleado_Salario> EmpleadoSalarios { get; set; }
    }
}
