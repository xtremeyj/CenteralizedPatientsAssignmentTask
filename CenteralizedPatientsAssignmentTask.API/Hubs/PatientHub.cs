using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Common.Interface.Service;
using Microsoft.AspNetCore.SignalR;

namespace CenteralizedPatientsAssignmentTask.API.Hubs
{
    public class PatientHub : Hub
    {
        private readonly IAnalyticsService _analyticsService;

        public PatientHub(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public override async Task OnConnectedAsync()
        {
            var hospitals = await _analyticsService.GetHospitalAnalyticsAsync();
            var monthly = await _analyticsService.GetMonthlyAdmissionsAsync();

            await Clients.Caller.SendAsync("ReceiveChartData", new
            {
                hospitals,
                monthly
            });

            await base.OnConnectedAsync();
        }
        public async Task BroadcastCharts(IEnumerable<AnalyticsDto> hospitalData, IEnumerable<MonthlyAdmissionsDto> monthlyData)
        {
            await Clients.All.SendAsync("ReceiveChartData", new
            {
                hospitals = hospitalData,
                monthly = monthlyData
            });
        }
    }
}
