using Microsoft.AspNetCore.Mvc;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymptomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SymptomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddSymptom([FromBody] Symptom symptom)
        {
            _context.Symptoms.Add(symptom);
            await _context.SaveChangesAsync();
            return Ok(symptom);
        }
    }
}
