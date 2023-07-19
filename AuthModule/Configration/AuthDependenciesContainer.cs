using AuthModule.Controllers;
using AuthModule.DbDesignerServices.IService;
using AuthModule.DbDesignerServices.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Configration
{
    public static class AuthDependenciesContainer
    {
        public static void LoadAuthDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthDBDesignerService, AuthDBDesignerService>();
            services.AddMvcCore().AddApplicationPart(typeof(AuthDataBaseDesignerController).Assembly);
        }
    }
}
