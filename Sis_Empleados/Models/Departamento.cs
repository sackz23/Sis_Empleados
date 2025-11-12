using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        public int Id_Departamento { get; set; }

        [Required, MaxLength(50)]
        public string Departamento_De_Trabajo { get; set; }

        // Relaciones
        public virtual ICollection<Cargo_Empleado>? Cargos { get; set; } = new List<Cargo_Empleado>();
    }
}
