using CenteralizedPatientsAssignmentTask.Common.Interface.Service;
using CenteralizedPatientsAssignmentTask.Models;
using Microsoft.Extensions.Configuration;

namespace CenteralizedPatientsAssignmentTask.Service
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;

        public UserService(IConfiguration config)
        {
            _users = config.GetSection("Users").Get<List<User>>() ?? new();
        }

        public User? Validate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
