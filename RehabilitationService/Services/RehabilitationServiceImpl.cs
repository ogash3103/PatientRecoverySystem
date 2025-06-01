using Grpc.Core;
using RehabilitationService;  // ⚠️ bu yerda “rehab” emas, "RehabilitationService" bo‘lsin
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RehabilitationService.Services
{
    public class RehabilitationServiceImpl : Rehabilitation.RehabilitationBase
    {
        private static readonly List<(int PatientId, RehabRecord Record)> _rehabData = new();

        public override Task<RehabReply> AddRehabData(RehabRequest request, ServerCallContext context)
        {
            var record = new RehabRecord
            {
                Exercise = request.Exercise,
                Feedback = request.Feedback,
                PainLevel = request.PainLevel,
                Timestamp = DateTime.UtcNow.ToString("u")
            };
            _rehabData.Add((request.PatientId, record));

            return Task.FromResult(new RehabReply
            {
                Message = "Rehabilitation data added successfully."
            });
        }

        public override Task<RehabListReply> GetRehabData(PatientRequest request, ServerCallContext context)
        {
            var reply = new RehabListReply();
            var records = _rehabData
                .Where(x => x.PatientId == request.PatientId)
                .Select(x => x.Record)
                .ToList();

            reply.Records.AddRange(records);
            return Task.FromResult(reply);
        }
    }
}
