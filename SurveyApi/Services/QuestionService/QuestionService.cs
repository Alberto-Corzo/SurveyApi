using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Question;
using SurveyApi.Models;

namespace SurveyApi.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetQuestionDto>>> AddQuestion(AddQuestionDto newQuestion)
        {
            var response = new ServiceResponse<List<GetQuestionDto>>();

            Question question = _mapper.Map<Question>(newQuestion);
            _context.Question.Add(question);

            await _context.SaveChangesAsync();

            response.Data = await _context.Question
                .Include(s => s.Survey)
                .Select(q => _mapper.Map<GetQuestionDto>(q)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetQuestionDto>>> DeleteQuestion(Guid id)
        {
            ServiceResponse<List<GetQuestionDto>> response = new ServiceResponse<List<GetQuestionDto>>();
            try
            {
                Question question = await _context.Question
                    .FirstOrDefaultAsync(q => q.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

                if (question != null)
                {
                    _context.Question.Remove(question);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Question
                        .Include(s => s.Survey)
                        .Select(q => _mapper.Map<GetQuestionDto>(q)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetQuestionDto>>> GetAllQuestions()
        {
            var response = new ServiceResponse<List<GetQuestionDto>>();

            var questions = await _context.Question
                .Include(s => s.Survey)
                .ToListAsync();

            response.Data = questions.Select(c => _mapper.Map<GetQuestionDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetQuestionDto>> GetQuestionById(Guid id)
        {
            var response = new ServiceResponse<GetQuestionDto>();
            var question = await _context.Question
                .Include(s => s.Survey)
                .FirstOrDefaultAsync(q => q.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

            if (question != null)
            {
                response.Data = _mapper.Map<GetQuestionDto>(question);
            }
            else
            {
                response.Success = false;
                response.Message = "Question Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetQuestionDto>> UpdateQuestion(UpdateQuestionDto updatedQuestion, Guid id)
        {
            ServiceResponse<GetQuestionDto> response = new ServiceResponse<GetQuestionDto>();

            try
            {
                var question = await _context.Question
                    .Include(s => s.Survey)
                    .FirstOrDefaultAsync(c => c.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

                if (question != null)
                {
                    _mapper.Map(updatedQuestion, question);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetQuestionDto>(question);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question Not Found";
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
