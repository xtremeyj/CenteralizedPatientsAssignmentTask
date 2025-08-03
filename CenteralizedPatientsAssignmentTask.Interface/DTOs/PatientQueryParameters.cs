namespace CenteralizedPatientsAssignmentTask.Common.DTOs
{
    public class PatientQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc";
        public string? Hospital { get; set; }
        public string? Status { get; set; }
        public string? Gender { get; set; }
    }
}
