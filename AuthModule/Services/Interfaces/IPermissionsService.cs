using AuthModule.DTOs;
using AuthModule.DTOs.Permissions.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Services.Interfaces
{
    public interface IPermissionsService
    {
        Task<GeneralResponse<string>> LoadPermissions();
        Task<GeneralResponse<List<string>>> GetAllModules();
        Task<GeneralResponse<string>> AddModuleToCompany(ModuleCompanyRequest model);
        Task<GeneralResponse<string>> RemoveModuleFromCompany(ModuleCompanyRequest model);
        Task<GeneralResponse<string>> AddPermissionsToUser(PermissionsUserRequest model);
        Task<GeneralResponse<string>> RemovePermissionsToUser(PermissionsUserRequest model);

    }
}
