using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Permisos")]
    public class Permiso
    {
        [Key]
        public int Id_Permiso { get; set; }

        [Required, MaxLength(50)]
        public string Nombre_Permiso { get; set; }

        public virtual ICollection<Rol_Permiso> RolPermisos { get; set; }
    }
}
