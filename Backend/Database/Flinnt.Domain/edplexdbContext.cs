using System;
using Flinnt.Business.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Flinnt.Business.ViewModels;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Flinnt.Domain
{
    public partial class edplexdbContext : IdentityDbContext<ApplicationUser>
    {
        public edplexdbContext()
        {
        }

        public edplexdbContext(DbContextOptions<edplexdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AutheticationType> AutheticationTypes { get; set; }
        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }
        public virtual DbSet<InstituteBatch> InstituteBatches { get; set; }
        public virtual DbSet<InstituteConfiguration> InstituteConfigurations { get; set; }
        public virtual DbSet<InstituteConfigureSession> InstituteConfigureSessions { get; set; }
        public virtual DbSet<InstituteDivision> InstituteDivisions { get; set; }
        public virtual DbSet<InstituteGroup> InstituteGroups { get; set; }
        public virtual DbSet<InstituteSemester> InstituteSemesters { get; set; }
        public virtual DbSet<InstituteSession> InstituteSessions { get; set; }
        public virtual DbSet<InstituteType> InstituteTypes { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<Medium> Medium { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Standard> Standards { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAccountHistory> UserAccountHistories { get; set; }
        public virtual DbSet<UserAccountVerification> UserAccountVerifications { get; set; }
        public virtual DbSet<UserDevice> UserDevices { get; set; }
        public virtual DbSet<UserInstitute> UserInstitutes { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=101.53.144.96,1232;Database=edplexdb;User ID=edplexdb_user; Password=ad9g@z385");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.ToTable("AcademicYear");

                entity.HasComment("This entity stores an academic year list.");

                entity.Property(e => e.AcademicYearId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnName("CreateDateTIme")
                    .HasComment("The date and time when this entry was done.");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The academic year display name.");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasComment("The date when the academic year ends.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the academic year is ready to use.");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasComment("The date when the academic year starts.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<AutheticationType>(entity =>
            {
                entity.HasKey(e => e.AuthenticationTypeId)
                    .HasName("PK__Authetic__886BDD752569B719");

                entity.ToTable("AutheticationType");

                entity.HasComment("This entity stores an authentication type list.");

                entity.Property(e => e.AuthenticationTypeId)
                    .ValueGeneratedOnAdd()
                    .HasComment("Unique Identifier.");

                entity.Property(e => e.AuthenticationType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The authentication type.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when entry was made.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when entry was last updated.");
            });

            modelBuilder.Entity<Board>(entity =>
            {
                entity.ToTable("Board");

                entity.HasComment("This entity stores a list of education boards.");

                entity.Property(e => e.BoardId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.BoardName)
                    .HasMaxLength(255)
                    .HasComment("The education board name.");

                entity.Property(e => e.BoardShortName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("The education board short name.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the education board is ready to use.");

                entity.Property(e => e.IsOptional)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, the board is optional.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasComment("This entity stores a city list.");

                entity.Property(e => e.CityId).HasComment("The unique identifier.");

                entity.Property(e => e.CityName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The city name.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the city is ready to use.");

                entity.Property(e => e.StateId).HasComment("The state identifier this city belongs to. Ref.: State.StateId");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cities_state_id");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasComment("This entity stores a country list.");

                entity.Property(e => e.CountryId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The country name.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the country is ready to use.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<GroupStructure>(entity =>
            {
                entity.ToTable("GroupStructure");

                entity.HasComment("This entity stores a list of group structures, like: Board->Medium->Standard, Board->Medium->Standard->Division etc.");

                entity.Property(e => e.GroupStructureId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The group structure description.");

                entity.Property(e => e.DisplayOrder).HasComment("The display order of the group structure.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the group structure is ready to use.");

                entity.Property(e => e.StructureTitle)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("The group structure title.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<GroupStructureType>(entity =>
            {
                entity.ToTable("GroupStructureType");

                entity.HasComment("This entity stores mapping between a group structure and institute types.");

                entity.Property(e => e.GroupStructureTypeId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.GroupStructureId).HasComment("The group structure identifier this type belongs to.");

                entity.Property(e => e.InstituteTypeId).HasComment("The institute type identifier this group belongs to.");

                entity.HasOne(d => d.GroupStructure)
                    .WithMany(p => p.GroupStructureTypes)
                    .HasForeignKey(d => d.GroupStructureId)
                    .HasConstraintName("fk_group_structure_type_group_structure_id");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.HasComment("This entity stores a gender list.");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.Gender1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Gender")
                    .HasComment("The gender.");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the gender is ready to use.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<Institute>(entity =>
            {
                entity.ToTable("Institute");

                entity.HasComment("This entity stores the institute information.\r\nMigration:\r\nInstituteName < users.user_school_name\r\nFirstName < users.user_firstname\r\nLastName < users.user_lastname\r\nEmailId < users.user_email\r\nMobileNo < users.user_mobile\r\nAddress < users.user_address\r\nCityId < users.user_city\r\nStateId < users.user_state\r\nCountryId < users.user_country\r\nPincode < users.user_pincode\r\nWebsite < users.user_website\r\nPublicPageName < users.user_name\r\nPageNameChanged < users.user_name_changed\r\nDisplayPicture < users.user_picture\r\nBannerPicture < users.user_school_banner");

                entity.HasIndex(e => e.InstituteTypeId, "idx_institute_institute_type_id");

                entity.Property(e => e.InstituteId).HasComment("The unique identifier.");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The institute address.\r\nMigrate: users.user_address");

                entity.Property(e => e.BannerPicture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The institute banner image path of the institute.\r\nMigrate: users.user_school_banner");

                entity.Property(e => e.CityId).HasComment("The city identifier this institute address belongs to. Ref.: City.CityId\r\nMigrate: users.user_city");

                entity.Property(e => e.CountryId).HasComment("The country identifier this institute address belongs to.  Ref.: Country.CountryId\r\nMigrate: users.user_country");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.DisplayPicture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The profile picture path of the institute.\r\nMigrate: users.user_picture");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The contact person email address.\r\nMigrate: users.user_email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasComment("The contact person first name.\r\nMigrate: users.user_firstname");

                entity.Property(e => e.InstituteName)
                    .HasMaxLength(255)
                    .HasComment("The institute name.")
                    .UseCollation("SQL_1xCompat_CP850_CI_AS");

                entity.Property(e => e.InstituteTypeId).HasComment("The institute type identifier this institute belongs to. Ref.: InstituteType.InstituteTypeId");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasComment("The contact person last name.\r\nMigrate: users.user_lastname");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("The contact person mobile number.\r\nMigrate: users.user_mobile");

                entity.Property(e => e.PageNameChanged).HasComment("If 1, the institute has already changed his public page name once.\r\nMigrate: users.user_name_changed");

                entity.Property(e => e.Pincode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The pincode of the institute address.\r\nMigrate: users.user_pincode");

                entity.Property(e => e.PublicPageName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasComment("The public page name (part of the permanent URL) of the institute.\r\nMigrate: users.user_name");

                entity.Property(e => e.StateId).HasComment("The state identifier this institute address belongs to. Ref.: State.StateId\r\nMigrate: users.user_state");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.Property(e => e.Website)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The website address of the institute.\r\nMigrate: users.user_website");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Institutes)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_institutions_city_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Institutes)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_institutions_country_id");

                entity.HasOne(d => d.InstituteType)
                    .WithMany(p => p.Institutes)
                    .HasForeignKey(d => d.InstituteTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_institutions_inst_type_id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Institutes)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("fk_institutions_state_id");
            });

            modelBuilder.Entity<InstituteBatch>(entity =>
            {
                entity.ToTable("InstituteBatch");

                entity.HasComment("This entity stores a batch for institutes.");

                entity.Property(e => e.InstituteBatchId).HasComment("The unique identifier.");

                entity.Property(e => e.BatchName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasComment("The batch name.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.EndTime).HasComment("The time when the batch ends.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the batch is ready to use.");

                entity.Property(e => e.StartTime).HasComment("The time when the batch starts.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<InstituteConfiguration>(entity =>
            {
                entity.ToTable("InstituteConfiguration");

                entity.Property(e => e.InstituteConfigurationId).HasComment("The unique identifier.");

                entity.Property(e => e.ConfigurationKey)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("The configuration key.");

                entity.Property(e => e.ConfigurationValue).HasComment("The configuration value.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.InstituteId).HasComment("The institute identifier this configuration belongs to.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.InstituteConfigurations)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("institute_configuration_institute_id");
            });

            modelBuilder.Entity<InstituteConfigureSession>(entity =>
            {
                entity.ToTable("InstituteConfigureSession");

                entity.Property(e => e.CurrentStep).HasComment("");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.InstituteConfigureSessions)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("fk_institute_configure_session_board_id");

                entity.HasOne(d => d.GroupStructure)
                    .WithMany(p => p.InstituteConfigureSessions)
                    .HasForeignKey(d => d.GroupStructureId)
                    .HasConstraintName("fk_institute_configure_session_group_structure_id");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.InstituteConfigureSessions)
                    .HasForeignKey(d => d.InstituteId)
                    .HasConstraintName("fk_institute_configure_session_institute_id");

                entity.HasOne(d => d.IntituteType)
                    .WithMany(p => p.InstituteConfigureSessions)
                    .HasForeignKey(d => d.IntituteTypeId)
                    .HasConstraintName("fk_institute_configure_session_institute_type_id");

                entity.HasOne(d => d.Medium)
                    .WithMany(p => p.InstituteConfigureSessions)
                    .HasForeignKey(d => d.MediumId)
                    .HasConstraintName("fk_institute_configure_session_medium_id");
            });

            modelBuilder.Entity<InstituteDivision>(entity =>
            {
                entity.ToTable("InstituteDivision");

                entity.HasComment("This entitiy stores a list of divisions for institutes.");

                entity.Property(e => e.InstituteDivisionId).HasComment("The unique idenfitier");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.DisplayOrder).HasComment("The display order of the division.");

                entity.Property(e => e.DivisionName)
                    .HasMaxLength(75)
                    .HasComment("The division name.");

                entity.Property(e => e.InstituteGroupId).HasComment("The institute group identifier this division belongs to. Ref.: InstituteGroup.InstituteGroupId");

                entity.Property(e => e.IsActive).HasComment("If 1, the division is ready to use.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.HasOne(d => d.InstituteGroup)
                    .WithMany(p => p.InstituteDivisions)
                    .HasForeignKey(d => d.InstituteGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_institute_division_institute_group_id");
            });

            modelBuilder.Entity<InstituteGroup>(entity =>
            {
                entity.ToTable("InstituteGroup");

                entity.HasComment("This entity stores institute wise group details like Medium, Board and Standard.");

                entity.Property(e => e.InstituteGroupId).HasComment("The unique identifier.");

                entity.Property(e => e.BoardId).HasComment("The board identifier this group belongs to. Ref.: Board.BoardId");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.DisplayOrder).HasComment("The display order of the group.");

                entity.Property(e => e.InstituteId).HasComment("The institute identifier this group belongs to. Ref.: Institute.InstituteId");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the group is ready to use.");

                entity.Property(e => e.MediumId).HasComment("The medium identifier this group belongs to. Ref.: Medium.MediumId");

                entity.Property(e => e.StandardId).HasComment("The standard identifier this group belongs to. Ref.: Standard.StandardId");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.InstituteGroups)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("fk_institute_group_board_id");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.InstituteGroups)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_institute_group_institute_id");

                entity.HasOne(d => d.Medium)
                    .WithMany(p => p.InstituteGroups)
                    .HasForeignKey(d => d.MediumId)
                    .HasConstraintName("fk_institute_group_medium_id");

                entity.HasOne(d => d.Standard)
                    .WithMany(p => p.InstituteGroups)
                    .HasForeignKey(d => d.StandardId)
                    .HasConstraintName("fk_institute_group_standard_id");
            });

            modelBuilder.Entity<InstituteSemester>(entity =>
            {
                entity.ToTable("InstituteSemester");

                entity.HasComment("This entity store institute and semester mapping.");

                entity.Property(e => e.InstituteSemesterId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.DisplayOrder).HasComment("The display order of this semester.");

                entity.Property(e => e.EndDateTime).HasComment("The date and time when this semester ends.");

                entity.Property(e => e.InstituteGroupId).HasComment("The institute group identifier this semester belongs to.");

                entity.Property(e => e.InstituteId).HasComment("The institute identifier this semester belongs to. Ref.: Semester.SemesterId");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, this institute and semester mapping is ready to use.");

                entity.Property(e => e.SemesterId).HasComment("The semester identifier this institute belongs to. Ref.: Institute.InstituteId");

                entity.Property(e => e.StartDateTime).HasComment("The date and time when this semester starts.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.InstituteSemesters)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_institute_semester_institute_id");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.InstituteSemesters)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_institute_semester");
            });

            modelBuilder.Entity<InstituteSession>(entity =>
            {
                entity.ToTable("InstituteSession");

                entity.HasComment("This entity stores a session list for institutes.");

                entity.Property(e => e.InstituteSessionId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.EndTime).HasComment("The time when the session ends.");

                entity.Property(e => e.InstituteBatchId).HasComment("The institute batch identifier this session belongs to.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the session is ready to use.");

                entity.Property(e => e.SessionName)
                    .HasMaxLength(75)
                    .HasComment("The session name.");

                entity.Property(e => e.StartTime).HasComment("The time when the session starts.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<InstituteType>(entity =>
            {
                entity.HasKey(e => e.InstituteTypeId)
                    .HasName("PK__Institut__840D4EE2086237CA");

                entity.ToTable("InstituteType");

                entity.HasComment("This entity stores a list of institute types.");

                entity.Property(e => e.InstituteTypeId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the institute type is ready to use.");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The type name.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("LoginHistory");

                entity.HasComment("This entity stores a list of login history.\r\nMigartion:\r\nUserId < login_history.user_id\r\nLoginDateTime < login_history.access_dt\r\nIsLogout < login_history.is_logout\r\nClientIP < login_history.ip_addr\r\nClientDevice < login_history.device_type + login_history.device_detail\r\nAccessUrl	< login_history.access_url");

                entity.Property(e => e.LoginHistoryId).HasComment("The unique identifier.");

                entity.Property(e => e.AccessUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The URL on which request has been sent.\r\nMigration: login_history.access_url");

                entity.Property(e => e.ClientDevice)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The type of client device from where request has been made.\r\nMigration: login_history.device_type + \r\nlogin_history.device_detail");

                entity.Property(e => e.ClientIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ClientIP")
                    .HasComment("The client device IP address from where request has been sent.\r\nMigration: login_history.ip_addr");

                entity.Property(e => e.IsLogout).HasComment("If 1, this entry records the log out date and time.\r\nMigration: login_history.is_logout");

                entity.Property(e => e.LoginDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The date and time when user logged in.\r\nMigration: login_history.access_dt");

                entity.Property(e => e.UserId).HasComment("The user identifier this history belongs to. Ref. User.UserId\r\nMigration: login_history.user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_login_history_user_id");
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.ToTable("Medium");

                entity.HasComment("This entity stores a list of language mediums.");

                entity.Property(e => e.MediumId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the medium is ready to use.");

                entity.Property(e => e.IsOptional)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, the medium is optional.");

                entity.Property(e => e.MediumName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The medium name.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.HasComment("This entity stores a permission list.\r\nMigrations:\r\nPermissionName < permissions.perm_name\r\nDescription < permissions.perm_desc");

                entity.Property(e => e.PermissionId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("A short description on the permission.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the permission is ready to use.");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasComment("The permission name without any white space.");

                entity.Property(e => e.PermissionTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("The descriptive name for the permission.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasComment("This entity stores the institute staff roles like Owner, Admin, Principal etc.");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The role description");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the role is ready to use.");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The role name.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission");

                entity.HasComment("This entity stores a mapping between a role and permissions.");

                entity.Property(e => e.RolePermissionId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.PermissionId).HasComment("The permission identifier this role belongs to. Ref.: Permission.PermissionId");

                entity.Property(e => e.RoleId).HasComment("The role identifier this permission belongs to. Ref.: Role.RoleId");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_role_permissions_permission_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_role_permissions_role_id");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("Semester");

                entity.HasComment("This entity stores a semester list.");

                entity.Property(e => e.SemesterId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique idenfier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the semester is ready to use.");

                entity.Property(e => e.IsOptional)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, the semester is optional.");

                entity.Property(e => e.SemesterName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The semester name.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<Standard>(entity =>
            {
                entity.ToTable("Standard");

                entity.HasComment("This entity stores a standard list.");

                entity.Property(e => e.StandardId)
                    .ValueGeneratedOnAdd()
                    .HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the standard is ready to use.");

                entity.Property(e => e.IsOptional)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, the standard is optional.");

                entity.Property(e => e.StandardName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The standard name.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.HasComment("This entity stores a state list.");

                entity.Property(e => e.StateId).HasComment("The unique identifier.");

                entity.Property(e => e.CountryId).HasComment("The country identifier this state belongs to. Ref.: Country.CountryId");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the state is ready to use.");

                entity.Property(e => e.StateName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The state name.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_states_country_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasComment("This entity stores the user information along with their credentials.\r\nMigrations:\r\nLoginId < users.user_login\r\nRegistrationDate < users.user_acc_reg_date\r\nClientIP < user_acc_history.sub_hist_user_ip\r\nClientDevice < user_acc_history.sub_hist_device_type");

                entity.HasIndex(e => e.LastLoginDateTime, "idx_users_last_login_datetime");

                entity.HasIndex(e => e.LoginId, "idx_users_user_login")
                    .IsUnique();

                entity.Property(e => e.AuthenticationTypeId).HasComment("Authentication type opted by the user. Ref.: AuthenticationType.AuthTypeId");

                entity.Property(e => e.ClientDevice)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The type of device from where the request was initiated.\r\nMigrate: user_acc_history.sub_hist_device_type");

                entity.Property(e => e.ClientIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ClientIP")
                    .HasComment("The Client IP address from where the request was initiated.\r\nMigrate: user_acc_history.sub_hist_user_ip");

                entity.Property(e => e.DeletionDateTime)
                    .HasColumnType("date")
                    .HasComment("The date and time when the account was deleted.");

                entity.Property(e => e.InactiveReason)
                    .HasMaxLength(255)
                    .HasComment("The reason behind deactivating the user account.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the user account is active.");

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, the user account is deleted. This is used for soft delete.");

                entity.Property(e => e.LastLoginDateTime).HasComment("The date and time when user has last login.");

                entity.Property(e => e.LastLoginDevice)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The type of device from where the last login request was initiated.\r\nMigrate: user_acc_history.sub_hist_device_type");

                entity.Property(e => e.LastLoginIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LastLoginIP")
                    .HasComment("The Client IP from where the user last login request was initiated.");

                entity.Property(e => e.LoginId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("User login id.\r\nMigration: users.user_login");

                entity.Property(e => e.OneTimePassword)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Initial password which should be changed after login.");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Password.");

                entity.Property(e => e.RegistrationDateTime).HasComment("The date and time when user account is created.\r\nMigrate: users.user_acc_reg_date");

                entity.Property(e => e.StatusId).HasComment("The current transactional status of the users.");

                entity.Property(e => e.UseCustomPermission)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, custom permissions are assigned to the user.");

                entity.Property(e => e.UserTypeId).HasComment("The type of the user. Ref.: UserType.UserTypeId");

                entity.HasOne(d => d.AuthenticationType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AuthenticationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_auth_type_id");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_user_type_id");
            });

            modelBuilder.Entity<UserAccountHistory>(entity =>
            {
                entity.ToTable("UserAccountHistory");

                entity.HasComment("This entity stores the user account history.\r\nMigrations:\r\nHistoryAction < user_acc_history.sub_hist_action\r\nHistoryDateTime < user_acc_history.sub_hist_dt\r\nAdditionalInfo < user_acc_history.sub_hist_extra\r\nClientIp < user_acc_history.sub_hist_ip\r\nClientDevice < user_acc_history.sub_hist_device_type");

                entity.Property(e => e.UserAccountHistoryId).HasComment("Unique identifier");

                entity.Property(e => e.ActionUserId).HasComment("The user identifier who has performed an action. Ref.: User.UserId");

                entity.Property(e => e.ClientDevice)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The Client device type from where the request was initiated.");

                entity.Property(e => e.ClientIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ClientIP")
                    .HasComment("The Client IP address from where the request was initiated.");

                entity.Property(e => e.HistoryAction)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The action performed on the user account.");

                entity.Property(e => e.HistoryDateTime).HasComment("The date and time when the action was performed.");

                entity.Property(e => e.UserId).HasComment("The user identifier this history belongs to. Ref.: User.UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccountHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_acc_history_user_id");
            });

            modelBuilder.Entity<UserAccountVerification>(entity =>
            {
                entity.ToTable("UserAccountVerification");

                entity.HasComment("This entity stores the user account verification requests.");

                entity.HasIndex(e => e.UserId, "idx_user_account_verification_user_id");

                entity.HasIndex(e => e.VerificationCode, "idx_user_account_verification_verification_code");

                entity.Property(e => e.UserAccountVerificationId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.ExpireDateTime).HasComment("The date and time when the code expires.");

                entity.Property(e => e.IsVerified)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, the code is already verified.");

                entity.Property(e => e.UserId).HasComment("The user identifier this verification belongs to. Ref.: User.UserId");

                entity.Property(e => e.VerificationCode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The verification code.");

                entity.Property(e => e.VerifyClientDevice)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The Client Device from where the verification request was initiated.");

                entity.Property(e => e.VerifyClientIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("VerifyClientIP")
                    .HasComment("The Client IP address from where the verification request was initiated.");

                entity.Property(e => e.VerifyDateTime).HasComment("The date and time when the code was verified.");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccountVerifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_account_verifications_user_id");
            });

            modelBuilder.Entity<UserDevice>(entity =>
            {
                entity.ToTable("UserDevice");

                entity.HasComment("This entity stores the device information from where the user has login at least once.");

                entity.Property(e => e.UserDeviceId).HasComment("The unique identifier.");

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("If the device is mobile, then the application version.");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The device id. Autogenerated based on the device signature.");

                entity.Property(e => e.DeviceType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The device type.");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the device is ready to use.");

                entity.Property(e => e.MobileOs)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MobileOS")
                    .HasComment("The mobile operating system.");

                entity.Property(e => e.NotificationId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("The notification identifier. Possibly the Firebase id.");

                entity.Property(e => e.RegisterDateTime).HasComment("The date and time when this device was first used.");

                entity.Property(e => e.UserAgent)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("The client device user agent.");

                entity.Property(e => e.UserId).HasComment("The user identifier this device belongs to. Ref.: User.UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDevices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_devices_user_id");
            });

            modelBuilder.Entity<UserInstitute>(entity =>
            {
                entity.ToTable("UserInstitute");

                entity.HasComment("This entity stores user and institute mapping.");

                entity.Property(e => e.UserInstituteId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.InstituteId).HasComment("The institute identifier this user belongs to. Ref.: Institute.InstituteId");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, the user and institute relation is active.");

                entity.Property(e => e.RoleId).HasComment("The role identifier this user belongs to the user and institute. Ref.: Role.RoleId");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.Property(e => e.UserId).HasComment("The user identifier belongs to the institute. Ref.: User.UserId\r\nMigrate: user_courses(find from the\r\nprivate course subscription)");

                entity.Property(e => e.UserTypeId).HasComment("The user type identifier this user belongs to the institute. Ref. UserType.UserTypeId");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.UserInstitutes)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_institute_institute_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInstitutes)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_institute_role_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInstitutes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_institute_user_id");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.UserInstitutes)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_institute_user_type_id");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.ToTable("UserPermission");

                entity.HasComment("This entity stores a mapping between a user and permissions.");

                entity.Property(e => e.UserPermissionId).HasComment("The unique identifier");

                entity.Property(e => e.CourseId).HasComment("The course identifier this user and permission belongs to. Ref.: Course.CourseId");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.InstituteId).HasComment("The institute identifier this user and permission belongs to. Ref.: Institute.InstituteId");

                entity.Property(e => e.PermissionId).HasComment("The permission identifier this user belongs to. Ref.: Permission.PermissionId");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.Property(e => e.UserId).HasComment("The user identifier this permisssion belongs to. Ref.: User.UserId");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_permissions_permission_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_permissions_user_id");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.HasComment("This entity stores the user profile.\r\nMigration:\r\nUserProfiles\r\n Stores user profile information.\r\n\r\nFirstName	 < users.user_firstname\r\nLastName	< users.user_lastname\r\nEmailId < users.user_email\r\nMobileNo < users.user_mobile\r\nDOB < users.user_birthdate\r\nGenderId < users.user_gender\r\nAddress < users.user_address\r\nCityId < users.user_city\r\nStateId < users.user_state\r\nCountryId < users.user_country\r\nPincode < users.user_pincode\r\nDisplayPicture < users.user_picture\r\nPublicId < users.user_public_id\r\nPublicIdSalt < users.user_public_id_salt");

                entity.Property(e => e.UserProfileId).HasComment("The unique identifier.");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The residential address.\r\nMigrate: users.user_address");

                entity.Property(e => e.CityId).HasComment("The city identifier this profile belongs to. Ref.: City.CityId\r\nMigrate: users.user_city");

                entity.Property(e => e.CountryId)
                    .HasDefaultValueSql("((1))")
                    .HasComment("The country identifier this profile belongs to. Ref.: Country.CountryId\r\nMigrate: users.user_country");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.DisplayPicture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The display picture of the user.\r\nMigrate: users.user_picture");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB")
                    .HasComment("The user date of birth.\r\nMigration: users.user_birthdate");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("The user email address.\r\nMigration: users.user_email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("The user first name.\r\nMigration: users.user_firstname");

                entity.Property(e => e.GenderId).HasComment("The gender identifier this profile belongs to. Ref.: Genders.GenderId\r\nMigrate: users.user_gender");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("The user last name.\r\nMigration: users.user_lastname");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("The user mobile number.\r\nMigration: users.user_mobile");

                entity.Property(e => e.Pincode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The residential address pincode.\r\nMigrate: users.user_pincode");

                entity.Property(e => e.PublicId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("The unique public identifier. These can be shared publically.\r\nMigrate: users.user_public_id");

                entity.Property(e => e.PublicIdSalt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("The secret key used to generate the public identifier.\r\nusers.user_public_id_salt");

                entity.Property(e => e.StateId).HasComment("The state identifier this profile belongs to. Ref.: State.StateId\r\nMigrate: users.user_state");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when this entry was last updated.");

                entity.Property(e => e.UserId).HasComment("The user identifier this profile belongs to. Ref.: Users.UserId");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_user_profile_city_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_user_profile_country_id");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("fk_user_profile_gender_id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("fk_user_profile_state_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_profile_user_id");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasComment("This entity stores a user and role mapping.");

                entity.Property(e => e.UserRoleId).HasComment("The unique identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.RoleId).HasComment("The role identifier this user belongs to. Ref.: Role.RoleId");

                entity.Property(e => e.UserId).HasComment("The user identifier this role belongs to. Ref.: User.UserId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_roles_role_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_roles_user_id");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.ToTable("UserSetting");

                entity.HasComment("This entity stores user settings.\r\nMigrations:\r\nReceiveUpdate < users.user_receive_update\r\nReceiveNewsletter < users.user_receive_newsletter\r\n");

                entity.Property(e => e.UserSettingId).HasComment("Unique Identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when this entry was done.");

                entity.Property(e => e.MuteAllNotifications)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, all notifications sound will be muted.");

                entity.Property(e => e.MuteComments)
                    .HasDefaultValueSql("((0))")
                    .HasComment("If 1, comments notification sound will be muted.");

                entity.Property(e => e.ReceiveNewsletter)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, user will receive newsletters from the platform.\r\nMigrate: users.user_receive_newsletter");

                entity.Property(e => e.ReceiveUpdate)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If 1, user will receive update from the platform.\r\nMigrate: users.user_receive_update");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when these settings were last updated.");

                entity.Property(e => e.UserId).HasComment("The user identifier these settings belongs to. Ref.: User.UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_settings_user_id");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.HasComment("This entity stores a list of user types.");

                entity.Property(e => e.UserTypeId)
                    .ValueGeneratedOnAdd()
                    .HasComment("Unique Identifier.");

                entity.Property(e => e.CreateDateTime).HasComment("The date and time when entry was made.");

                entity.Property(e => e.UpdateDateTime).HasComment("The date and time when entry was last updated.");

                entity.Property(e => e.UserType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserType")
                    .HasComment("The user type.\r\nPossible Values: Institution Staff, Parent, Student");
            });

            base.OnModelCreating(modelBuilder);
            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
