using AuthModule.DTOs.Company.Request;
using AuthModule.Services.Interfaces;
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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        // Write Crud Endpoints Here
        [HttpGet("getCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            var result = await _companyService.GetAllCompanies();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getCompanyById/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var result = await _companyService.GetCompanyById(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);

        }
        [HttpPost("addCompany")]
        public async Task<IActionResult> AddCompany([FromBody]AddCompanyRequest company)
        {
            var result = await _companyService.CreateCompany(company);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);

        }
        [HttpPut("updateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody]UpdateCompanyRequest company)
        {
            var result = await _companyService.UpdateCompany(company);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);


        }
        [HttpDelete("deleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var result = await _companyService.DeleteCompany(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
