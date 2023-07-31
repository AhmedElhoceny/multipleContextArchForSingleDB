using AuthModule.Controllers;
using AuthModule.DbDesignerServices.IService;
using AuthModule.DbDesignerServices.Service;
using AuthModule.Repos.IRepositories;
using AuthModule.Repos.Repositories;
using AuthModule.Services.Interfaces;
using AuthModule.Services.ServicesImps;
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
            services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
            services.AddMvcCore().AddApplicationPart(typeof(AuthDataBaseDesignerController).Assembly);
            services.AddMvcCore().AddApplicationPart(typeof(CompanyController).Assembly);
            services.AddMvcCore().AddApplicationPart(typeof(UserController).Assembly);

            #region Services
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            #endregion
        }
    }
}
