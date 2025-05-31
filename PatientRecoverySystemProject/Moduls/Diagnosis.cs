namespace PatientRecoverySystemProject.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public DateTime DateDiagnosed { get; set; } = DateTime.UtcNow;
    }
}
