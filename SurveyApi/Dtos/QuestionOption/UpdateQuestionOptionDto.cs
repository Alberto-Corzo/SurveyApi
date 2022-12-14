namespace SurveyApi.Dtos.QuestionOption
{
    public class UpdateQuestionOptionDto
    {
        public string StrAnswerOption { get; set; } = null!;
        public bool Correct { get; set; }
        public bool Status { get; set; }
        public Guid QuestionId { get; set; }
    }
}
