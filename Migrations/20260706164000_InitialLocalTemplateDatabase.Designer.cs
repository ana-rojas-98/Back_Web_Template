using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using StrategicviewBack.Models;

#nullable disable

namespace StrategicviewBack.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260706164000_InitialLocalTemplateDatabase")]
    partial class InitialLocalTemplateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.UseIdentityColumns();

            BuildModel(modelBuilder);
#pragma warning restore 612, 618
        }

        internal static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("StrategicviewBack.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("company_id");

                    b.Property<string>("ManagerLastName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("manager_last_name");

                    b.Property<string>("ContactLastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("contact_last_name");

                    b.Property<string>("ContactPosition")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("contact_position");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("city");

                    b.Property<int?>("PostalCode")
                        .HasColumnType("int")
                        .HasColumnName("postal_code");

                    b.Property<string>("CompanyEmail")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("company_email");

                    b.Property<string>("ManagerEmail")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("manager_email");

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("contact_email");

                    b.Property<string>("State")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("state");

                    b.Property<string>("CompanyAddress")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("company_address");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Industry")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("industry");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("company_name");

                    b.Property<string>("ManagerFirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("manager_first_name");

                    b.Property<string>("ContactFirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("contact_first_name");

                    b.Property<string>("Country")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("country");

                    b.Property<string>("CompanyPhone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("company_phone");

                    b.Property<string>("ManagerPhone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("manager_phone");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("contact_phone");

                    b.HasKey("CompanyId")
                        .HasName("PK_Companies");

                    b.ToTable("Companies", "administration");

                    b.HasData(new
                    {
                        CompanyId = 1,
                        CompanyName = "Demo Company",
                        Industry = "Software",
                        Country = "Local",
                        City = "Local",
                        CompanyEmail = "company@example.com"
                    });
                });

            modelBuilder.Entity("StrategicviewBack.Models.CompanyUser", b =>
                {
                    b.Property<int>("CompanyUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("company_user_id");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("company_id");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("CompanyUserId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyUsers", "security");

                    b.HasData(new
                    {
                        CompanyUserId = 1,
                        CompanyId = 1,
                        UserId = 1
                    });
                });

            modelBuilder.Entity("StrategicviewBack.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("permission_id");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("icon");

                    b.Property<int?>("ParentPermissionId")
                        .HasColumnType("int")
                        .HasColumnName("parent_permission_id");

                    b.Property<string>("PermissionName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("permission_name");

                    b.Property<bool?>("IsParent")
                        .HasColumnType("bit")
                        .HasColumnName("is_parent");

                    b.Property<string>("ApplicationUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("application_url");

                    b.HasKey("PermissionId")
                        .HasName("PK_Permissions");

                    b.ToTable("Permissions", "security");

                    b.HasData(
                        new { PermissionId = 1, PermissionName = "Administration", ApplicationUrl = "/admin", Icon = "settings", IsParent = true },
                        new { PermissionId = 2, PermissionName = "Users", ApplicationUrl = "/users", Icon = "users", IsParent = false, ParentPermissionId = 1 });
                });

            modelBuilder.Entity("StrategicviewBack.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("company_id");

                    b.Property<string>("RoleName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId");

                    b.ToTable("Roles", "security");

                    b.HasData(new
                    {
                        RoleId = 1,
                        CompanyId = 1,
                        RoleName = "Admin"
                    });
                });

            modelBuilder.Entity("StrategicviewBack.Models.RolePermission", b =>
                {
                    b.Property<int>("RolePermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_permission_id");

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int")
                        .HasColumnName("permission_id");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("RolePermissionId")
                        .HasName("PK_RolePermissions");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions", "security");

                    b.HasData(new
                    {
                        RolePermissionId = 1,
                        PermissionId = 2,
                        RoleId = 1
                    });
                });

            modelBuilder.Entity("StrategicviewBack.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("email");

                    b.Property<DateOnly?>("CompanyJoinedAt")
                        .HasColumnType("date")
                        .HasColumnName("company_joined_at");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int?>("IdentificationNumber")
                        .HasColumnType("int")
                        .HasColumnName("identification_number");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("phone");

                    b.Property<string>("IdentificationType")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("identification_type");

                    b.Property<string>("Username")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", "security");

                    b.HasData(new
                    {
                        UserId = 1,
                        RoleId = 1,
                        FirstName = "Admin",
                        LastName = "Demo",
                        Username = "admin",
                        Password = "Admin123*",
                        Email = "admin@example.com",
                        Phone = "0000000000"
                    });
                });

            modelBuilder.Entity("StrategicviewBack.Models.CompanyUser", b =>
                {
                    b.HasOne("StrategicviewBack.Models.Company", "CompanyNavigation")
                        .WithMany("CompanyUsers")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_CompanyUsers_Companies_company_id");

                    b.HasOne("StrategicviewBack.Models.User", "UserNavigation")
                        .WithMany("CompanyUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_CompanyUsers_Users_user_id");

                    b.Navigation("CompanyNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("StrategicviewBack.Models.RolePermission", b =>
                {
                    b.HasOne("StrategicviewBack.Models.Permission", "PermissionNavigation")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_RolePermissions_Permissions_permission_id");

                    b.HasOne("StrategicviewBack.Models.Role", "RoleNavigation")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_RolePermissions_Roles_role_id");

                    b.Navigation("PermissionNavigation");

                    b.Navigation("RoleNavigation");
                });

            modelBuilder.Entity("StrategicviewBack.Models.User", b =>
                {
                    b.HasOne("StrategicviewBack.Models.Role", "RoleNavigation")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Users_Roles_role_id");

                    b.Navigation("RoleNavigation");
                });

            modelBuilder.Entity("StrategicviewBack.Models.Company", b =>
                {
                    b.Navigation("CompanyUsers");
                });

            modelBuilder.Entity("StrategicviewBack.Models.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("StrategicviewBack.Models.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("StrategicviewBack.Models.User", b =>
                {
                    b.Navigation("CompanyUsers");
                });
        }
    }
}
