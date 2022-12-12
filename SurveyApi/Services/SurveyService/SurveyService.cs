using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Survey;
using SurveyApi.Models;

namespace SurveyApi.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SurveyService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetSurveyDto>>> AddSurvey(AddSurveyDto newSurvey)
        {
            var response = new ServiceResponse<List<GetSurveyDto>>();

            Survey survey = _mapper.Map<Survey>(newSurvey);
            _context.Survey.Add(survey);

            await _context.SaveChangesAsync();

            response.Data = await _context.Survey
                .Include(cat => cat.Category)
                .Select(s => _mapper.Map<GetSurveyDto>(s)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetSurveyDto>>> DeleteSurvey(int id)
        {
            ServiceResponse<List<GetSurveyDto>> response = new ServiceResponse<List<GetSurveyDto>>();
            try
            {
                Survey survey = await _context.Survey
                    .Include(cat => cat.Category)
                    .FirstOrDefaultAsync(s => s.IdSurvey == id);

                if (survey != null)
                {
                    _context.Survey.Remove(survey);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Survey.Select(s => _mapper.Map<GetSurveyDto>(s)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Survey Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetSurveyDto>>> GetAllSurveys()
        {
            var response = new ServiceResponse<List<GetSurveyDto>>();

            var surveys = await _context.Survey
                .Include(cat => cat.Category)
                .ToListAsync();

            response.Data = surveys.Select(c => _mapper.Map<GetSurveyDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetSurveyDto>> GetSurveyById(int id)
        {
            var response = new ServiceResponse<GetSurveyDto>();
            var survey = await _context.Survey
                .Include(cat => cat.Category)
                .FirstOrDefaultAsync(s => s.IdSurvey == id);

            if (survey != null)
            {
                response.Data = _mapper.Map<GetSurveyDto>(survey);
            }
            else
            {
                response.Success = false;
                response.Message = "Survey Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetSurveyDto>> UpdateSurvey(UpdateSurveyDto updatedSurvey, int id)
        {
            ServiceResponse<GetSurveyDto> response = new ServiceResponse<GetSurveyDto>();

            try
            {
                var survey = await _context.Survey
                    .Include(cat => cat.Category)
                    .FirstOrDefaultAsync(c => c.IdSurvey == id);

                if (survey != null)
                {
                    _mapper.Map(updatedSurvey, survey);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetSurveyDto>(survey);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Survey Not Found";
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
