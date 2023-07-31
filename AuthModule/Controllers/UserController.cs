using AuthModule.DTOs.User.Request;
using AuthModule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthModule.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("userRegister")]
        public async Task<IActionResult> GetAll(AddUserRequest model)
        {
            var result =await _userService.AddUser(model);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("userLogin")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await _userService.Login(model.email, model.password,model.CompanyId);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest model)
        {
            var result = await _userService.UpdateUser(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetUser(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            var result = await _userService.VerifyEmail(model.email, model.verificationCode);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest model)
        {
            var result = await _userService.ForgetPassword(model.email);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            var result = await _userService.ResetPassword(model.email, model.verificationCode, model.newPassword);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
