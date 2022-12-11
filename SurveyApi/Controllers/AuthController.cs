using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Models;
using SurveyApi.Services.AuthService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepo _authRepo;

        public AuthController(DataContext context, IAuthRepo authRepo)
        {
            _context = context;
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            //Data that will be inserted by user
            var response = await _authRepo.Register(
                new User
                {
                    StrName = request.StrName,
                    StrFirstSurname = request.StrFirstSurname,
                    StrLastSurname = request.StrLastSurname,
                    Email = request.Email,
                    Status = request.Status,
                    PhotoId = request.PhotoId,
                }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(request.StrName, request.Password, request.Email);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("users")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetUser()
        {
            return Ok(await _authRepo.GetAllUser());
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserById(Guid id)
        {
            var response = await _authRepo.GetUserById(id);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(Guid id)
        {
            var response = await _authRepo.DeleteUser(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto updUser, Guid id)
        {
            var response = await _authRepo.UpdateUser(
                    new User 
                    {
                        StrName = updUser.StrName,
                        StrFirstSurname = updUser.StrFirstSurname,
                        StrLastSurname = updUser.StrLastSurname,
                        Email = updUser.Email,
                        Status = updUser.Status,
                        PhotoId = updUser.PhotoId,
                    }, updUser.Password, id
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
