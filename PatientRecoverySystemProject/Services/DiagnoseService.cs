using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Services
{
    public class DiagnoseService
    {
        // AI yoki qo‘lda yozilgan bilimga asoslangan soddalashtirilgan tavsiya
        public string GetRecommendation(List<Symptom> symptoms)
        {
            if (symptoms.Any(s => s.Description.Contains("pain", StringComparison.OrdinalIgnoreCase)))
                return "Recommend painkiller and rest.";

            if (symptoms.Any(s => s.Description.Contains("fever", StringComparison.OrdinalIgnoreCase)))
                return "Recommend paracetamol and hydration.";

            return "Consult further for unknown symptoms.";
        }
    }
}
