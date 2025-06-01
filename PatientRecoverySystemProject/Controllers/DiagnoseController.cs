// File: PatientRecoverySystemProject/Controllers/DiagnoseController.cs
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
        private readonly DiagnosisService _diagnosisService;

        public DiagnoseController(
            ApplicationDbContext context,
            DiagnosisService diagnosisService)
        {
            _context = context;
            _diagnosisService = diagnosisService ?? throw new ArgumentNullException(nameof(diagnosisService));
        }

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
                Condition = "Auto‐generated based on symptoms",
                Recommendation = recommendation
            };

            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();

            return Ok(diagnosis);
        }

        [HttpGet]
        public ActionResult<string> GetByText([FromQuery] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("text parameter is required.");

            var fakeSymptoms = new List<Symptom> { new Symptom { Description = text } };
            var recommendation = _diagnosisService.GetRecommendation(fakeSymptoms);

            return Ok(recommendation);
        }
    }
}
