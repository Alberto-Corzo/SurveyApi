namespace SurveyApi.Dtos.Photo
{
    public class GetPhotoDto
    {
        public Guid IdPhoto { get; set; }
        public byte[] Image { get; set; } = null!;
    }
}
