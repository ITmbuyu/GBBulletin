using System.ComponentModel.DataAnnotations;

namespace GBBulletin.Models
{
    public class Roles
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
