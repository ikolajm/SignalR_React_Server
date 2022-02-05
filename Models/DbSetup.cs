using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class DbSetup : DbContext
    {
        public DbSetup(DbContextOptions<DbSetup> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}