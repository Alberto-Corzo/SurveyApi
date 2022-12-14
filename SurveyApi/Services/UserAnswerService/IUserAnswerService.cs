using SurveyApi.Dtos.UserAnswer;
using SurveyApi.Models;

namespace SurveyApi.Services.UserAnswerService
{
    public interface IUserAnswerService
    {
        Task<ServiceResponse<List<GetUserAnswerDto>>> GetAllUserAnswers();

        Task<ServiceResponse<GetUserAnswerDto>> GetUserAnswerById(Guid id);

        Task<ServiceResponse<List<GetUserAnswerDto>>> AddUserAnswer(AddUserAnswerDto newUserAnswer);

        Task<ServiceResponse<GetUserAnswerDto>> UpdateUserAnswer(UpdateUserAnswerDto updatedUserAnswer, Guid id);

        Task<ServiceResponse<List<GetUserAnswerDto>>> DeleteUserAnswer(Guid id);
    }
}
