namespace PatientRecoverySystemProject.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
    }
}
