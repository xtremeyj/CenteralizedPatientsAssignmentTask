namespace CenteralizedPatientsAssignmentTask.Models
{
    public class Patient
    {
        public Guid? PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string HospitalName { get; set; }
        public string Diagnosis { get; set; }
        public string Status { get; set; } // Active / Discharged / Deceased
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
