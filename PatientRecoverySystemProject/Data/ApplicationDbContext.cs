using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        // Keyinroq: Symptoms, Diagnosis, MonitoringData, etc.
    }
}
