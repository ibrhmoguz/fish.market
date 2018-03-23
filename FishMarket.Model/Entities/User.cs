using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishMarket.Model.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName("E-Mail")]
        [Required]
        public string Email { get; set; }

        public virtual ICollection<Fish> Fishes { get; set; }
    }
}
