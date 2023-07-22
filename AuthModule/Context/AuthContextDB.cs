using AuthModule.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthModule.Context
{
    public class AuthContextDB : DbContext
    {
        public AuthContextDB(DbContextOptions<AuthContextDB> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<UserToken> userTokens { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<UserPermission> userPermissions { get; set; }
    }
}
