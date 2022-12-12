using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Role;
using SurveyApi.Models;

namespace SurveyApi.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoleService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetRoleDto>>> AddRole(AddRoleDto newRole)
        {
            var response = new ServiceResponse<List<GetRoleDto>>();

            Role rol = _mapper.Map<Role>(newRole);

            _context.Role.Add(rol);

            await _context.SaveChangesAsync();

            response.Data = await _context.Role.Select(r => _mapper.Map<GetRoleDto>(r)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> DeleteRole(Guid id)
        {
            ServiceResponse<List<GetRoleDto>> response = new ServiceResponse<List<GetRoleDto>>();
            try
            {
                Role rol = await _context.Role
                    .FirstOrDefaultAsync(u => u.IdRole.ToString().ToUpper() == id.ToString().ToUpper());

                if (rol != null)
                {
                    _context.Role.Remove(rol);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Role.Select(c => _mapper.Map<GetRoleDto>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Role Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> GetAllRoles()
        {
            var response = new ServiceResponse<List<GetRoleDto>>();

            var roles = await _context.Role.ToListAsync();

            response.Data = roles.Select(c => _mapper.Map<GetRoleDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetRoleDto>> GetRoleById(Guid id)
        {
            var response = new ServiceResponse<GetRoleDto>();
            var rol = await _context.Role
                .FirstOrDefaultAsync(c => c.IdRole.ToString().ToUpper() == id.ToString().ToUpper());

            if (rol != null)
            {
                response.Data = _mapper.Map<GetRoleDto>(rol);
            }
            else
            {
                response.Success = false;
                response.Message = "Role Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetRoleDto>> UpdateRole(UpdateRoleDto updatedRole, Guid id)
        {
            ServiceResponse<GetRoleDto> response = new ServiceResponse<GetRoleDto>();

            try
            {
                var rol = await _context.Role
                    .FirstOrDefaultAsync(c => c.IdRole.ToString().ToUpper() == id.ToString().ToUpper());

                if (rol != null)
                {
                    _mapper.Map(updatedRole, rol);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetRoleDto>(rol);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Role Not Found";
                }
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
