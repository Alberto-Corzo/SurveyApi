namespace SurveyApi.Dtos.UserAnswer
{
    public class AddUserAnswerDto
    {
        public string StrUserAnswer { get; set; } = null!;
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
