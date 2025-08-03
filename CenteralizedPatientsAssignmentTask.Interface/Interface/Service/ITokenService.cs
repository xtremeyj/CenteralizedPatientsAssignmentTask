namespace CenteralizedPatientsAssignmentTask.Common.Interface.Service
{
    public interface ITokenService
    {
        string GenerateToken(string username, string role);
    }
}
