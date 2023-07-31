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
            .ForMember(User => User.PassWord, opt => opt.MapFrom(Company => HashingService.GetHash("123456")))
            .ForMember(User => User.Address, opt => opt.MapFrom(Company => Company.Address))
            .ForMember(User => User.CompId, opt => opt.MapFrom(Company => Company.Id))
            .ForMember(User => User.IsEmailConfirmaed, opt => opt.MapFrom(Company => true))
            .ForMember(User => User.Phone, opt => opt.MapFrom(Company => Company.Phone))
            .ForMember(user => user.Id , opt => opt.MapFrom(obj => 0))
            .ForMember(user => user.EmailVerificationCode , opt => opt.MapFrom(obj => ""));

            CreateMap<Company, Role>()
                .ForMember(Role => Role.Name, opt => opt.MapFrom(Company => $"{Company.Name}_Admin"))
                .ForMember(Role => Role.CompId, opt => opt.MapFrom(Company => Company.Id))
                .ForMember(Role => Role.IsActive, opt => opt.MapFrom(Company =>true))
                .ForMember(Role => Role.Id, opt => opt.MapFrom(Company => 0));

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
