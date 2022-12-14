using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Dtos.Question;
using SurveyApi.Models;
using SurveyApi.Services.QuestionService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        // GET: api/Questions
        [HttpGet, Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionDto>>>> GetQuestion()
        {
            return Ok(await _questionService.GetAllQuestions());
        }

        // GET: api/Questions/5
        [HttpGet("{id}"), Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> GetQuestionById(Guid id)
        {
            var response = await _questionService.GetQuestionById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> PutQuestion(Guid id, UpdateQuestionDto question)
        {
            var response = await _questionService.UpdateQuestion(question, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionDto>>>> PostQuestion(AddQuestionDto question)
        {
            return Ok(await _questionService.AddQuestion(question));
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionDto>>>> DeleteQuestion(Guid id)
        {
            var response = await _questionService.DeleteQuestion(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
