using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("UserAnswer")]
    public class UserAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdUserAnswer { get; set; }
        [Unicode(false)]
        public string StrUserAnswer { get; set; } = null!;
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }

        //Relationships
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;
    }
}
