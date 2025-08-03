using CenteralizedPatientsAssignmentTask.Common;
using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Models;

namespace CenteralizedPatientsAssignmentTask.Interface.Repositories
{
    public interface IPatientRepository
    {
        Task<PaginatedResult<PatientResultDto>> GetPatientsFromSpAsync(PatientQueryParameters query);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task<Patient?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
