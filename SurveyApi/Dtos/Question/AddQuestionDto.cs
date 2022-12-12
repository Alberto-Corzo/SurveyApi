namespace SurveyApi.Dtos.Question
{
    public class AddQuestionDto
    {
        public Guid IdQuestion { get; set; }
        public string StrQuestion { get; set; } = null!;
        public string StrQuestionType { get; set; } = null!;
        public int SurveyId { get; set; }
    }
}
