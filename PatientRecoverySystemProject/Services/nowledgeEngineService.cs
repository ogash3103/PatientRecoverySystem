
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Services
{
    public class KnowledgeEngineService
    {
        public string GenerateAdvice(List<Symptom> symptoms, List<MonitoringData> monitoringData)
        {
            if (symptoms.Any(s => s.Description.ToLower().Contains("chest pain")))
                return "Immediate attention required: Possible heart issue.";

            var latest = monitoringData.OrderByDescending(m => m.Timestamp).FirstOrDefault();

            if (latest != null)
            {
                if (latest.Temperature > 38)
                    return "High temperature detected. Recommend cooling and hydration.";

                if (latest.HeartRate > 110)
                    return "High heart rate detected. Recommend resting and monitoring.";
            }

            return "All normal. Continue monitoring and follow up if symptoms persist.";
        }
    }
}
