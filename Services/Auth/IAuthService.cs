using backend.Models;

namespace backend.Services.AuthService
{
    public interface IAuthService
    {
        // string GetMyName();
        Task<User> RegisterUser(User user);
        Task<User> GetUser(string email);
        Task<User> GetUserById(int id);
    }
}