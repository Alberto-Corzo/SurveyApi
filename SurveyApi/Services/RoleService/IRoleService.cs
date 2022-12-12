using SurveyApi.Dtos.Role;
using SurveyApi.Models;

namespace SurveyApi.Services.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResponse<List<GetRoleDto>>> GetAllRoles();

        Task<ServiceResponse<GetRoleDto>> GetRoleById(Guid id);

        Task<ServiceResponse<List<GetRoleDto>>> AddRole(AddRoleDto newRole);

        Task<ServiceResponse<GetRoleDto>> UpdateRole(UpdateRoleDto updatedRole, Guid id);

        Task<ServiceResponse<List<GetRoleDto>>> DeleteRole(Guid id);
    }
}
