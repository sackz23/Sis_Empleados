using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Deduccion")]
    public class Deduccion
    {
        [Key]
        public int Id_TotalDeducciones { get; set; }

        [Required]
        public int Id_DetalleDeduccion { get; set; }

        [Required]
        public int Id_EmpleadoSalario { get; set; }

        [Required]
        public int Monto_Deduccion { get; set; }

        [ForeignKey("Id_DetalleDeduccion")]
        public virtual Detalle_Deduccion DetalleDeduccion { get; set; }

        [ForeignKey("Id_EmpleadoSalario")]
        public virtual Empleado_Salario EmpleadoSalario { get; set; }
    }
}
