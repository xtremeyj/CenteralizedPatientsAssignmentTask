using CenteralizedPatientsAssignmentTask.Common;
using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Interface.Repositories;
using CenteralizedPatientsAssignmentTask.Interface.Service;
using CenteralizedPatientsAssignmentTask.Models;

namespace CenteralizedPatientsAssignmentTask.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PaginatedResult<PatientResultDto>> GetPaginatedFromSpAsync(PatientQueryParameters query)
        {
            return await _patientRepository.GetPatientsFromSpAsync(query);
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            patient.PatientId = Guid.NewGuid();
            patient.CreatedDate = DateTime.UtcNow;

            await _patientRepository.AddAsync(patient);
            return patient;
        }

        public async Task<bool> UpdateAsync(Guid id, Patient updatedPatient)
        {
            var existing = await _patientRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.Name = updatedPatient.Name;
            existing.Age = updatedPatient.Age;
            existing.Gender = updatedPatient.Gender;
            existing.ContactNo = updatedPatient.ContactNo;
            existing.Email = updatedPatient.Email;
            existing.HospitalName = updatedPatient.HospitalName;
            existing.Diagnosis = updatedPatient.Diagnosis;
            existing.Status = updatedPatient.Status;

            await _patientRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _patientRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.IsDeleted = true;

            await _patientRepository.UpdateAsync(existing);
            return true;
        }
    }
}
