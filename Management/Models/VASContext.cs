using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Management.Models
{
    public partial class VASContext : DbContext
    {
        public virtual DbSet<Cutomers> Cutomers { get; set; }
        public virtual DbSet<SentSms> SentSms { get; set; }
        public virtual DbSet<ShoortNumber> ShoortNumber { get; set; }
        public virtual DbSet<ShoortNumberActions> ShoortNumberActions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=guj-devdb-t01;database=VAS;uid=alm_nid;pwd=alm_nid;");
            }
        }

        public VASContext(DbContextOptions<VASContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cutomers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(25);
            });

            modelBuilder.Entity<SentSms>(entity =>
            {
                entity.ToTable("SentSMS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ShoortNumber>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.Service).HasMaxLength(150);

                entity.Property(e => e.Smscount).HasColumnName("SMSCount");

                entity.Property(e => e.State).HasDefaultValueSql("((0))");

                entity.Property(e => e.To).HasColumnType("datetime");

                entity.Property(e => e.UsageSms).HasColumnName("UsageSMS");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShoortNumber)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ShoortNumber_Cutomers");
            });

            modelBuilder.Entity<ShoortNumberActions>(entity =>
            {
                entity.Property(e => e.ActionDescription).HasMaxLength(100);

                entity.Property(e => e.CreatecdOn).HasColumnType("datetime");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.Smscount).HasColumnName("SMSCount");

                entity.Property(e => e.To).HasColumnType("datetime");

                entity.HasOne(d => d.ShoortNumber)
                    .WithMany(p => p.ShoortNumberActions)
                    .HasForeignKey(d => d.ShoortNumberId)
                    .HasConstraintName("FK_ShoortNumberActions_ShoortNumber");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.LoginTryAttemptDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.State).HasDefaultValueSql("((0))");
            });
        }
    }
}
