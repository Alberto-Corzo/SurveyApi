namespace SurveyApi.Dtos.UserAnswer
{
    public class UpdateUserAnswerDto
    {
        public string StrUserAnswer { get; set; } = null!;
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
