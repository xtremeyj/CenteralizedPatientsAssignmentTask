using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CenteralizedPatientsAssignmentTask.Infrastructure
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly CenteralizedPatientsAssignmentTaskDbContext _context;

        public AnalyticsRepository(CenteralizedPatientsAssignmentTaskDbContext context) => _context = context;

        public async Task<IEnumerable<AnalyticsDto>> GetAnalyticsDataAsync()
        {
            // Call stored proc
            return await _context.AnalyticsDtos
              .FromSqlRaw("EXEC sp_GetAnalyticsData")
              .ToListAsync();
        }

        public async Task<IEnumerable<MonthlyAdmissionsDto>> GetMonthlyAdmissionsAsync()
        {
            return await _context.MonthlyAdmissions
                .FromSqlRaw("EXEC sp_GetMonthlyAdmissions")
                .ToListAsync();
        }

    }
}
