using backend.Models;

namespace backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DbSetup _context;
        public AuthService(DbSetup context)
        {
            _context = context;
        }

        public async Task<User> RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUser(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.ToLower() == email);

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

    }
}