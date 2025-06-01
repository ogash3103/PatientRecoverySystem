// MonitoringService/Services/MonitoringServiceImpl.cs
using Grpc.Core;
using MonitoringService;  // gRPC orqali avval Generate qilingan namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringService.Services
{
    public class MonitoringServiceImpl : Monitoring.MonitoringBase
    {
        // Misol uchun, oddiy roʻyxatda saqlaymiz
        private static readonly List<(int patientId, MonitoringRecord record)> _data = new();

        public override Task<MonitoringReply> AddMonitoringData(
            MonitoringRequest request,
            ServerCallContext context)
        {
            var record = new MonitoringRecord
            {
                Temperature = request.Temperature,
                HeartRate = request.HeartRate,
                BpSystolic = request.BpSystolic,
                BpDiastolic = request.BpDiastolic,
                Notes = request.Notes,
                Timestamp = DateTime.UtcNow.ToString("u")
            };

            _data.Add((request.PatientId, record));

            return Task.FromResult(new MonitoringReply
            {
                Message = "Monitoring data added successfully."
            });
        }

        public override Task<MonitoringListReply> GetMonitoringData(
            PatientRequest request,
            ServerCallContext context)
        {
            var records = _data
                .Where(d => d.patientId == request.PatientId)
                .Select(d => d.record)
                .ToList();

            var reply = new MonitoringListReply();
            reply.Records.AddRange(records);

            return Task.FromResult(reply);
        }
    }
}
