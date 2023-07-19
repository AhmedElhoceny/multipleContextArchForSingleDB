using AuthModule.Models;
using Microsoft.EntityFrameworkCore;
using OrderModule.Models;

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
        #endregion
        #region OrdersModule
            public DbSet<Order> Orders { get; set; }
        #endregion
    }
}
