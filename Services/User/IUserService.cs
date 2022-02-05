using backend.Models;

namespace backend.Services.UserService
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);
        Task<User> EditUser(UserForEdit user);
    }
}