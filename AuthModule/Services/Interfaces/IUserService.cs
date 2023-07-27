using AuthModule.DTOs;
using AuthModule.DTOs.User.Request;
using AuthModule.DTOs.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Services.Interfaces
{
    public interface IUserService
    {
        public Task<GeneralResponse<UserResponse>> AddUser(AddUserRequest request);
        public Task<GeneralResponse<UserResponse>> UpdateUser(UpdateUserRequest request);
        public Task<GeneralResponse<UserResponse>> DeleteUser(int id);
        public Task<GeneralResponse<UserResponse>> GetUser(int id);
        public Task<GeneralResponse<List<UserResponse>>> GetUsers();
        public Task<GeneralResponse<UserResponse>> VerifyEmail(string email, string verificationCode);
        public Task<GeneralResponse<loginResponse>> Login(string email, string password);
        public Task<GeneralResponse<UserResponse>> ForgetPassword(string email);
        public Task<GeneralResponse<UserResponse>> ResetPassword(string email, string verificationCode, string newPassword);
    }
}
