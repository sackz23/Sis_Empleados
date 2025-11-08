using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sis_Empleados.Models;

public partial class SistemaEmpleadosContext : DbContext
{
    public SistemaEmpleadosContext()
    {
    }

    public SistemaEmpleadosContext(DbContextOptions<SistemaEmpleadosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CargoEmpleado> CargoEmpleados { get; set; }

    public virtual DbSet<Deduccion> Deduccions { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleDeduccion> DetalleDeduccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoSalario> EmpleadoSalarios { get; set; }

    public virtual DbSet<Periodo> Periodos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<RolPermiso> RolPermisos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoDeduccione> TipoDeducciones { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<TurnoEmpleado> TurnoEmpleados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CargoEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdCargoEmpleado).HasName("PK__Cargo_Em__FB269836DDED7532");

            entity.ToTable("Cargo_Empleado");

            entity.Property(e => e.IdCargoEmpleado).HasColumnName("Id_CargoEmpleado");
            entity.Property(e => e.CargoDeEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cargo_De_Empleado");
            entity.Property(e => e.IdDepartamento).HasColumnName("Id_Departamento");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.CargoEmpleados)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CargoEmpleado_Departamento");
        });

        modelBuilder.Entity<Deduccion>(entity =>
        {
            entity.HasKey(e => e.IdTotalDeducciones).HasName("PK__Deduccio__646DB4BCDE0DBC35");

            entity.ToTable("Deduccion");

            entity.Property(e => e.IdTotalDeducciones).HasColumnName("Id_TotalDeducciones");
            entity.Property(e => e.IdDetalleDeduccion).HasColumnName("Id_DetalleDeduccion");
            entity.Property(e => e.IdEmpleadoSalario).HasColumnName("Id_EmpleadoSalario");
            entity.Property(e => e.MontoDeduccion).HasColumnName("Monto_Deduccion");

            entity.HasOne(d => d.IdDetalleDeduccionNavigation).WithMany(p => p.Deduccions)
                .HasForeignKey(d => d.IdDetalleDeduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deduccion_DetalleDeduccion");

            entity.HasOne(d => d.IdEmpleadoSalarioNavigation).WithMany(p => p.Deduccions)
                .HasForeignKey(d => d.IdEmpleadoSalario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deduccion_EmpleadoSalario");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__D9F8A911D5B11B68");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("Id_Departamento");
            entity.Property(e => e.DepartamentoDeTrabajo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Departamento_De_Trabajo");
        });

        modelBuilder.Entity<DetalleDeduccion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDeduccion).HasName("PK__Detalle___718F4C8197ACBDCE");

            entity.ToTable("Detalle_Deduccion");

            entity.Property(e => e.IdDetalleDeduccion).HasColumnName("Id_DetalleDeduccion");
            entity.Property(e => e.Deduccion).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.IdTipoDeducciones).HasColumnName("Id_TipoDeducciones");

            entity.HasOne(d => d.IdTipoDeduccionesNavigation).WithMany(p => p.DetalleDeduccions)
                .HasForeignKey(d => d.IdTipoDeducciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDeduccion_TipoDeducciones");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__7405622338CC8AB4");

            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnName("Fecha_Ingreso");
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.IdCargoEmpleado).HasColumnName("Id_CargoEmpleado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCargoEmpleadoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargoEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Cargo_Empleado");
        });

        modelBuilder.Entity<EmpleadoSalario>(entity =>
        {
            entity.HasKey(e => e.IdEmpleadoSalario).HasName("PK__Empleado__B725DE391BED4E0D");

            entity.ToTable("Empleado_Salario");

            entity.Property(e => e.IdEmpleadoSalario).HasColumnName("Id_EmpleadoSalario");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.IdPeriodo).HasColumnName("Id_Periodo");
            entity.Property(e => e.SalarioBase).HasColumnName("salario_base");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.EmpleadoSalarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpleadoSalario_Empleado");

            entity.HasOne(d => d.IdPeriodoNavigation).WithMany(p => p.EmpleadoSalarios)
                .HasForeignKey(d => d.IdPeriodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpleadoSalario_Periodo");
        });

        modelBuilder.Entity<Periodo>(entity =>
        {
            entity.HasKey(e => e.IdPeriodo).HasName("PK__Periodos__48658097BEF2CC60");

            entity.Property(e => e.IdPeriodo).HasColumnName("Id_Periodo");
            entity.Property(e => e.PeriodoDePago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Periodo_De_Pago");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__153CFB6D4C8FD95C");

            entity.Property(e => e.IdPermiso).HasColumnName("Id_Permiso");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Permiso");
        });

        modelBuilder.Entity<RolPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolPermiso).HasName("PK__Rol_Perm__A875E0F2B082BE03");

            entity.ToTable("Rol_Permiso");

            entity.Property(e => e.IdRolPermiso).HasColumnName("Id_RolPermiso");
            entity.Property(e => e.IdPermiso).HasColumnName("Id_Permiso");
            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolPermiso_Permiso");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolPermiso_Rol");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__55932E86D37CE2D7");

            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Rol");
        });

        modelBuilder.Entity<TipoDeduccione>(entity =>
        {
            entity.HasKey(e => e.IdTipoDeducciones).HasName("PK__Tipo_Ded__0144A3447C65403F");

            entity.ToTable("Tipo_Deducciones");

            entity.Property(e => e.IdTipoDeducciones).HasColumnName("Id_TipoDeducciones");
            entity.Property(e => e.NombreDeduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Deduccion");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__Turno__5CF9003FD514A914");

            entity.ToTable("Turno");

            entity.Property(e => e.IdTurno).HasColumnName("Id_Turno");
            entity.Property(e => e.TipoTurno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_Turno");
        });

        modelBuilder.Entity<TurnoEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdTurnoEmpleado).HasName("PK__Turno_Em__5FDB5DF193C3F1A9");

            entity.ToTable("Turno_Empleado");

            entity.Property(e => e.IdTurnoEmpleado).HasColumnName("Id_TurnoEmpleado");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.IdTurno).HasColumnName("Id_Turno");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TurnoEmpleados)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurnoEmpleado_Empleado");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.TurnoEmpleados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurnoEmpleado_Turno");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__63C76BE2965D93AB");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(64)
                .HasColumnName("contraseña");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Usuario");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioEmpleado");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
