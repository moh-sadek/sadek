using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public partial class CSORContext : DbContext
    {
        public virtual DbSet<Centers> Centers { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }

        public CSORContext(DbContextOptions<CSORContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=DESKTOP-RNT1C11;database=CSOR;uid=Ahmed;pwd=35087124567Ahmed;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Centers>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Organizations>(entity =>
            {
                entity.Property(e => e.CapacityOtherAreas).HasMaxLength(250);

                entity.Property(e => e.CoalitionDescription).HasMaxLength(550);

                entity.Property(e => e.CooperatDescription).HasMaxLength(550);

                entity.Property(e => e.CooperatFieldOther).HasMaxLength(550);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DallLibya).HasColumnName("DAllLibya");

                entity.Property(e => e.Deast).HasColumnName("DEast");

                entity.Property(e => e.Dsouth).HasColumnName("DSouth");

                entity.Property(e => e.Dwest).HasColumnName("DWest");

                entity.Property(e => e.ElectoralCommission).HasMaxLength(550);

                entity.Property(e => e.ExperiencesLessons).HasMaxLength(250);

                entity.Property(e => e.IsCompletedProjectDesc).HasMaxLength(550);

                entity.Property(e => e.Issues).HasMaxLength(550);

                entity.Property(e => e.ManagingDirectorExPhone).HasMaxLength(50);

                entity.Property(e => e.ManagingDirectorName).HasMaxLength(50);

                entity.Property(e => e.ManagingDirectorPhone).HasMaxLength(50);

                entity.Property(e => e.MembersFcount).HasColumnName("MembersFCount");

                entity.Property(e => e.MembersMcount).HasColumnName("MembersMCount");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.OtherCapabilities).HasMaxLength(250);

                entity.Property(e => e.OtherContent).HasMaxLength(250);

                entity.Property(e => e.OtherContentS).HasMaxLength(250);

                entity.Property(e => e.OtherFinancing).HasMaxLength(250);

                entity.Property(e => e.ParticipatedElections).HasMaxLength(550);

                entity.Property(e => e.PresidentEmail).HasMaxLength(50);

                entity.Property(e => e.PresidentName).HasMaxLength(50);

                entity.Property(e => e.SharedProjectsName).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TraningIssues).HasMaxLength(550);

                entity.Property(e => e.TraningPlace).HasMaxLength(550);

                entity.HasOne(d => d.Center)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_Organizations_Centers");
            });
        }
    }
}
