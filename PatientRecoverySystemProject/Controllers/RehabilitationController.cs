using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RehabilitationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RehabilitationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/rehabilitation/1
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<RehabilitationData>>> GetRehabData(int patientId)
        {
            var data = await _context.RehabilitationData
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();

            return Ok(data);
        }

        // POST: api/rehabilitation
        [HttpPost]
        public async Task<ActionResult<RehabilitationData>> CreateRehabData(RehabilitationData data)
        {
            _context.RehabilitationData.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRehabData), new { patientId = data.PatientId }, data);
        }
    }
}
