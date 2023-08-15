using AuthModule.DTOs.Permissions.Request;
using AuthModule.Services.Interfaces;
using AuthModule.Services.ServicesImps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PermissionsController : ControllerBase
    {
        public IPermissionsService _permissionService;
        public PermissionsController(IPermissionsService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpPut("loadPermissions")]
        public async Task<IActionResult> LoadPermissions()
        {
            var result = await _permissionService.LoadPermissions();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getAllModules")]
        public async Task<IActionResult> GetAllModules()
        {
            var result = await _permissionService.GetAllModules();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("addModuleToCompany")]
        public async Task<IActionResult> AddModuleToCompany([FromBody]ModuleCompanyRequest model)
        {
            var result = await _permissionService.AddModuleToCompany(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("removeModuleFromCompany")]
        public async Task<IActionResult> RemoveModuleFromCompany([FromBody]ModuleCompanyRequest model)
        {
            var result = await _permissionService.RemoveModuleFromCompany(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("addPermissionsToUser")]
        public async Task<IActionResult> AddPermissionsToUser([FromBody]PermissionsUserRequest model)
        {
            var result = await _permissionService.AddPermissionsToUser(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);

        }
        [HttpPost("removePermissionsToUser")]
        public async Task<IActionResult> RemovePermissionsToUser([FromBody]PermissionsUserRequest model)
        {
            var result = await _permissionService.RemovePermissionsToUser(model);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }


    }
}
