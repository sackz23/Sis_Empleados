using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Turno_Empleado")]
    public class Turno_Empleado
    {
        [Key]
        public int Id_TurnoEmpleado { get; set; }

        [Required]
        public int Id_Turno { get; set; }

        [Required]
        public int Id_Empleado { get; set; }

        [ForeignKey("Id_Turno")]
        public virtual Turno Turno { get; set; }

        [ForeignKey("Id_Empleado")]
        public virtual Empleado Empleado { get; set; }
    }
}

