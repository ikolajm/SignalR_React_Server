using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DbSetup _context;
        public UserService(DbSetup context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> EmailExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            } else {
                return false;
            }
        }

        public async Task<User> EditUser(UserForEdit user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.UserId);
            existingUser!.Username = user.Username;
            _context.SaveChanges();

            return existingUser;
        }
    }
}