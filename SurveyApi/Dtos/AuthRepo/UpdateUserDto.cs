namespace SurveyApi.Dtos.AuthRepo
{
    public class UpdateUserDto
    {
        public string StrName { get; set; } = null!;
        public string StrFirstSurname { get; set; } = string.Empty;
        public string? StrLastSurname { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool? Status { get; set; }
        public Guid? PhotoId { get; set; }
    }
}
