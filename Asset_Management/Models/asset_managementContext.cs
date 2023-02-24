using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Asset_Management.Models
{
    public partial class asset_managementContext : DbContext
    {
        public asset_managementContext()
        {
        }

        public asset_managementContext(DbContextOptions<asset_managementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssetDetail> AssetDetails { get; set; } = null!;
        public virtual DbSet<AssetTransaction> AssetTransactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /* if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=asset_management; user id=sa; password=abcd@1234");
            }
           */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetDetail>(entity =>
            {
                entity.ToTable("asset_details");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Configuration)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("configuration");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("expiry_date");

                entity.Property(e => e.HostName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("host_name");

                entity.Property(e => e.Model)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Oem)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("OEM");

                entity.Property(e => e.Owner)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("owner");

                entity.Property(e => e.Proprietary)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("proprietary");

                entity.Property(e => e.Ram)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RAM");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.ServiceTag)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("service_tag");

                entity.Property(e => e.Tyape)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tyape");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.AssetDetails)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK__asset_det__vendo__32E0915F");
            });

            modelBuilder.Entity<AssetTransaction>(entity =>
            {
                entity.ToTable("asset_transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.Department)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EmpId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("emp_id");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issue_date");

                entity.Property(e => e.IssuedBy)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("issued_by");

                entity.Property(e => e.Location)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.SubmitDate)
                    .HasColumnType("date")
                    .HasColumnName("submit_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetTransactions)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK__asset_tra__asset__35BCFE0A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssetTransactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__asset_tra__user___36B12243");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contact_no");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("date")
                    .HasColumnName("registration_date");

                entity.Property(e => e.TerminationDate)
                    .HasColumnType("date")
                    .HasColumnName("termination_date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
