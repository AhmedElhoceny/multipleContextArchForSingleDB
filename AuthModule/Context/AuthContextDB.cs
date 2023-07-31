using AuthModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedHelpers.HelperServices;
using SharedHelpers.Models;

namespace AuthModule.Context
{
    public class AuthContextDB : DbContext
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthContextDB(DbContextOptions<AuthContextDB> options, IHttpContextAccessor accessor) : base(options)
        {
            _accessor = accessor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralEntity>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasQueryFilter(p => p.IsActive)
                .HasQueryFilter(x => x.CompId == _accessor.HttpContext.User.GetCompanyId());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.NoAction;

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<UserToken> userTokens { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<UserPermission> userPermissions { get; set; }
        public DbSet<Company> companies { get; set; }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            DateTime dateNow = DateTime.Now;
            int userId = _accessor!.HttpContext == null ? 0 : _accessor!.HttpContext!.User.GetUserId();

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is GeneralEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified
                        || e.State == EntityState.Deleted)
                        );

            foreach (var entityEntry in entries)
            {



                if (entityEntry.State == EntityState.Added)
                {
                    ((GeneralEntity)entityEntry.Entity).CreatedDate = dateNow;
                    ((GeneralEntity)entityEntry.Entity).CreatedBy = userId;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
