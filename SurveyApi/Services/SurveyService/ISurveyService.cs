using SurveyApi.Dtos.Survey;
using SurveyApi.Models;

namespace SurveyApi.Services.SurveyService
{
    public interface ISurveyService
    {
        Task<ServiceResponse<List<GetSurveyDto>>> GetAllSurveys();

        Task<ServiceResponse<GetSurveyDto>> GetSurveyById(int id);

        Task<ServiceResponse<List<GetSurveyDto>>> AddSurvey(AddSurveyDto newSurvey);

        Task<ServiceResponse<GetSurveyDto>> UpdateSurvey(UpdateSurveyDto updatedSurvey, int id);

        Task<ServiceResponse<List<GetSurveyDto>>> DeleteSurvey(int id);
    }
}
