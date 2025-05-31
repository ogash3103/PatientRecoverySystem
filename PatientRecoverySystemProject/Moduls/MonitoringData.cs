namespace PatientRecoverySystemProject.Models
{
    public class MonitoringData
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public float Temperature { get; set; }
        public int HeartRate { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // optional
        public Patient? Patient { get; set; }
    }
}
