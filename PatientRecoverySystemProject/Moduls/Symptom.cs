// (Masalan)
namespace PatientRecoverySystemProject.Models
{
    public class Symptom
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DateRecorded { get; set; } = DateTime.UtcNow;
    }
}
