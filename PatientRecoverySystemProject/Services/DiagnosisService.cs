﻿// File: PatientRecoverySystemProject/Services/DiagnosisService.cs
using System.Collections.Generic;
using System.Linq;
using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Services
{
    /// <summary>
    /// Simptomlar ro‘yxatiga asoslangan oddiy diagnostika xizmati.
    /// </summary>
    public class DiagnosisService
    {
        public string GetRecommendation(List<Symptom> symptoms)
        {
            if (symptoms.Any(s =>
                    s.Description.Contains("pain", System.StringComparison.OrdinalIgnoreCase)))
            {
                return "Use mild painkillers and rest.";
            }

            if (symptoms.Any(s =>
                    s.Description.Contains("fever", System.StringComparison.OrdinalIgnoreCase)))
            {
                return "Take paracetamol and hydrate.";
            }

            if (symptoms.Any(s =>
                    s.Description.Contains("headache", System.StringComparison.OrdinalIgnoreCase)))
            {
                return "Avoid screen time and rest.";
            }

            return "Consult further for unknown symptoms.";
        }
    }
}
