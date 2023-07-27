using AuthModule.DTOs;
using AuthModule.DTOs.User.Request;
using AuthModule.DTOs.User.Response;
using AuthModule.Helpers;
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
    public class UserService : IUserService
    {
        private readonly IAuthUnitOfWork _authUnitOfWork;
        private readonly IMapper _mapper;
        public UserService(IAuthUnitOfWork authUnitOfWork, IMapper mapper)
        {
            _authUnitOfWork = authUnitOfWork;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<UserResponse>> AddUser(AddUserRequest request)
        {
            try
            {
                if ((await _authUnitOfWork.UserRepository.GetAllAsync(obj => obj.Email == request.Email)).Any())
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "Email is Exist"
                    };
                var addedUser = _mapper.Map<User>(request);
                await _authUnitOfWork.UserRepository.AddAsync(addedUser);
                await _authUnitOfWork.Complete();

                var verificationCode = HashingService.RandomString(6);
                if (!(await MailingService.SendEmailAsync(request.Email, "Verification Code", verificationCode)))
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "Sending Mail Failed"
                    };

                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = true,
                    Message = "User Added Successfully",
                    Data = _mapper.Map<UserResponse>(addedUser)
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<UserResponse>> DeleteUser(int id)
        {
            try
            {
                var searchedUser = await _authUnitOfWork.UserRepository.GetByIdAsync(id);
                if (searchedUser == null)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                await _authUnitOfWork.UserRepository.Remove(searchedUser);
                await _authUnitOfWork.Complete();
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = true,
                    Message = "User Deleted Successfully",
                    Data = _mapper.Map<UserResponse>(searchedUser)
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<UserResponse>> ForgetPassword(string email)
        {
            try
            {
                var searchedUser = (await _authUnitOfWork.UserRepository.GetAllAsync(obj => obj.Email == email)).FirstOrDefault();
                if (searchedUser == null)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };

                var verificationCode = HashingService.RandomString(6);
                if(!(await MailingService.SendEmailAsync(email, "Verification Code", verificationCode)))
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "Sending Mail Failed"
                    };
                searchedUser.EmailVerificationCode = verificationCode;
                _authUnitOfWork.UserRepository.Update(searchedUser);
                await _authUnitOfWork.Complete();

                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = true,
                    Message = "Verification Code Sent Successfully"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<UserResponse>> GetUser(int id)
        {
            try
            {
                var searchedUser = await _authUnitOfWork.UserRepository.GetByIdAsync(id);
                if (searchedUser == null)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                return new GeneralResponse<UserResponse>()
                {
                    Data = _mapper.Map<UserResponse>(searchedUser),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<List<UserResponse>>> GetUsers()
        {
            try
            {
                var users = await _authUnitOfWork.UserRepository.GetAllAsync();
                return new GeneralResponse<List<UserResponse>>()
                {
                    Data = _mapper.Map<List<UserResponse>>(users),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<UserResponse>>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }

        }

        public async Task<GeneralResponse<loginResponse>> Login(string email, string password)
        {
            try
            {
                var searchedUser = _authUnitOfWork.UserRepository.GetAll(obj => obj.Email == email).FirstOrDefault();
                if (searchedUser == null)
                    return new GeneralResponse<loginResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                if (searchedUser.PassWord != HashingService.GetHash(password))
                    return new GeneralResponse<loginResponse>()
                    {
                        IsSuccess = false,
                        Message = "Password is Wrong"
                    };
                if (!searchedUser.IsEmailConfirmaed)
                    return new GeneralResponse<loginResponse>()
                    {
                        IsSuccess = false,
                        Message = "Email is Not Verified"
                    };
                var userPermissions = (await _authUnitOfWork.UserPermissionRepository.GetSpecificSelectAsync(obj => obj.User_Id == searchedUser.Id, select: obj => obj.Permission.Name)).ToList();
                var token = TokenServices.GenerateJSONWebToken(new UserTokenInfo() { companyId = (int)searchedUser.CompId, userId = searchedUser.Id, Permissions = userPermissions });
                return new GeneralResponse<loginResponse>()
                {
                    IsSuccess = true,
                    Message = "Logged In Successfully",
                    Data = new loginResponse()
                    {
                        token = token
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<loginResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<UserResponse>> ResetPassword(string email, string verificationCode, string newPassword)
        {
            try
            {
                var searchedUser = (await _authUnitOfWork.UserRepository.GetAllAsync(obj => obj.Email == email)).FirstOrDefault();
                if (searchedUser == null)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                if (searchedUser.EmailVerificationCode != verificationCode)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "Verification Code is Wrong"
                    };
                searchedUser.PassWord = HashingService.GetHash(newPassword);
                _authUnitOfWork.UserRepository.Update(searchedUser);
                await _authUnitOfWork.Complete();
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = true,
                    Message = "Password Changed Successfully"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<UserResponse>> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                var searchedUser = await _authUnitOfWork.UserRepository.GetByIdAsync(request.Id);
                if (searchedUser == null)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                if (request.Email != searchedUser.Email)
                {
                    if ((await _authUnitOfWork.UserRepository.GetAllAsync(obj => obj.Email == request.Email)).Any())
                        return new GeneralResponse<UserResponse>()
                        {
                            IsSuccess = false,
                            Message = "Email is Exist"
                        };
                }
                searchedUser = _mapper.Map<User>(request);
                _authUnitOfWork.UserRepository.Update(searchedUser);
                await _authUnitOfWork.Complete();
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = true,
                    Message = "User Updated Successfully",
                    Data = _mapper.Map<UserResponse>(searchedUser)
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<UserResponse>> VerifyEmail(string email, string verificationCode)
        {
            try
            {
                var searchedUser = (await _authUnitOfWork.UserRepository.GetAllAsync(obj => obj.Email == email)).FirstOrDefault();
                if (searchedUser == null)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "User Not Found"
                    };
                if (searchedUser.EmailVerificationCode != verificationCode)
                    return new GeneralResponse<UserResponse>()
                    {
                        IsSuccess = false,
                        Message = "Verification Code is Wrong"
                    };
                searchedUser.IsEmailConfirmaed = true;
                _authUnitOfWork.UserRepository.Update(searchedUser);
                await _authUnitOfWork.Complete();
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = true,
                    Message = "Email Verified Successfully"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Error = ex
                };

            }
        }
    }
}
