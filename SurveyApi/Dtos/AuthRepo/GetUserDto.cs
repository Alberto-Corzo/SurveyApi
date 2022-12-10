namespace SurveyApi.Dtos.AuthRepo
{
    public class GetUserDto
    {
        public Guid IdUser { get; set; }
        public string StrName { get; set; } = null!;
        public string StrFirstSurname { get; set; } = string.Empty;
        public string? StrLastSurname { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public Guid PhotoId { get; set; }

        //public GetPhotoDto? Photo { get; set; }
    }
}
