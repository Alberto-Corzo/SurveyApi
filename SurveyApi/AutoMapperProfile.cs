using AutoMapper;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Dtos.Category;
using SurveyApi.Models;

namespace SurveyApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User/Auth Dto
            CreateMap<User, GetUserDto>();

            // Category Dto
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Role Dto
            //CreateMap<Role, GetRoleDto>();
            //CreateMap<AddRoleDto, Role>();
            //CreateMap<UpdateRoleDto, Role>();
        }
    }
}
