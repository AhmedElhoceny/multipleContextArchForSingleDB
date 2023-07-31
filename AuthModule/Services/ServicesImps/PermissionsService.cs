using AuthModule.DTOs;
using AuthModule.DTOs.Permissions.Request;
using AuthModule.Models;
using AuthModule.Repos.IRepositories;
using AuthModule.Services.Interfaces;
using AutoMapper;
using SharedHelpers.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Services.ServicesImps
{
    public class PermissionsService : IPermissionsService
    {
        public IAuthUnitOfWork _authUnitOfWork;
        public IMapper _mapper;
        public PermissionsService(IMapper mapper, IAuthUnitOfWork authUnitOfWork)
        {
            _mapper = mapper;
            _authUnitOfWork = authUnitOfWork;
        }
        public Task<GeneralResponse<string>> AddModuleToCompany(ModuleCompanyRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<string>> AddPermissionsToUser(PermissionsUserRequest model)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse<List<string>>> GetAllModules()
        {
            return new GeneralResponse<List<string>>()
            {
                Data = PermissionGenerator.GenerateModules(),
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<GeneralResponse<string>> LoadPermissions()
        {
            using (var transaction = await _authUnitOfWork.BeginTransaction())
            {
                try
                {
                    var searchedSuperAdmin = await _authUnitOfWork.UserRepository.GetFirstOrDefaultAsync(ignoreQueryFilters: true, filter: obj => obj.Name == "SuperAdmin");
                    if (searchedSuperAdmin is null)
                    {
                        await _authUnitOfWork.UserRepository.AddAsync(new User() { Address = "", CompId = 0, CreatedBy = 0, CreatedDate = DateTime.Now, Email = "SuperAdmin@gmail.com", EmailVerificationCode = "", Id = 0, ImagePath = "", IsActive = true, IsDeleted = false, IsEmailConfirmaed = true, ModifiedBy = 0, ModifiedDate = DateTime.Now, Name = "SuperAdmin", NationalId = "", PassWord = HashingService.GetHash("123456"), Phone = "" });
                        await _authUnitOfWork.Complete();
                    }
                    var existingPermissions = await _authUnitOfWork.PermissionRepository.GetAllAsync();
                    var permissions = PermissionGenerator.GeneratePermissions().Select(per => new Permission() { Name = per }).Where(obj => existingPermissions.All(x => x.Name != obj.Name)).ToList();
                    var addedUserPermissions = (await _authUnitOfWork.PermissionRepository.AddRangeAsync(permissions)).ToList();


                    var superAdminPermissions = (await _authUnitOfWork.UserPermissionRepository.GetAllAsync(ignoreQueryFilters: true, filter: obj => obj.User_Id == searchedSuperAdmin!.Id)).ToList();
                    _authUnitOfWork.UserPermissionRepository.RemoveRange(superAdminPermissions);
                    _authUnitOfWork.Complete();

                    var userPermisions = addedUserPermissions.Select(obj => new UserPermission() { User_Id = searchedSuperAdmin!.Id, Permission_Id = obj.Id });
                    await _authUnitOfWork.UserPermissionRepository.AddRangeAsync(userPermisions);
                    await _authUnitOfWork.Complete();

                    transaction.Commit();

                    return new GeneralResponse<string>()
                    {
                        IsSuccess = true,
                        Message = "Done"
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new GeneralResponse<string>()
                    {
                        Error = ex,
                        IsSuccess = false
                    };
                }
            }
            
        }

        public Task<GeneralResponse<string>> RemoveModuleFromCompany(ModuleCompanyRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<string>> RemovePermissionsToUser(PermissionsUserRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
