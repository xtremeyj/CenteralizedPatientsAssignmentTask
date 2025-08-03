using CenteralizedPatientsAssignmentTask.API.Hubs;
using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Common.Interface.Service;
using CenteralizedPatientsAssignmentTask.Interface.Service;
using CenteralizedPatientsAssignmentTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CenteralizedPatientsAssignmentTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAnalyticsService _analyticsService;
        private readonly IHubContext<PatientHub> _hub;

        public PatientController(IPatientService patientService,
            IAnalyticsService analyticsService, 
            IHubContext<PatientHub> hub)
        {
            _patientService = patientService; 
            _analyticsService = analyticsService;
            _hub = hub;
        }

        // GET: api/patient 
        [HttpGet]
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> GetPatients([FromQuery] PatientQueryParameters query)
        {
            var result = await _patientService.GetPaginatedFromSpAsync(query);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return Ok(patient);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            patient.PatientId = Guid.NewGuid();
            var created = await _patientService.CreateAsync(patient);

            var hospitals = await _analyticsService.GetHospitalAnalyticsAsync();
            var monthly = await _analyticsService.GetMonthlyAdmissionsAsync();

            await _hub.Clients.All.SendAsync("ReceiveChartData", new
            {
                hospitals,
                monthly
            });
            return Ok(created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] Patient updated)
        {
            var success = await _patientService.UpdateAsync(id, updated);
            return Ok(success);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var success = await _patientService.DeleteAsync(id);
            return Ok(success);
        }
    }
}
