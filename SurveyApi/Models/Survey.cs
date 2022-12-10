using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("Survey")]
    public class Survey
    {
        public Survey()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSurvey { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string strName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime RegisterDate { get; set; }
        public bool Status { get; set; }
        public Guid CategoryId { get; set; }

        //Foreign Key to the table Category
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        
        //Inverse prop to the table Question
        public ICollection<Question> Questions { get; set; }

    }
}
