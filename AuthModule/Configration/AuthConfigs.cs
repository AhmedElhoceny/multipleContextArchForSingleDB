using Microsoft.Extensions.DependencyInjection;
using AuthModule.autoMapper;

namespace AuthModule.Configration
{
    public static class AuthConfigs
    {
        public static void RunAuthConfigs(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthMappingProfile));
        }
    }
}
