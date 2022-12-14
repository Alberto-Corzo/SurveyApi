using SurveyApi.Dtos.QuestionOption;
using SurveyApi.Models;

namespace SurveyApi.Services.QuestionOptionService
{
    public interface IQuestionOptionService
    {
        Task<ServiceResponse<List<GetQuestionOptionDto>>> GetAllQuestionOptions();

        Task<ServiceResponse<GetQuestionOptionDto>> GetQuestionOptionById(Guid id);

        Task<ServiceResponse<List<GetQuestionOptionDto>>> AddQuestionOption(AddQuestionOptionDto newQuestionOption);

        Task<ServiceResponse<GetQuestionOptionDto>> UpdateQuestionOption(UpdateQuestionOptionDto updatedQuestionOption, Guid id);

        Task<ServiceResponse<List<GetQuestionOptionDto>>> DeleteQuestionOption(Guid id);
    }
}
