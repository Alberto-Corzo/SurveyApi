using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdRole { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string StrName { get; set; } = null!;

        //Inverse prop
        public RoleUser RoleUsers { get; set; }
    }
}
