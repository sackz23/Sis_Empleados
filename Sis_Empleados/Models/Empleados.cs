using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        public int Id_Empleado { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        public int Id_CargoEmpleado { get; set; }

        [ForeignKey("Id_CargoEmpleado")]
        public virtual Cargo_Empleado CargoEmpleado { get; set; }

        [Required]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required, MaxLength(50)]
        public string Telefono { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public DateTime Fecha_Ingreso { get; set; }

        public bool Activo { get; set; }

        // Relaciones
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Turno_Empleado> Turnos { get; set; }
        public virtual ICollection<Empleado_Salario> Salarios { get; set; }
    }
}
