using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Models;

namespace SurveyApi.Services.AuthService
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password, string email);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUser();
        Task<ServiceResponse<GetUserDto>> UpdateUser(User user, string password, Guid id);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(Guid id);
        Task<ServiceResponse<GetUserDto>> GetUserById(Guid id);
        Task<bool> UserExist(string username, string email);
        Task<bool> UserIdExist(Guid id);
    }
}
