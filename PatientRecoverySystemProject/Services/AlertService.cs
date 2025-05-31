using PatientRecoverySystemProject.Models;

namespace PatientRecoverySystemProject.Services
{
    public class AlertService
    {
        public bool IsEmergency(MonitoringData data)
        {
            if (data.Temperature >= 39 || data.HeartRate >= 130 ||
                data.BloodPressureSystolic >= 180 || data.BloodPressureDiastolic >= 120)
                return true;

            return false;
        }

        public string GetAlertMessage(MonitoringData data)
        {
            return $"⚠️ EMERGENCY DETECTED for Patient {data.PatientId} - Temp: {data.Temperature}, HR: {data.HeartRate}, BP: {data.BloodPressureSystolic}/{data.BloodPressureDiastolic}";
        }
    }
}
