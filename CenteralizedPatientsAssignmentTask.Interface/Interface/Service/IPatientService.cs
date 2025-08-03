using CenteralizedPatientsAssignmentTask.Common;
using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Models;

namespace CenteralizedPatientsAssignmentTask.Interface.Service
{
    public interface IPatientService
    {
        Task<PaginatedResult<PatientResultDto>> GetPaginatedFromSpAsync(PatientQueryParameters query);
        Task<Patient?> GetByIdAsync(Guid id);
        Task<Patient> CreateAsync(Patient patient);
        Task<bool> UpdateAsync(Guid id, Patient updatedPatient);
        Task<bool> DeleteAsync(Guid id);
    }
}
