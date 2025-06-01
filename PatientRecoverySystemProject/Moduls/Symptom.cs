namespace PatientRecoverySystemProject.Models
{
    /// <summary>
    /// Simptom maʼlumotlarini saqlash uchun model.
    /// </summary>
    public class Symptom
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; } = string.Empty;
        // Boshqa kerakli maydonlar bo'lishi mumkin (Timestamp, Severity va hokazo)
    }
}
