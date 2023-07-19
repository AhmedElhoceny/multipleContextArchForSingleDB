using Microsoft.Extensions.DependencyInjection;
using OrderModule.Controllers;
using OrderModule.DbDesignerServices.IService;
using OrderModule.DbDesignerServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Configration
{
    public static class OrderDependenciesContainer
    {
        public static void LoadOrderDependencies(this IServiceCollection services)
        {
            services.AddScoped<IOrderDBDesignerService, OrderDBDesignerService>();
            services.AddMvcCore().AddApplicationPart(typeof(OrderDataBaseDesignerController).Assembly);
        }
    }
}
