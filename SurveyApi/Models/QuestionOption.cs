using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("QuestionOption")]
    public class QuestionOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdQuestionOption { get; set; }
        [Unicode(false)]
        public string StrAnswerOption { get; set; } = null!;
        public bool Correct { get; set; }
        public bool Status { get; set; }
        public Guid QuestionId { get; set; }

        //Relationship to Question Table
        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;

    }
}
