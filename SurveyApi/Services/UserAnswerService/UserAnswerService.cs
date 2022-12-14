using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Category;
using SurveyApi.Dtos.UserAnswer;
using SurveyApi.Models;

namespace SurveyApi.Services.UserAnswerService
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserAnswerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetUserAnswerDto>>> AddUserAnswer(AddUserAnswerDto newUserAnswer)
        {
            var response = new ServiceResponse<List<GetUserAnswerDto>>();

            UserAnswer u_answ = _mapper.Map<UserAnswer>(newUserAnswer);
            _context.UserAnswer.Add(u_answ);
            await _context.SaveChangesAsync();

            response.Data = await _context.UserAnswer
                .Include(u => u.User)
                    .ThenInclude(u => u.Roles)
                .Include(u => u.User.Photo)
                .Include(q => q.Question)
                    .ThenInclude(s => s.Survey)
                        .ThenInclude(cat => cat.Category)
                .Select(ua => _mapper.Map<GetUserAnswerDto>(ua)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetUserAnswerDto>>> DeleteUserAnswer(Guid id)
        {
            ServiceResponse<List<GetUserAnswerDto>> response = new ServiceResponse<List<GetUserAnswerDto>>();
            try
            {
                UserAnswer u_answ = await _context.UserAnswer
                    .FirstOrDefaultAsync(ua => ua.IdUserAnswer.ToString().ToUpper() == id.ToString().ToUpper());

                if (u_answ != null)
                {
                    _context.UserAnswer.Remove(u_answ);
                    await _context.SaveChangesAsync();

                    response.Data = _context.UserAnswer
                        .Include(u => u.User)
                            .ThenInclude(u => u.Roles)
                        .Include(u => u.User.Photo)
                        .Include(q => q.Question)
                            .ThenInclude(s => s.Survey)
                                .ThenInclude(cat => cat.Category)
                        .Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Answer Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetUserAnswerDto>>> GetAllUserAnswers()
        {
            var response = new ServiceResponse<List<GetUserAnswerDto>>();

            var u_answers = await _context.UserAnswer
                .Include(u => u.User)
                    .ThenInclude(u => u.Roles)
                .Include(u => u.User.Photo)
                .Include(q => q.Question)
                    .ThenInclude(s => s.Survey)
                        .ThenInclude(cat => cat.Category)
                .ToListAsync();

            response.Data = u_answers.Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetUserAnswerDto>> GetUserAnswerById(Guid id)
        {
            var response = new ServiceResponse<GetUserAnswerDto>();

            var u_answ = await _context.UserAnswer
                .Include(u => u.User)
                    .ThenInclude(u => u.Roles)
                .Include(u => u.User.Photo)
                .Include(q => q.Question)
                    .ThenInclude(s => s.Survey)
                        .ThenInclude(cat => cat.Category)
                .FirstOrDefaultAsync(ua => ua.IdUserAnswer.ToString().ToUpper() == id.ToString().ToUpper());

            
            if (u_answ != null)
            {
                response.Data = _mapper.Map<GetUserAnswerDto>(u_answ);
            }
            else
            {
                response.Success = false;
                response.Message = "User Answer Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetUserAnswerDto>> UpdateUserAnswer(UpdateUserAnswerDto updatedUserAnswer, Guid id)
        {
            ServiceResponse<GetUserAnswerDto> response = new ServiceResponse<GetUserAnswerDto>();

            try
            {
                var u_answer = await _context.UserAnswer
                    .Include(u => u.User)
                        .ThenInclude(u => u.Roles)
                    .Include(u => u.User.Photo)
                    .Include(q => q.Question)
                        .ThenInclude(s => s.Survey)
                            .ThenInclude(cat => cat.Category)
                    .FirstOrDefaultAsync(c => c.IdUserAnswer.ToString().ToUpper() == id.ToString().ToUpper());

                if (u_answer != null)
                {
                    _mapper.Map(updatedUserAnswer, u_answer);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetUserAnswerDto>(u_answer);
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Answer Not Found";
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
