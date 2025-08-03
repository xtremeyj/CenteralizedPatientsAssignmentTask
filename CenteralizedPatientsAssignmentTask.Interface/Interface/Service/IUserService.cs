using CenteralizedPatientsAssignmentTask.Models;

namespace CenteralizedPatientsAssignmentTask.Common.Interface.Service
{
    public interface IUserService
    {
        User? Validate(string username, string password);
    }
}
