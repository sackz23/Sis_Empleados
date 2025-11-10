using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int Id_Rol { get; set; }

        [Required, MaxLength(50)]
        public string Nombre_Rol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Rol_Permiso> RolPermisos { get; set; }
    }
}
