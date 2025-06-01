using Microsoft.AspNetCore.Mvc;
using PatientRecoverySystemProject.Services;  // MonitoringGrpcClient va RehabilitationGrpcClient shu papkada joylashadi
using rehab;                                  // bu namespace .proto fayldan avtomatik yaratiladi
using System.Threading.Tasks;

namespace PatientRecoverySystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RehabilitationController : ControllerBase
    {
        private readonly RehabilitationGrpcClient _grpcClient;

        public RehabilitationController(RehabilitationGrpcClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        // POST: /api/rehabilitation
        [HttpPost]
        public async Task<IActionResult> PostRehabData([FromBody] RehabRequest data)
        {
            var result = await _grpcClient.AddRehabDataAsync(
                data.PatientId,
                data.Exercise,
                data.Feedback,
                data.PainLevel
            );
            return Ok(new { message = result });
        }

        // GET: /api/rehabilitation/{patientId}
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRehabData(int patientId)
        {
            var records = await _grpcClient.GetRehabDataAsync(patientId);
            return Ok(records);
        }
    }
}
