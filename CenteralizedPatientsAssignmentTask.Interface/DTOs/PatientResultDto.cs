namespace CenteralizedPatientsAssignmentTask.Common.DTOs
{
    public class PatientResultDto
    {
        public int TotalCount { get; set; }
        public Guid PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
