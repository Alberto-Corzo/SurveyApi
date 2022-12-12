using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SurveyApi.Dtos.Category;

namespace SurveyApi.Dtos.Survey
{
    public class GetSurveyDto
    {
        public int IdSurvey { get; set; }
        public string strName { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public bool Status { get; set; }
        //public Guid CategoryId { get; set; }

        public GetCategoryDto Category  { get; set; }
    }
}
