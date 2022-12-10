using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            Surveys = new HashSet<Survey>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdCategory { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string StrName { get; set; } = string.Empty;
        
        //Inverse property to Survey table
        public ICollection<Survey> Surveys { get; set; }
    }
}
