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
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<Maquina> Maquinas { get; set; }

    public virtual DbSet<TbEmpresa> TbEmpresas { get; set; }

    public virtual DbSet<TbEmpresasPermiso> TbEmpresasPermisos { get; set; }

    public virtual DbSet<TbEmpresasUsuario> TbEmpresasUsuarios { get; set; }

    public virtual DbSet<TbInformesJd> TbInformesJds { get; set; }

    public virtual DbSet<TbIngresosRealesyProyectado> TbIngresosRealesyProyectados { get; set; }

    public virtual DbSet<TbPermiso> TbPermisos { get; set; }

    public virtual DbSet<TbProyecto> TbProyectos { get; set; }

    public virtual DbSet<TbRolPermiso> TbRolPermisos { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

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

            entity.ToTable("TbEmpresas", "administracion");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.ApellidoGerente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellido_gerente");
            entity.Property(e => e.ApellidoResponsable)
                .HasMaxLength(200)
                .HasColumnName("apellido_responsable");
            entity.Property(e => e.CargoResponsable)
                .HasMaxLength(200)
                .HasColumnName("cargo_responsable");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(200)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal).HasColumnName("codigo_postal");
            entity.Property(e => e.CorreoEmpresa)
                .HasMaxLength(200)
                .HasColumnName("correo_empresa");
            entity.Property(e => e.CorreoGerente)
                .HasMaxLength(200)
                .HasColumnName("correo_gerente");
            entity.Property(e => e.CorreoResponsable)
                .HasMaxLength(200)
                .HasColumnName("correo_responsable");
            entity.Property(e => e.Departamento)
                .HasMaxLength(200)
                .HasColumnName("departamento");
            entity.Property(e => e.DireccionEmpresa)
                .HasMaxLength(200)
                .HasColumnName("direccion_empresa");
            entity.Property(e => e.FechaCreacionEmpresa).HasColumnName("fecha_creacion_empresa");
            entity.Property(e => e.Industria).HasMaxLength(200);
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(200)
                .HasColumnName("Nombre_empresa");
            entity.Property(e => e.NombreGerente)
                .HasMaxLength(200)
                .HasColumnName("nombre_gerente");
            entity.Property(e => e.NombreResponsable)
                .HasMaxLength(200)
                .HasColumnName("nombre_responsable");
            entity.Property(e => e.Pais)
                .HasMaxLength(200)
                .HasColumnName("pais");
            entity.Property(e => e.TelefonoEmpresa)
                .HasMaxLength(50)
                .HasColumnName("telefono_empresa");
            entity.Property(e => e.TelefonoGerente)
                .HasMaxLength(50)
                .HasColumnName("telefono_gerente");
            entity.Property(e => e.TelefonoResponsable)
                .HasMaxLength(50)
                .HasColumnName("telefono_responsable");
        });

        modelBuilder.Entity<TbEmpresasPermiso>(entity =>
        {
            entity.HasKey(e => e.IdEmpresapermiso);

            entity.ToTable("TbEmpresasPermisos", "seguridad");

            entity.Property(e => e.IdEmpresapermiso).HasColumnName("id_empresapermiso");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TbEmpresasPermisos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_TbEmpresas_TbEmpresasPermisos_id_empresa");
        });

        modelBuilder.Entity<TbEmpresasUsuario>(entity =>
        {
            entity.HasKey(e => e.IdEmpresasusuarios);

            entity.ToTable("TbEmpresasUsuarios", "seguridad");

            entity.Property(e => e.IdEmpresasusuarios).HasColumnName("id_empresasusuarios");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TbEmpresasUsuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_TbEmpresas_TbEmpresasUsuarios_id_empresa");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TbEmpresasUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_TbUsuarios_TbEmpresasUsuarios_id_usuario");
        });

        modelBuilder.Entity<TbInformesJd>(entity =>
        {
            entity.HasKey(e => e.IdInforme).HasName("PK_NewTable");

            entity.ToTable("TbInformesJD", "administracion");

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

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TbInformesJds)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_TbEmpresas_TbInformesJD_id_empresa");
        });

        modelBuilder.Entity<TbIngresosRealesyProyectado>(entity =>
        {
            entity.HasKey(e => e.IdIngreso);

            entity.ToTable("TbIngresosRealesyProyectados", "administracion");

            entity.Property(e => e.IdIngreso).HasColumnName("id_ingreso");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CategoriaIngreso)
                .HasMaxLength(200)
                .HasColumnName("categoria_ingreso");
            entity.Property(e => e.ConceptoIngreso)
                .HasMaxLength(200)
                .HasColumnName("concepto_ingreso");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.ValorIngresoProyectado).HasColumnName("valor_ingreso_proyectado");
            entity.Property(e => e.ValorIngresoReal).HasColumnName("valor_ingreso_real");
            entity.Property(e => e.VerticalNegocio)
                .HasMaxLength(200)
                .HasColumnName("vertical_negocio");
        });

        modelBuilder.Entity<TbPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK_Tb_Permisos");

            entity.ToTable("TbPermisos", "seguridad");

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IdPermisopadre).HasColumnName("id_permisopadre");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(200)
                .HasColumnName("Nombre_permiso");
            entity.Property(e => e.Padre).HasColumnName("padre");
            entity.Property(e => e.UrlAplicacion)
                .HasMaxLength(200)
                .HasColumnName("url_aplicacion");
        });

        modelBuilder.Entity<TbProyecto>(entity =>
        {
            entity.HasKey(e => e.IdProyecto).HasName("PK_TbEmpresas");

            entity.ToTable("TbProyectos", "administracion");

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

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TbProyectos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_TbEmpresas_TbProyectos_id_empresa");
        });

        modelBuilder.Entity<TbRolPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolpermiso).HasName("PK_TbRolEmpresaPermiso");

            entity.ToTable("TbRolPermiso", "seguridad");

            entity.Property(e => e.IdRolpermiso).HasColumnName("id_rolpermiso");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.TbRolPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK_TbPermisos_TbRolPermiso_id_permiso");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TbRolPermisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_TbRoles_TbRolPermiso_id_rol");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("TbRoles", "seguridad");

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

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
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

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TbUsuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_TbRoles__TbUsuarios_id_rol");
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
