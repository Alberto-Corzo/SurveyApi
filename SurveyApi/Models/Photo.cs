using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("Photo")]
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdPhoto { get; set; }
        public byte[] Image { get; set; } = null!;

        //Inverse prop to User table
        public User User { get; set; } = null!;
    }
}
