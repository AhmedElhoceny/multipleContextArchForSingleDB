using AuthModule.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthModule.Context
{
    public class AuthContextDB : DbContext
    {
        public AuthContextDB(DbContextOptions<AuthContextDB> options) : base(options){}

        public DbSet<User> Users { get; set; }
    }
}
