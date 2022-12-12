namespace SurveyApi.Dtos.Survey
{
    public class UpdateSurveyDto
    {
        public string strName { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public bool Status { get; set; }
        public Guid CategoryId { get; set; }
    }
}
