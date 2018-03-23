using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishMarket.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [DisplayName("E-Mail")]
        [Required]
        public string Email { get; set; }
    }
}
