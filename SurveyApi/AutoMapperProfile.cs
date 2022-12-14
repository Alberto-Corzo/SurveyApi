using AutoMapper;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Dtos.Category;
using SurveyApi.Dtos.Photo;
using SurveyApi.Dtos.Question;
using SurveyApi.Dtos.QuestionOption;
using SurveyApi.Dtos.Role;
using SurveyApi.Dtos.Survey;
using SurveyApi.Dtos.UserAnswer;
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
            CreateMap<Role, GetRoleDto>();
            CreateMap<AddRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            // Photo Dto
            CreateMap<Photo, GetPhotoDto>();
            CreateMap<AddPhotoDto, Photo>();
            CreateMap<UpdatePhotoDto, Photo>();

            // Survey Dto
            CreateMap<Survey, GetSurveyDto>();
            CreateMap<AddSurveyDto, Survey>();
            CreateMap<UpdateSurveyDto, Survey>();

            // Question Dto
            CreateMap<Question, GetQuestionDto>();
            CreateMap<AddQuestionDto, Question>();
            CreateMap<UpdateQuestionDto, Question>();
            
            // Question Option Dto
            CreateMap<QuestionOption, GetQuestionOptionDto>();
            CreateMap<AddQuestionOptionDto, QuestionOption>();
            CreateMap<UpdateQuestionOptionDto, QuestionOption>();

            // User Answer Dto
            CreateMap<UserAnswer, GetUserAnswerDto>();
            CreateMap<AddUserAnswerDto, UserAnswer>();
            CreateMap<UpdateUserAnswerDto, UserAnswer>();
        }
    }
}
