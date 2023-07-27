using AuthModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedHelpers.HelperServices;
using SharedHelpers.Models;

namespace SharedConfilgrations.Context
{
    public class DBContext: DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DBContext(DbContextOptions<DBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralEntity>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasQueryFilter(x => x.CompId == _httpContextAccessor.HttpContext.GetCompanyId());
        }

        #region AuthModule
            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<UserRole> userRoles { get; set; }
            public DbSet<UserToken> userTokens { get; set; }
            public DbSet<Permission> permissions { get; set; }
            public DbSet<UserPermission> userPermissions { get; set; }
            public DbSet<Company> companies { get; set; }
        #endregion
    }
}
