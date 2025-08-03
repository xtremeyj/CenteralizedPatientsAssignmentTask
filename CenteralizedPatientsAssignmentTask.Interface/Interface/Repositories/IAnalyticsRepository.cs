using CenteralizedPatientsAssignmentTask.Common.DTOs;

namespace CenteralizedPatientsAssignmentTask.Common.Interface.Repositories
{
    public interface IAnalyticsRepository
    {
        Task<IEnumerable<AnalyticsDto>> GetAnalyticsDataAsync();
        Task<IEnumerable<MonthlyAdmissionsDto>> GetMonthlyAdmissionsAsync();
    }
}
