// DiagnoseService/Services/DiagnoseServiceImpl.cs
using Grpc.Core;
using DiagnoseService;      // bu – gRPC orqali avval Generate qilingan namspace
using System.Threading.Tasks;

namespace DiagnoseService.Services
{
    public class DiagnoseServiceImpl : Diagnose.DiagnoseBase
    {
        public override Task<DiagnosisReply> GetRecommendation(
            DiagnosisRequest request,
            ServerCallContext context)
        {
            var recommendation = "Symptom unclear. Please consult a doctor.";
            var text = request.SymptomText.ToLower() ?? string.Empty;

            if (text.Contains("pain"))
                recommendation = "Use mild painkillers and rest.";
            else if (text.Contains("fever"))
                recommendation = "Take paracetamol and hydrate.";
            else if (text.Contains("headache"))
                recommendation = "Avoid screen time and rest.";

            return Task.FromResult(new DiagnosisReply
            {
                Recommendation = recommendation
            });
        }
    }
}
