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
using SurveyApi.Dtos.UserAnswer;
using SurveyApi.Models;
using SurveyApi.Services.UserAnswerService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswersController : ControllerBase
    {
        private readonly IUserAnswerService _userAnswerService;

        public UserAnswersController(IUserAnswerService userAnswerService)
        {
            _userAnswerService = userAnswerService;
        }

        // GET: api/UserAnswers
        [HttpGet, Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetUserAnswerDto>>>> GetUserAnswer()
        {
            return Ok(await _userAnswerService.GetAllUserAnswers());
        }

        // GET: api/UserAnswers/5
        [HttpGet("{id}"), Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> GetUserAnswer(Guid id)
        {
            var response = await _userAnswerService.GetUserAnswerById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/UserAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Normal")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> PutUserAnswer(Guid id, UpdateUserAnswerDto userAnswer)
        {
            var response = await _userAnswerService.UpdateUserAnswer(userAnswer, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/UserAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetUserAnswerDto>>>> PostUserAnswer(AddUserAnswerDto userAnswer)
        {
            return Ok(await _userAnswerService.AddUserAnswer(userAnswer));
        }

        // DELETE: api/UserAnswers/5
        [HttpDelete("{id}"), Authorize(Roles = "Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetUserAnswerDto>>>> DeleteUserAnswer(Guid id)
        {
            var response = await _userAnswerService.DeleteUserAnswer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
