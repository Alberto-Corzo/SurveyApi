using AutoMapper;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Models;

namespace SurveyApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User/Auth Dto
            CreateMap<User, GetUserDto>();
        }
    }
}
