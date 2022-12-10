using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Models;

namespace SurveyApi.Services.AuthService
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<GetUserDto>> UpdateUser(User user, string password, Guid id);
        Task<bool> UserNameExist(string username);
        Task<bool> UserIdExist(Guid id);
    }
}
