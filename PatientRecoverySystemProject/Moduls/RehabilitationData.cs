using System;

namespace PatientRecoverySystemProject.Models
{
    public class RehabilitationData
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public string Exercise { get; set; } = string.Empty;
        public string Feedback { get; set; } = string.Empty;
        public int PainLevel { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Patient? Patient { get; set; }
    }

}
