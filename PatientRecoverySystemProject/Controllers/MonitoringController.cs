using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MonitoringController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ POST: /api/Monitoring
        [HttpPost]
        public async Task<ActionResult<MonitoringData>> PostMonitoringData(MonitoringData data)
        {
            _context.MonitoringData.Add(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMonitoringData), new { patientId = data.PatientId }, data);
        }

        // ✅ GET: /api/Monitoring/{patientId}
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<MonitoringData>>> GetMonitoringData(int patientId)
        {
            var monitoring = await _context.MonitoringData
                .Where(md => md.PatientId == patientId)
                .AsNoTracking()
                .ToListAsync();

            return Ok(monitoring);
        }
    }
}
