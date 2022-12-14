using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Category;
using SurveyApi.Dtos.Role;
using SurveyApi.Models;
using SurveyApi.Services.RoleService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> GetRole()
        {
            return Ok(await _roleService.GetAllRoles());
        }

        // GET: api/Roles/5
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> GetRoleById(Guid id)
        {
            var response = await _roleService.GetRoleById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> PutRole(Guid id, UpdateRoleDto role)
        {
            var response = await _roleService.UpdateRole(role, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> PostRole(AddRoleDto role)
        {
            return Ok(await _roleService.AddRole(role));
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> DeleteRole(Guid id)
        {
            var response = await _roleService.DeleteRole(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
