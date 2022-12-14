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
using SurveyApi.Dtos.QuestionOption;
using SurveyApi.Models;
using SurveyApi.Services.QuestionOptionService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionsController : ControllerBase
    {
        private readonly IQuestionOptionService _questionOptionService;

        public QuestionOptionsController(IQuestionOptionService questionOptionService)
        {
            _questionOptionService = questionOptionService;
        }

        // GET: api/QuestionOptions
        [HttpGet, Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionOptionDto>>>> GetQuestionOption()
        {
            return Ok(await _questionOptionService.GetAllQuestionOptions());
        }

        // GET: api/QuestionOptions/5
        [HttpGet("{id}"), Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<GetQuestionOptionDto>>> GetQuestionOptionById(Guid id)
        {
            var response = await _questionOptionService.GetQuestionOptionById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/QuestionOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetQuestionOptionDto>>> PutQuestionOption(Guid id, UpdateQuestionOptionDto questionOption)
        {
            var response = await _questionOptionService.UpdateQuestionOption(questionOption, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/QuestionOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionOptionDto>>>> PostQuestionOption(AddQuestionOptionDto questionOption)
        {
            return Ok(await _questionOptionService.AddQuestionOption(questionOption));
        }

        // DELETE: api/QuestionOptions/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionOptionDto>>>> DeleteQuestionOption(Guid id)
        {
            var response = await _questionOptionService.DeleteQuestionOption(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
