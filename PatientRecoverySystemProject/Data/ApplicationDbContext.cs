// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<MonitoringData> MonitoringData { get; set; }
        public DbSet<RehabilitationData> RehabilitationDatas { get; set; }
    }
}
