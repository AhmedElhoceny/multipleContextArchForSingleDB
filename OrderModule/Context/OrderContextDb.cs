using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderModule.Models;

namespace OrderModule.Context
{
    public class OrderContextDb : DbContext
    {
        public OrderContextDb(DbContextOptions<OrderContextDb> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var connectionString = config.GetConnectionString("OrdersDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Order> Orders { get; set; }
    }
}
