using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Rol_Permiso")]
    public class Rol_Permiso
    {
        [Key]
        public int Id_RolPermiso { get; set; }

        [Required]
        public int Id_Rol { get; set; }

        [Required]
        public int Id_Permiso { get; set; }

        [ForeignKey("Id_Rol")]
        public virtual Rol Rol { get; set; }

        [ForeignKey("Id_Permiso")]
        public virtual Permiso Permiso { get; set; }
    }
}
