using AuthModule.DTOs;
using AuthModule.DTOs.Company.Request;
using AuthModule.DTOs.Company.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Services.Interfaces
{
    public interface ICompanyService
    {
        // Write Crud Operations for Company interface
        public Task<GeneralResponse<CompanyResponse>> CreateCompany(AddCompanyRequest request);
        public Task<GeneralResponse<CompanyResponse>> UpdateCompany(UpdateCompanyRequest request);
        public Task<GeneralResponse<CompanyResponse>> DeleteCompany(int id);
        public Task<GeneralResponse<CompanyResponse>> GetCompanyById(int id);
        public Task<GeneralResponse<List<CompanyResponse>>> GetAllCompanies();

    }
}
