using CenteralizedPatientsAssignmentTask.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenteralizedPatientsAssignmentTask.Common.Interface.Service
{
    public interface IAnalyticsService
    {
        Task<IEnumerable<AnalyticsDto>> GetHospitalAnalyticsAsync();
        Task<IEnumerable<MonthlyAdmissionsDto>> GetMonthlyAdmissionsAsync();
    }
}
