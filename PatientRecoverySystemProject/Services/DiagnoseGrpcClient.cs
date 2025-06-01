using Grpc.Net.Client;
using DiagnoseService;            // Proto’dan generatsiya qilingan namespace
using System.Threading.Tasks;

namespace PatientRecoverySystemProject.Services
{
    public class DiagnoseGrpcClient
    {
        private readonly Diagnose.DiagnoseClient _client;

        public DiagnoseGrpcClient()
        {
            // “DiagnoseService” gRPC serveriga ulanish
            var channel = GrpcChannel.ForAddress("https://localhost:7167");
            _client = new Diagnose.DiagnoseClient(channel);
        }

        public async Task<string> GetRecommendationAsync(string symptomText)
        {
            var request = new DiagnosisRequest
            {
                SymptomText = symptomText
            };
            var reply = await _client.GetRecommendationAsync(request);
            return reply.Recommendation;
        }
    }
}
