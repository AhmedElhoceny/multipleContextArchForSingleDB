﻿using AuthModule.DTOs;
using AuthModule.DTOs.Company.Request;
using AuthModule.DTOs.Company.Response;
using AuthModule.Models;
using AuthModule.Repos.IRepositories;
using AuthModule.Services.Interfaces;
using AutoMapper;

namespace AuthModule.Services.ServicesImps
{
    public class CompanyService : ICompanyService
    {
        private readonly IAuthUnitOfWork _authUnitOfWork;
        private readonly IMapper _mapper;
        public CompanyService(IAuthUnitOfWork authUnitOfWork, IMapper mapper)
        {
            _authUnitOfWork = authUnitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<CompanyResponse>> CreateCompany(AddCompanyRequest request)
        {
            try
            {
                if ((await _authUnitOfWork.CompanyRepository.GetAllAsync(obj => obj.Name == request.Name)).Any())
                {
                    return new GeneralResponse<CompanyResponse>
                    {
                        IsSuccess = false,
                        Message = "Company already exists"
                    };
                }

                using (var transaction = await _authUnitOfWork.BeginTransaction())
                {
                    try
                    {
                        var company = _mapper.Map<Company>(request);
                        var addedCompany = await _authUnitOfWork.CompanyRepository.AddAsync(company);
                        await _authUnitOfWork.Complete();

                        var companyAdminUser = _mapper.Map<User>(addedCompany);
                        var addedUser = await _authUnitOfWork.UserRepository.AddAsync(companyAdminUser);
                        await _authUnitOfWork.Complete();

                        var companyAdminRole = _mapper.Map<Role>(addedCompany);
                        var addedCompanyAdminRole = await _authUnitOfWork.RoleRepository.AddAsync(companyAdminRole);
                        await _authUnitOfWork.Complete();

                        var userRole = new UserRole
                        {
                            Role_Id = addedCompanyAdminRole.Id,
                            User_Id = addedUser.Id
                        };
                        await _authUnitOfWork.UserRoleRepository.AddAsync(userRole);
                        await _authUnitOfWork.Complete();

                        await transaction.CommitAsync();

                        return new GeneralResponse<CompanyResponse>
                        {
                            IsSuccess = true,
                            Message = "Company created successfully",
                            Data = _mapper.Map<CompanyResponse>(addedCompany)
                        };
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return new GeneralResponse<CompanyResponse>
                        {
                            IsSuccess = false,
                            Message = "Something went wrong",
                            Data = null,
                            Error = ex
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = false,
                    Message = "Something went wrong",
                    Data = null,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<CompanyResponse>> DeleteCompany(int id)
        {
            try
            {
                var searchedCompany = await _authUnitOfWork.CompanyRepository.GetByIdAsync(id);
                if (searchedCompany == null)
                {
                    return new GeneralResponse<CompanyResponse>
                    {
                        IsSuccess = false,
                        Message = "Company not found"
                    };
                }
                await _authUnitOfWork.CompanyRepository.Remove(searchedCompany);
                await _authUnitOfWork.Complete();
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = true,
                    Message = "Company deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = false,
                    Message = "Something went wrong",
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<List<CompanyResponse>>> GetAllCompanies()
        {
            try
            {
                var data = await _authUnitOfWork.CompanyRepository.GetAllAsync();
                var mappedData = _mapper.Map<List<CompanyResponse>>(data);
                
                return new GeneralResponse<List<CompanyResponse>>
                {
                    IsSuccess = true,
                    Message = "Companies retrieved successfully",
                    Data = mappedData
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<CompanyResponse>>
                {
                    IsSuccess = false,
                    Message = "Something went wrong",
                    Data = null,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<CompanyResponse>> GetCompanyById(int id)
        {
            try
            {
                var searchedCompany = _authUnitOfWork.CompanyRepository.GetByIdAsync(id);
                if (searchedCompany == null)
                {
                    return new GeneralResponse<CompanyResponse>
                    {
                        IsSuccess = false,
                        Message = "Company not found"
                    };
                }
                var mappedData = _mapper.Map<CompanyResponse>(searchedCompany);
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = true,
                    Message = "Company retrieved successfully",
                    Data = mappedData
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = false,
                    Message = "Something went wrong",
                    Data = null,
                    Error = ex
                };
            }
        }

        public async Task<GeneralResponse<CompanyResponse>> UpdateCompany(UpdateCompanyRequest request)
        {
            try
            {
                var searchedCompany = _authUnitOfWork.CompanyRepository.GetByIdAsync(request.Id);
                if (searchedCompany == null)
                {
                    return new GeneralResponse<CompanyResponse>
                    {
                        IsSuccess = false,
                        Message = "Company not found"
                    };
                }
                var mappedData = _mapper.Map<Company>(request);
                _authUnitOfWork.CompanyRepository.Update(mappedData);
                _authUnitOfWork.Complete();
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = true,
                    Message = "Company updated successfully",
                    Data = _mapper.Map<CompanyResponse>(mappedData)
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CompanyResponse>
                {
                    IsSuccess = false,
                    Message = "Something went wrong",
                    Data = null,
                    Error = ex
                };
            }
        }
    }
}