using Microsoft.EntityFrameworkCore;

namespace Sis_Empleados.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tablas 
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Cargo_Empleado> CargosEmpleados { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Turno_Empleado> TurnosEmpleados { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Rol_Permiso> RolPermisos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Empleado_Salario> EmpleadoSalarios { get; set; }
        public DbSet<Tipo_Deducciones> TipoDeducciones { get; set; }
        public DbSet<Detalle_Deduccion> DetalleDeducciones { get; set; }
        public DbSet<Deduccion> Deducciones { get; set; }
    }





}
