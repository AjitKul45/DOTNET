using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CS_Assessment0._1.Models
{
    public partial class A01Context : DbContext
    {
        public A01Context()
        {
        }

        public A01Context(DbContextOptions<A01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Consultant> Consultants { get; set; } = null!;
        public virtual DbSet<Investment> Investments { get; set; } = null!;
        public virtual DbSet<Leave> Leaves { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public virtual DbSet<SalarySlip> SalarySlips { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=A0.1; User Id = sa ; Password = abcd@1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_name");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PK__Consulta__A9FDEC322A3F92B5");

                entity.ToTable("Consultant");

                entity.Property(e => e.CId).HasColumnName("C_Id");

                entity.Property(e => e.DaysWorked).HasColumnName("Days_worked");

                entity.Property(e => e.ExtraHoursWorked).HasColumnName("Extra_hours_worked");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.FixedPay).HasColumnName("Fixed_pay");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.NoDays).HasColumnName("No_days");

                entity.Property(e => e.PerHourPay).HasColumnName("Per_Hour_pay");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Consultants)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Consultan__User___71D1E811");
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("Investment");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.InsuranceAmount).HasColumnName("Insurance_amount");

                entity.Property(e => e.PfAmount).HasColumnName("PF_amount");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Investmen__user___693CA210");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.ToTable("leave");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("leave_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__leave__user_id__6EF57B66");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("salary");

                entity.Property(e => e.BasicPay).HasColumnName("basic_pay");

                entity.Property(e => e.Da).HasColumnName("DA");

                entity.Property(e => e.Hra).HasColumnName("HRA");

                entity.Property(e => e.Overtime).HasColumnName("overtime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Ta).HasColumnName("TA");

                entity.Property(e => e.Tds).HasColumnName("TDS");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.WeekendWorked).HasColumnName("weekend_worked");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__salary__User_id__6C190EBB");
            });

            modelBuilder.Entity<SalarySlip>(entity =>
            {
                entity.HasKey(e => e.SlipId)
                    .HasName("PK__salary_s__43C7142201FFD97C");

                entity.ToTable("salary_slip");

                entity.Property(e => e.SlipId).HasColumnName("slip_id");

                entity.Property(e => e.Contribution).HasColumnName("contribution");

                entity.Property(e => e.DateFrom)
                    .HasColumnType("date")
                    .HasColumnName("date_from");

                entity.Property(e => e.DateTo)
                    .HasColumnType("date")
                    .HasColumnName("date_to");

                entity.Property(e => e.Deduction).HasColumnName("deduction");

                entity.Property(e => e.NetAmount).HasColumnName("net_amount");

                entity.Property(e => e.TotalEarnings).HasColumnName("total_earnings");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__salary_sl__user___74AE54BC");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User_inf__B9BE370F7B4E7F4A");

                entity.ToTable("User_info");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AccNo).HasColumnName("Acc_no");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .HasColumnName("id");

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("date")
                    .HasColumnName("joining_date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.UserInfos)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__User_info__id__5FB337D6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
