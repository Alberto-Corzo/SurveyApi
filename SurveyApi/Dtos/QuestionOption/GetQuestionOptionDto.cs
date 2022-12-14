using SurveyApi.Dtos.Question;

namespace SurveyApi.Dtos.QuestionOption
{
    public class GetQuestionOptionDto
    {
        public Guid IdQuestionOption { get; set; }
        public string StrAnswerOption { get; set; } = null!;
        public bool Correct { get; set; }
        public bool Status { get; set; }
        public GetQuestionDto Question { get; set; }
    }
}
