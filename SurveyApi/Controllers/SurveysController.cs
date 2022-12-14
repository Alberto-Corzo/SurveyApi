using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Survey;
using SurveyApi.Models;
using SurveyApi.Services.SurveyService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IMapper _mapper;

        public SurveysController(ISurveyService surveyService, IMapper mapper)
        {
            _surveyService = surveyService;
            _mapper = mapper;
        }

        // GET: api/Surveys
        [HttpGet, Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetSurveyDto>>>> GetSurvey()
        {
            return Ok(await _surveyService.GetAllSurveys());
        }

        // GET: api/Surveys/5
        [HttpGet("{id}"), Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> GetSurvey(int id)
        {
            var response = await _surveyService.GetSurveyById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/Surveys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> PutSurvey(int id, UpdateSurveyDto survey)
        {
            var response = await _surveyService.UpdateSurvey(survey, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Surveys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetSurveyDto>>>> PostSurvey(AddSurveyDto survey)
        {
            return Ok(await _surveyService.AddSurvey(survey));
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetSurveyDto>>>> DeleteSurvey(int id)
        {
            var response = await _surveyService.DeleteSurvey(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
