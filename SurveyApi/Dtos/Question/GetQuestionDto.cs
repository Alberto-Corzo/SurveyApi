using SurveyApi.Dtos.Survey;

namespace SurveyApi.Dtos.Question
{
    public class GetQuestionDto
    {
        public Guid IdQuestion { get; set; }
        public string StrQuestion { get; set; } = null!;
        public string StrQuestionType { get; set; } = null!;
        public GetSurveyDto Survey { get; set; }
        //public GetQuestionOptionDto QuestionOption { get; set; }
    }
}
