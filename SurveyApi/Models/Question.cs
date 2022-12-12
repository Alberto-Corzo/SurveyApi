using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("Question")]
    public class Question
    {
        public Question()
        {
            QuestionOptions = new HashSet<QuestionOption>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdQuestion { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string StrQuestion { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string StrQuestionType { get; set; } = null!;
        public int SurveyId { get; set; }

        //Foreign Key to Survey Table
        [ForeignKey("SurveyId")]
        public Survey Survey { get; set; } = null!;

        //Inverse prop
        public ICollection<QuestionOption> QuestionOptions { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
