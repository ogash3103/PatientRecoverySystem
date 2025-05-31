using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Services;

namespace PatientRecoverySystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly KnowledgeEngineService _knowledgeEngine = new();
        private readonly AlertService _alertService = new();

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/report/1
        [HttpGet("{patientId}")]
        public async Task<ActionResult<object>> GetPatientReport(int patientId)
        {
            var symptoms = await _context.Symptoms.Where(s => s.PatientId == patientId).ToListAsync();
            var monitoring = await _context.MonitoringData.Where(m => m.PatientId == patientId).ToListAsync();
            var lastMonitor = monitoring.OrderByDescending(m => m.Timestamp).FirstOrDefault();

            var advice = _knowledgeEngine.GenerateAdvice(symptoms, monitoring);

            string? alertMessage = null;
            if (lastMonitor != null && _alertService.IsEmergency(lastMonitor))
                alertMessage = _alertService.GetAlertMessage(lastMonitor);

            return Ok(new
            {
                patientId,
                lastMonitoring = lastMonitor,
                advice,
                emergency = alertMessage
            });
        }
    }
}
