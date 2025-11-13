using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Cargo_Empleado")]
    public class Cargo_Empleado
    {
        [Key]
        public int Id_CargoEmpleado { get; set; }

        [Required]
        public int Id_Departamento { get; set; }

        [Required, MaxLength(50)]
        public string Cargo_De_Empleado { get; set; }

        [ForeignKey("Id_Departamento")]
        public virtual Departamento? Departamento { get; set; }

        public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}

