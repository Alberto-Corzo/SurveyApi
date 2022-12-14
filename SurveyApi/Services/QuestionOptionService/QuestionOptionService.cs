using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Category;
using SurveyApi.Dtos.QuestionOption;
using SurveyApi.Models;

namespace SurveyApi.Services.QuestionOptionService
{
    public class QuestionOptionService : IQuestionOptionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionOptionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetQuestionOptionDto>>> AddQuestionOption(AddQuestionOptionDto newQuestionOption)
        {
            var response = new ServiceResponse<List<GetQuestionOptionDto>>();

            QuestionOption quest_opt = _mapper.Map<QuestionOption>(newQuestionOption);
            _context.QuestionOption.Add(quest_opt);

            await _context.SaveChangesAsync();

            response.Data = await _context.QuestionOption
                .Include(q => q.Question)
                    .ThenInclude(s => s.Survey)
                        .ThenInclude(cat => cat.Category)
                .Select(qo => _mapper.Map<GetQuestionOptionDto>(qo)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetQuestionOptionDto>>> DeleteQuestionOption(Guid id)
        {
            ServiceResponse<List<GetQuestionOptionDto>> response = new ServiceResponse<List<GetQuestionOptionDto>>();
            try
            {
                QuestionOption quest_opt = await _context.QuestionOption
                    .Include(q => q.Question)
                        .ThenInclude(s => s.Survey)
                            .ThenInclude(cat => cat.Category)
                    .FirstOrDefaultAsync(qo => qo.IdQuestionOption.ToString().ToUpper() == id.ToString().ToUpper());

                if (quest_opt != null)
                {
                    _context.QuestionOption.Remove(quest_opt);
                    await _context.SaveChangesAsync();

                    response.Data = _context.QuestionOption.Select(qo => _mapper.Map<GetQuestionOptionDto>(qo)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question Option Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetQuestionOptionDto>>> GetAllQuestionOptions()
        {
            var response = new ServiceResponse<List<GetQuestionOptionDto>>();

            var question_options = await _context.QuestionOption
                .Include(q => q.Question)
                    .ThenInclude(s => s.Survey)
                        .ThenInclude(cat => cat.Category)
                .ToListAsync();

            response.Data = question_options.Select(qo => _mapper.Map<GetQuestionOptionDto>(qo)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetQuestionOptionDto>> GetQuestionOptionById(Guid id)
        {
            var response = new ServiceResponse<GetQuestionOptionDto>();
            var question_opt = await _context.QuestionOption
                .Include(q => q.Question)
                    .ThenInclude(s => s.Survey)
                        .ThenInclude(cat => cat.Category)
                .FirstOrDefaultAsync(qo => qo.IdQuestionOption.ToString().ToUpper() == id.ToString().ToUpper());

            if (question_opt != null)
            {
                response.Data = _mapper.Map<GetQuestionOptionDto>(question_opt);
            }
            else
            {
                response.Success = false;
                response.Message = "Question Option Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetQuestionOptionDto>> UpdateQuestionOption(UpdateQuestionOptionDto updatedQuestionOption, Guid id)
        {
            ServiceResponse<GetQuestionOptionDto> response = new ServiceResponse<GetQuestionOptionDto>();

            try
            {
                var question_opt = await _context.QuestionOption
                    .Include(q => q.Question)
                        .ThenInclude(s => s.Survey)
                            .ThenInclude(cat => cat.Category)
                    .FirstOrDefaultAsync(qo => qo.IdQuestionOption.ToString().ToUpper() == id.ToString().ToUpper());

                if (question_opt != null)
                {
                    _mapper.Map(updatedQuestionOption, question_opt);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetQuestionOptionDto>(question_opt);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question Option Not Found";
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
