// Services/MonitoringGrpcClient.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using MonitoringService;             // gRPC auto‐generated namespace (monitoring.proto ichida `package monitoring;`)
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Services
{
    /// <summary>
    /// gRPC client sinfi, MonitoringServiceImpl ga murojaat qiladi.
    /// </summary>
    public class MonitoringGrpcClient
    {
        private readonly Monitoring.MonitoringClient _client;

        public MonitoringGrpcClient()
        {
            // Bu yerga MonitoringService gRPC serverning manzilini qo‘ying:
            // Masalan: VS da server ishlayotgani “Now listening on: https://localhost:5003” bo‘lsa:
            var channel = GrpcChannel.ForAddress("https://localhost:5003");
            _client = new Monitoring.MonitoringClient(channel);
        }

        public async Task<string> AddMonitoringDataAsync(int patientId, float temperature,
            int heartRate, int bpSystolic, int bpDiastolic, string notes)
        {
            var request = new MonitoringRequest
            {
                PatientId = patientId,
                Temperature = temperature,
                HeartRate = heartRate,
                BpSystolic = bpSystolic,
                BpDiastolic = bpDiastolic,
                Notes = notes
            };

            var response = await _client.AddMonitoringDataAsync(request);
            return response.Message;
        }

        public async Task<List<MonitoringData>> GetMonitoringDataAsync(int patientId)
        {
            var request = new PatientRequest { PatientId = patientId };
            var response = await _client.GetMonitoringDataAsync(request);

            // gRPC’dan qaytgan MonitoringRecord’larni lokal MonitoringData tipiga map qilamiz
            var result = response.Records.Select(r => new MonitoringData
            {
                Id = 0,  // yoki biror boshqa logika bo‘yicha
                PatientId = patientId,
                Temperature = r.Temperature,
                HeartRate = r.HeartRate,
                BpSystolic = r.BpSystolic,
                BpDiastolic = r.BpDiastolic,
                Notes = r.Notes,
                Timestamp = DateTime.Parse(r.Timestamp)  // string → DateTime
            }).ToList();

            return result;
        }
    }
}
