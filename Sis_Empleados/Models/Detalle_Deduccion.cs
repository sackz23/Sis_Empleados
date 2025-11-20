using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis_Empleados.Models
{
    [Table("Detalle_Deduccion")]
    public class Detalle_Deduccion
    {
        [Key]
        public int Id_DetalleDeduccion { get; set; }

        [Required]
        public int Id_TipoDeducciones { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Deduccion { get; set; }

        [ForeignKey("Id_TipoDeducciones")]
        public virtual Tipo_Deducciones? TipoDeduccion { get; set; }

        public virtual ICollection<Deduccion>? Deducciones { get; set; }
    }
}
