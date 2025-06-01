using Grpc.Net.Client;
using RehabilitationService;  // ← avto‐generate qilingan sinflar shu namespace ichida

namespace PatientRecoverySystemProject.Services
{
    public class RehabilitationGrpcClient
    {
        private readonly Rehabilitation.RehabilitationClient _client;

        public RehabilitationGrpcClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            _client = new Rehabilitation.RehabilitationClient(channel);
        }

        public async Task<string> AddRehabDataAsync(int patientId, string exercise, string feedback, int painLevel)
        {
            var request = new RehabRequest
            {
                PatientId = patientId,
                Exercise = exercise,
                Feedback = feedback,
                PainLevel = painLevel
            };
            var response = await _client.AddRehabDataAsync(request);
            return response.Message;
        }

        public async Task<List<RehabRecord>> GetRehabDataAsync(int patientId)
        {
            var request = new PatientRequest { PatientId = patientId };
            var response = await _client.GetRehabDataAsync(request);
            return response.Records.ToList();
        }
    }
}
