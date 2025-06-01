// Controllers/MonitoringController.cs

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// Quyidagi using’lar muhim, ularni yuqorida qo‘shing:
using PatientRecoverySystemProject.Models;    // MonitoringDto, MonitoringData uchun
using PatientRecoverySystemProject.Services;  // MonitoringGrpcClient uchun

namespace PatientRecoverySystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly MonitoringGrpcClient _grpcClient;

        // DI orqali MonitoringGrpcClient inject qilinadi:
        public MonitoringController(MonitoringGrpcClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        /// <summary>
        /// POST: /api/monitoring
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostMonitoringData([FromBody] MonitoringData data)
        {
            if (data == null)
            {
                return BadRequest("Monitoring data is required.");
            }

            // gRPC client yordamida serverga yozuv yuboramiz
            var result = await _grpcClient.AddMonitoringDataAsync(
                data.PatientId,
                data.Temperature,
                data.HeartRate,
                data.BpSystolic,
                data.BpDiastolic,
                data.Notes
            );

            return Ok(new { message = result });
        }

        /// <summary>
        /// GET: /api/monitoring/{patientId}
        /// </summary>
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetMonitoringData(int patientId)
        {
            if (patientId <= 0)
                return BadRequest("Invalid patientId.");

            var records = await _grpcClient.GetMonitoringDataAsync(patientId);
            return Ok(records);
        }
    }
}
