using AuthModule.DbDesignerServices.IService;
using Microsoft.AspNetCore.Mvc;

namespace AuthModule.Controllers
{
    [Route("api/[controller]")]
    public class AuthDataBaseDesignerController : ControllerBase
    {
        private readonly IAuthDBDesignerService _authDBDesignerService;
        public AuthDataBaseDesignerController(IAuthDBDesignerService authDBDesignerService)
        {
            _authDBDesignerService = authDBDesignerService;
        }
        [HttpPut("buildAuthDataBase")]
        public async Task<IActionResult> BuildAuthDataBase()
        {
            await _authDBDesignerService.BuildAuthModuleDataBase();
            return Ok();
        }
    }
}
