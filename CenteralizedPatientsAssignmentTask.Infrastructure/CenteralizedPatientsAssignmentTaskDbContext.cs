using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CenteralizedPatientsAssignmentTask.Infrastructure
{
    public class CenteralizedPatientsAssignmentTaskDbContext : DbContext
    {
        public CenteralizedPatientsAssignmentTaskDbContext(DbContextOptions<CenteralizedPatientsAssignmentTaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientResultDto> PatientsResultDto { get; set; }
        public DbSet<AnalyticsDto> AnalyticsDtos { get; set; }
        public DbSet<MonthlyAdmissionsDto> MonthlyAdmissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.ContactNo).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.HospitalName).HasMaxLength(100);
                entity.Property(e => e.Diagnosis).HasMaxLength(200);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            });

            modelBuilder.Entity<PatientResultDto>().HasNoKey();
            modelBuilder.Entity<AnalyticsDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<MonthlyAdmissionsDto>().HasNoKey().ToView(null);
        }
    }
}
