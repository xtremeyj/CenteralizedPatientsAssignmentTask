namespace CenteralizedPatientsAssignmentTask.Common
{
    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> PageData { get; set; } = new List<T>();
    }
}
