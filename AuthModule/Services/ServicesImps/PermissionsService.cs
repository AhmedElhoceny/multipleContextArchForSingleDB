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
        public async Task<GeneralResponse<string>> AddModuleToCompany(ModuleCompanyRequest model)
        {
            try
            {
                var companyAdmin = await _authUnitOfWork.UserRepository.GetFirstOrDefaultAsync(filter: obj => obj.CompId == model.CompanyId && obj.IsAdmin, ignoreQueryFilters: true);
                if (companyAdmin is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "Company Admin Not Found"
                    };
                }
                var modulePermissions = (await _authUnitOfWork.PermissionRepository.GetAllAsync(filter: obj => model.Modules.Any(x => obj.Name.Contains(x)), ignoreQueryFilters: true)).ToList();

                var ExistPermissions = _authUnitOfWork.UserPermissionRepository.GetAll(filter: obj => obj.User_Id == companyAdmin.Id && modulePermissions.Any(x => obj.Permission_Id == x.Id), ignoreQueryFilters: true).ToList();

                var addingPermissions = modulePermissions.Where(obj => !ExistPermissions.Any(x => x.Permission_Id == obj.Id)).ToList();

                var userPermissions = addingPermissions.Select(obj => new UserPermission() { Permission_Id = obj.Id, User_Id = companyAdmin.Id }).ToList();

                await _authUnitOfWork.UserPermissionRepository.AddRangeAsync(userPermissions);
                await _authUnitOfWork.Complete();
                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GeneralResponse<string>> AddPermissionsToUser(PermissionsUserRequest model)
        {
            try
            {
                var searchedUser = await _authUnitOfWork.UserRepository.GetFirstOrDefaultAsync(filter: obj => obj.Id == model.UserId, ignoreQueryFilters: true);
                if (searchedUser is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                }
                var permissions = await _authUnitOfWork.PermissionRepository.GetAllAsync(filter: obj => model.Permissions.Any(x => obj.Name.Contains(x)), ignoreQueryFilters: true);
                if (permissions is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "Permissions Not Found"
                    };
                }

                var userPermissions = await _authUnitOfWork.UserPermissionRepository.GetAllAsync(filter: obj => obj.User_Id == model.UserId && permissions.Any(x => x.Id == obj.Permission_Id), ignoreQueryFilters: true);

                var addingPermissions = permissions.Where(obj => !userPermissions.Any(x => x.Permission_Id == obj.Id)).ToList();

                var userPermissionsToAdd = addingPermissions.Select(obj => new UserPermission() { Permission_Id = obj.Id, User_Id = model.UserId }).ToList();

                await _authUnitOfWork.UserPermissionRepository.AddRangeAsync(userPermissionsToAdd);

                await _authUnitOfWork.Complete();

                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = false,
                    Error = ex
                };
            }
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
                    var searchedSuperAdmin = await _authUnitOfWork.UserRepository.GetFirstOrDefaultAsync(ignoreQueryFilters: true, filter: obj => obj.IsSuperAdmin);
                    if (searchedSuperAdmin is null)
                    {
                        await _authUnitOfWork.UserRepository.AddAsync(new User() { Address = "", CompId = 0, CreatedBy = 0, CreatedDate = DateTime.Now, Email = "SuperAdmin@gmail.com", EmailVerificationCode = "", Id = 0, ImagePath = "", IsActive = true, IsDeleted = false, IsEmailConfirmaed = true, ModifiedBy = 0, ModifiedDate = DateTime.Now, Name = "SuperAdmin", NationalId = "", PassWord = HashingService.GetHash("123456"), Phone = "",IsSuperAdmin = true,IsAdmin = true });
                        await _authUnitOfWork.Complete();
                    }
                    var existingPermissions = await _authUnitOfWork.PermissionRepository.GetAllAsync(ignoreQueryFilters: true);
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

        public async Task<GeneralResponse<string>> RemoveModuleFromCompany(ModuleCompanyRequest model)
        {
            try
            {
                var companyAdmin = await _authUnitOfWork.UserRepository.GetFirstOrDefaultAsync(filter: obj => obj.IsAdmin && obj.CompId == model.CompanyId, ignoreQueryFilters: true);
                if (companyAdmin is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "Company Admin Not Found"
                    };
                }
                var permissions = await _authUnitOfWork.PermissionRepository.GetAllAsync(filter: obj => model.Modules.Any(x => obj.Name.Contains(x)), ignoreQueryFilters: true);
                if (permissions is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "Modules Not Found"
                    };
                }
                var companyAdminPermissions = await _authUnitOfWork.UserPermissionRepository.GetAllAsync(filter: obj => obj.User_Id == companyAdmin.Id, ignoreQueryFilters: true);
                var removingPermissions = companyAdminPermissions.Where(obj => permissions.Any(x => x.Id == obj.Permission_Id));
                _authUnitOfWork.UserPermissionRepository.RemoveRange(removingPermissions);
                _authUnitOfWork.Complete();

                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GeneralResponse<string>> RemovePermissionsToUser(PermissionsUserRequest model)
        {
            try
            {
                var searchedUser = await _authUnitOfWork.UserRepository.GetFirstOrDefaultAsync(filter: obj => obj.Id == model.UserId, ignoreQueryFilters: true);
                if (searchedUser is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                }

                var permissions = await _authUnitOfWork.PermissionRepository.GetAllAsync(filter: obj => model.Permissions.Any(x => obj.Name.Contains(x)), ignoreQueryFilters: true);
                if (permissions is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "Permissions Not Found"
                    };
                }

                var userPermissions = await _authUnitOfWork.UserPermissionRepository.GetAllAsync(filter: obj => obj.User_Id == model.UserId && permissions.Any(x => x.Id == obj.Permission_Id), ignoreQueryFilters: true);
                if (userPermissions is null)
                {
                    return new GeneralResponse<string>()
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "Permissions Not Found"
                    };
                }

                _authUnitOfWork.UserPermissionRepository.RemoveRange(userPermissions);
                await _authUnitOfWork.Complete();

                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>()
                {
                    Data = "",
                    IsSuccess = false,
                    Error = ex
                };
            }
        }
    }
}
