using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StrategicviewBack.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyUser> CompanyUsers { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException(
                "Database connection is not configured. Set ConnectionStrings:DefaultConnection in appsettings, user-secrets, or environment variables.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_Companies");

            entity.ToTable("Companies", "administration");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ManagerLastName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("manager_last_name");
            entity.Property(e => e.ContactLastName)
                .HasMaxLength(200)
                .HasColumnName("contact_last_name");
            entity.Property(e => e.ContactPosition)
                .HasMaxLength(200)
                .HasColumnName("contact_position");
            entity.Property(e => e.City)
                .HasMaxLength(200)
                .HasColumnName("city");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.CompanyEmail)
                .HasMaxLength(200)
                .HasColumnName("company_email");
            entity.Property(e => e.ManagerEmail)
                .HasMaxLength(200)
                .HasColumnName("manager_email");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(200)
                .HasColumnName("contact_email");
            entity.Property(e => e.State)
                .HasMaxLength(200)
                .HasColumnName("state");
            entity.Property(e => e.CompanyAddress)
                .HasMaxLength(200)
                .HasColumnName("company_address");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Industry).HasMaxLength(200).HasColumnName("industry");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .HasColumnName("company_name");
            entity.Property(e => e.ManagerFirstName)
                .HasMaxLength(200)
                .HasColumnName("manager_first_name");
            entity.Property(e => e.ContactFirstName)
                .HasMaxLength(200)
                .HasColumnName("contact_first_name");
            entity.Property(e => e.Country)
                .HasMaxLength(200)
                .HasColumnName("country");
            entity.Property(e => e.CompanyPhone)
                .HasMaxLength(50)
                .HasColumnName("company_phone");
            entity.Property(e => e.ManagerPhone)
                .HasMaxLength(50)
                .HasColumnName("manager_phone");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .HasColumnName("contact_phone");
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.CompanyUserId).HasName("PK_CompanyUsers");

            entity.ToTable("CompanyUsers", "security");

            entity.Property(e => e.CompanyUserId).HasColumnName("company_user_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_CompanyUsers_Companies_company_id");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CompanyUsers_Users_user_id");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK_Permissions");

            entity.ToTable("Permissions", "security");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.ParentPermissionId).HasColumnName("parent_permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(200)
                .HasColumnName("permission_name");
            entity.Property(e => e.IsParent).HasColumnName("is_parent");
            entity.Property(e => e.ApplicationUrl)
                .HasMaxLength(200)
                .HasColumnName("application_url");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId).HasName("PK_RolePermissions");

            entity.ToTable("RolePermissions", "security");

            entity.Property(e => e.RolePermissionId).HasColumnName("role_permission_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.PermissionNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_RolePermissions_Permissions_permission_id");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RolePermissions_Roles_role_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Roles");

            entity.ToTable("Roles", "security");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users");

            entity.ToTable("Users", "security");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.CompanyJoinedAt).HasColumnName("company_joined_at");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.IdentificationNumber).HasColumnName("identification_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.IdentificationType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("identification_type");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles_role_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
