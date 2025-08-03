using CenteralizedPatientsAssignmentTask.Common;
using CenteralizedPatientsAssignmentTask.Common.DTOs;
using CenteralizedPatientsAssignmentTask.Infrastructure;
using CenteralizedPatientsAssignmentTask.Interface;
using CenteralizedPatientsAssignmentTask.Interface.Repositories;
using CenteralizedPatientsAssignmentTask.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CenteralizedPatientsAssignmentTask.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly CenteralizedPatientsAssignmentTaskDbContext _context;

        public PatientRepository(CenteralizedPatientsAssignmentTaskDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<PatientResultDto>> GetPatientsFromSpAsync(PatientQueryParameters query)
        {
            var sql = "EXEC sp_GetPatients @PageNumber, @PageSize, @Search, @SortBy, @SortDirection, @HospitalName, @Status, @Gender";

            var parameters = new[]
            {
                new SqlParameter("@PageNumber", query.PageNumber),
                new SqlParameter("@PageSize", query.PageSize),
                new SqlParameter("@Search", (object?)query.Search ?? DBNull.Value),
                new SqlParameter("@SortBy", (object?)query.SortBy ?? DBNull.Value),
                new SqlParameter("@SortDirection", (object?)query.SortDirection ?? "ASC"),
                new SqlParameter("@HospitalName", (object?)query.Hospital ?? DBNull.Value),
                new SqlParameter("@Status", (object?)query.Status ?? DBNull.Value),
                new SqlParameter("@Gender", (object?)query.Gender ?? DBNull.Value),
            };

            var results = await _context.PatientsResultDto
                .FromSqlRaw(sql, parameters)
                .ToListAsync();

            var totalCount = results.FirstOrDefault()?.TotalCount ?? 0;

            return new PaginatedResult<PatientResultDto>
            {
                TotalCount = totalCount,
                PageData = results
            };
        }

        public async Task AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Patients.AnyAsync(p => p.PatientId == id);
        }
    }
}
