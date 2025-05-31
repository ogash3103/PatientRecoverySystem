using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Models;
using PatientRecoverySystemProject.Services;

namespace PatientRecoverySystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnoseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DiagnoseService _diagnoseService;

        public DiagnoseController(ApplicationDbContext context)
        {
            _context = context;
            _diagnoseService = new DiagnoseService();
        }

        [HttpPost("{patientId}")]
        public async Task<ActionResult<Diagnosis>> DiagnosePatient(int patientId)
        {
            var symptoms = await _context.Symptoms
                .Where(s => s.PatientId == patientId)
                .ToListAsync();

            if (symptoms.Count == 0)
                return BadRequest("No symptoms found for this patient.");

            var recommendation = _diagnoseService.GetRecommendation(symptoms);

            var diagnosis = new Diagnosis
            {
                PatientId = patientId,
                Condition = "Auto-generated based on symptoms",
                Recommendation = recommendation
            };

            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();

            return Ok(diagnosis);
        }
    }
}
