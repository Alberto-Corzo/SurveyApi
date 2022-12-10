using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {
            UserAnswers = new HashSet<UserAnswer>();
            //RoleUsers = new HashSet<RoleUser>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdUser { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string StrName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string StrFirstSurname { get; set; } = string.Empty;
        [StringLength(50)]
        [Unicode(false)]
        public string? StrLastSurname { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        //public byte[]? Photo { get; set; }
        public Guid? PhotoId { get; set; }
        public bool? Status { get; set; }

        //Foreign Key Photo Table
        [ForeignKey("PhotoId")]
        public Photo? Photo { get; set; }

        //Inverse prop
        public ICollection<UserAnswer> UserAnswers { get; set; }
        //public ICollection<RoleUser> RoleUsers { get; set; }
    }
}
