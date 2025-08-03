namespace CenteralizedPatientsAssignmentTask.Common.DTOs
{
    public class AnalyticsDto
    {
        public string HospitalName { get; set; }
        public int PatientCount { get; set; }

    }

    public class MonthlyAdmissionsDto
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }

}
