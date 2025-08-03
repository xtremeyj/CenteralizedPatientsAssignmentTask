using CenteralizedPatientsAssignmentTask.Common.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CenteralizedPatientsAssignmentTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetInitialAnalytics()
        {
            var hospitals = await _analyticsService.GetHospitalAnalyticsAsync();
            var monthly = await _analyticsService.GetMonthlyAdmissionsAsync();
            return Ok(new { hospitals, monthly });
        }
    }
}
