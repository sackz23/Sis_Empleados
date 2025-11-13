using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Required]
        public int Id_Empleado { get; set; }

        [Required]
        public int Id_Rol { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre_Usuario { get; set; }

        [Required]
        [Column(TypeName = "varbinary(64)")]
        public byte[] Contraseña { get; set; } = Array.Empty<byte>();

        [ForeignKey("Id_Empleado")]
        public virtual Empleado? Empleado { get; set; }

        [ForeignKey("Id_Rol")]
        public virtual Rol? Rol { get; set; }
    }
}
