using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Empleado_Salario")]
    public class Empleado_Salario
    {
        [Key]
        public int Id_EmpleadoSalario { get; set; }

        [Required]
        public int Id_Empleado { get; set; }

        [Required]
        public int Id_Periodo { get; set; }

        [Required]
        public int Salario_Base { get; set; }

        [ForeignKey("Id_Empleado")]
        public virtual Empleado Empleado { get; set; }

        [ForeignKey("Id_Periodo")]
        public virtual Periodo Periodo { get; set; }

        public virtual ICollection<Deduccion> Deducciones { get; set; }
    }
}
