using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Tipo_Deducciones")]
    public class Tipo_Deducciones
    {
        [Key]
        public int Id_TipoDeducciones { get; set; }

        [Required, MaxLength(50)]
        public string Nombre_Deduccion { get; set; }

        public virtual ICollection<Detalle_Deduccion>? DetallesDeduccion { get; set; }
    }
}
