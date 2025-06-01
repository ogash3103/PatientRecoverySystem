// Models/MonitoringData.cs

using System;

namespace PatientRecoverySystemProject.Models
{
    public class MonitoringData
    {
        public int Id { get; internal set; }
        public int PatientId { get; set; }
        public float Temperature { get; set; }
        public int HeartRate { get; set; }
      
        public string Notes { get; set; } = string.Empty;

        public DateTime Timestamp { get;  set; } =DateTime.UtcNow; // UTC vaqtni saqlaymiz, serverda ishlash uchun qulay

        public Patient Patient { get; set; }  // Navigation property, Patient modeliga bog‘lanadi
        public int BpSystolic { get; set; }
        public int BpDiastolic { get;  set; }
    }
}
