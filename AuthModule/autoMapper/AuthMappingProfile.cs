using AuthModule.DTOs.Company.Request;
using AuthModule.DTOs.Company.Response;
using AuthModule.DTOs.User.Request;
using AuthModule.DTOs.User.Response;
using AuthModule.Models;
using AutoMapper;
using SharedHelpers.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.autoMapper
{
    internal class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            #region Comapny
                CreateMap<AddCompanyRequest,Company>();
                CreateMap<UpdateCompanyRequest,Company>();
                CreateMap<Company,CompanyResponse>();

            CreateMap<Company, User>()
            .ForMember(User => User.Name, opt => opt.MapFrom(Company => $"{Company.Name}_Admin"))
            .ForMember(User => User.Email, opt => opt.MapFrom(Company => $"{Company.Name}_Admin@{Company.Name}.com"))
            .ForMember(User => User.PassWord, opt => opt.MapFrom(Company => "123456"))
            .ForMember(User => User.Address, opt => opt.MapFrom(Company => Company.Address))
            .ForMember(User => User.CompId, opt => opt.MapFrom(Company => Company.Id))
            .ForMember(User => User.IsEmailConfirmaed, opt => opt.MapFrom(Company => true))
            .ForMember(User => User.Phone, opt => opt.MapFrom(Company => Company.Phone));

            CreateMap<Company, Role>()
                .ForMember(Role => Role.Name, opt => opt.MapFrom(Company => $"{Company.Name}_Admin"));

            #endregion

            #region user
            CreateMap<AddUserRequest, User>()
                .ForMember(User => User.PassWord, opt => opt.MapFrom(obj => HashingService.GetHash(obj.PassWord)))
                .ForMember(User => User.IsEmailConfirmaed, opt => opt.MapFrom(obj => false));
            CreateMap<UpdateUserRequest, User>()
                .ForMember(User => User.PassWord, opt => opt.MapFrom(obj => HashingService.GetHash(obj.PassWord)));
            CreateMap<User, UserResponse>();
            #endregion
        }
    }
}
