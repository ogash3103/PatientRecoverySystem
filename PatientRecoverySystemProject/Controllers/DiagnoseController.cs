using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Models;
using PatientRecoverySystemProject.Services;  // ← endi DiagnosisService shu namespace ichida

namespace PatientRecoverySystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnoseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DiagnosisService _diagnosisService;

        // DI orqali ApplicationDbContext va DiagnosisService inject qilinadi
        public DiagnoseController(
            ApplicationDbContext context,
            DiagnosisService diagnosisService)
        {
            _context = context;
            _diagnosisService = diagnosisService;
        }

        /// <summary>
        /// POST: /api/diagnose/{patientId}
        /// </summary>
        [HttpPost("{patientId}")]
        public async Task<ActionResult<Diagnosis>> DiagnosePatient(int patientId)
        {
            var symptoms = await _context.Symptoms
                .Where(s => s.PatientId == patientId)
                .ToListAsync();

            if (!symptoms.Any())
                return BadRequest($"No symptoms found for patientId = {patientId}.");

            var recommendation = _diagnosisService.GetRecommendation(symptoms);

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
