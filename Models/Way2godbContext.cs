using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StrategicviewBack.Models;

public partial class Way2godbContext : DbContext
{
    public Way2godbContext()
    {
    }

    public Way2godbContext(DbContextOptions<Way2godbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Maquina> Maquinas { get; set; }

    public virtual DbSet<TbEmpresa> TbEmpresas { get; set; }

    public virtual DbSet<TbEmpresasPermiso> TbEmpresasPermisos { get; set; }

    public virtual DbSet<TbPermiso> TbPermisos { get; set; }

    public virtual DbSet<TbProyecto> TbProyectos { get; set; }

    public virtual DbSet<TbRolEmpresaPermiso> TbRolEmpresaPermisos { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TdInformesJd> TdInformesJds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=way2godb.cnwk0usq629g.us-east-2.rds.amazonaws.com,1433;Initial Catalog=way2godb;Persist Security Info=False;User ID=admin;Password=Algoritmo2023!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Maquina>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("maquinas");

            entity.Property(e => e.IdMachine).HasColumnName("Id_machine");
            entity.Property(e => e.Plant).IsUnicode(false);
            entity.Property(e => e.Reference).IsUnicode(false);
        });

        modelBuilder.Entity<TbEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK_Tb_Empresas");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.ApellidoGerente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellido_gerente");
            entity.Property(e => e.ApellidoResponsable)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellido_responsable");
            entity.Property(e => e.CargoResponsable)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("cargo_responsable");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal).HasColumnName("codigo_postal");
            entity.Property(e => e.CorreoEmpresa)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("correo_empresa");
            entity.Property(e => e.CorreoGerente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("correo_gerente");
            entity.Property(e => e.CorreoResponsable)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("correo_responsable");
            entity.Property(e => e.Departamento)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.DireccionEmpresa)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion_empresa");
            entity.Property(e => e.FechaCreacionEmpresa).HasColumnName("fecha_creacion_empresa");
            entity.Property(e => e.IdIndustria).HasColumnName("id_industria");
            entity.Property(e => e.NombreGerente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_gerente");
            entity.Property(e => e.NombreResponsable)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_responsable");
            entity.Property(e => e.Pais)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.TelefonoEmpresa).HasColumnName("telefono_empresa");
            entity.Property(e => e.TelefonoGerente).HasColumnName("telefono_gerente");
            entity.Property(e => e.TelefonoResponsable).HasColumnName("telefono_responsable");
        });

        modelBuilder.Entity<TbEmpresasPermiso>(entity =>
        {
            entity.HasKey(e => e.IdEmpresapermiso);

            entity.Property(e => e.IdEmpresapermiso).HasColumnName("id_empresapermiso");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TbEmpresasPermisos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("id_empresa");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.TbEmpresasPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("id_permiso");
        });

        modelBuilder.Entity<TbPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK_Tb_Permisos");

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.Valor)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<TbProyecto>(entity =>
        {
            entity.HasKey(e => e.IdProyecto).HasName("PK_TbEmpresas");

            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Cliente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.FechaFinal).HasColumnName("fecha_final");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.LiderProyecto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("lider_proyecto");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("objetivo");
            entity.Property(e => e.ProyectoNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("proyecto_nombre");
            entity.Property(e => e.VerticalNegocio)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vertical_negocio");
        });

        modelBuilder.Entity<TbRolEmpresaPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolempresapermiso);

            entity.ToTable("TbRolEmpresaPermiso");

            entity.Property(e => e.IdRolempresapermiso)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_rolempresapermiso");
            entity.Property(e => e.IdEmpresapermiso).HasColumnName("id_empresapermiso");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("id_usuario");

            entity.HasOne(d => d.IdRolempresapermisoNavigation).WithOne(p => p.TbRolEmpresaPermiso)
                .HasForeignKey<TbRolEmpresaPermiso>(d => d.IdRolempresapermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_usuario");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.Rol)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("TbUsuarios", "seguridad");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.FechaIngresoEmpresa).HasColumnName("fecha_ingreso_empresa");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telefono)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("tipo_identificacion");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.TbUsuario)
                .HasForeignKey<TbUsuario>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_rol");
        });

        modelBuilder.Entity<TdInformesJd>(entity =>
        {
            entity.HasKey(e => e.IdInforme).HasName("PK_NewTable");

            entity.ToTable("TdInformesJD");

            entity.Property(e => e.IdInforme)
                .ValueGeneratedNever()
                .HasColumnName("id_informe");
            entity.Property(e => e.Estado)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Etapa)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("etapa");
            entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
            entity.Property(e => e.FechaJunta).HasColumnName("fecha_junta");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.PorcentajeCumplimiento).HasColumnName("porcentaje_cumplimiento");
            entity.Property(e => e.Responsable)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("responsable");
            entity.Property(e => e.TituloInforme)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("titulo_informe");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Age).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
