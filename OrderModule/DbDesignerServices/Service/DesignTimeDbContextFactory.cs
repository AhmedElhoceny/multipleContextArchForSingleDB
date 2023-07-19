using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderModule.Context;

namespace OrderModule.DbDesignerServices.Service
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderContextDb>
    {
        public OrderContextDb CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<OrderContextDb>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OrderContextDb(optionsBuilder.Options);
        }
    }
}
