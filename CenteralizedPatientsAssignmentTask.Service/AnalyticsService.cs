using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Common.Interface.Repositories;
using CenteralizedPatientsAssignmentTask.Common.Interface.Service;

namespace CenteralizedPatientsAssignmentTask.Service
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _repo;

        public AnalyticsService(IAnalyticsRepository repo) => _repo = repo;

        public Task<IEnumerable<AnalyticsDto>> GetHospitalAnalyticsAsync() =>
          _repo.GetAnalyticsDataAsync();

        public Task<IEnumerable<MonthlyAdmissionsDto>> GetMonthlyAdmissionsAsync()
       => _repo.GetMonthlyAdmissionsAsync();
    }
}
