using SurveyApi.Dtos.Category;
using SurveyApi.Dtos.Question;
using SurveyApi.Models;

namespace SurveyApi.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<ServiceResponse<List<GetQuestionDto>>> GetAllQuestions();

        Task<ServiceResponse<GetQuestionDto>> GetQuestionById(Guid id);

        Task<ServiceResponse<List<GetQuestionDto>>> AddQuestion(AddQuestionDto newQuestion);

        Task<ServiceResponse<GetQuestionDto>> UpdateQuestion(UpdateQuestionDto updatedQuestion, Guid id);

        Task<ServiceResponse<List<GetQuestionDto>>> DeleteQuestion(Guid id);
    }
}
