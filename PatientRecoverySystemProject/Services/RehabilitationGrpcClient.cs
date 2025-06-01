using Grpc.Net.Client;
using MonitoringService;
using rehab;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecoverySystemProject.Services
{
    public class RehabilitationGrpcClient
    {
        private readonly Rehabilitation.RehabilitationClient _client;

        public RehabilitationGrpcClient()
        {
            // E’tibor: manzil va portni o‘zingizki sozlang 
            // (RehabilitationService loyihangiz run bo‘ladigan portni yozing)
            var channel = GrpcChannel.ForAddress("https://localhost:7023");
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
