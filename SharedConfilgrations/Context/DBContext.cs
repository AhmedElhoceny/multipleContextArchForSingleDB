using AuthModule.Models;
using Microsoft.EntityFrameworkCore;

namespace SharedConfilgrations.Context
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        #region AuthModule
            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<UserRole> userRoles { get; set; }
            public DbSet<UserToken> userTokens { get; set; }
            public DbSet<Permission> permissions { get; set; }
            public DbSet<UserPermission> userPermissions { get; set; }
        #endregion
    }
}
