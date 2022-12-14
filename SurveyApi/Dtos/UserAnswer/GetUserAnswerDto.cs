using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Dtos.Question;

namespace SurveyApi.Dtos.UserAnswer
{
    public class GetUserAnswerDto
    {
        public Guid IdUserAnswer { get; set; }
        public string StrUserAnswer { get; set; } = null!;

        public GetUserDto User { get; set; }
        public GetQuestionDto Question { get; set; }
    }
}
