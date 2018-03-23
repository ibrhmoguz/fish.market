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

        [MaxLength(50)]
        [Required]
        public string Password { get; set; }

        [DisplayName("E-Mail")]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        public string ActivationCode { get; set; }
        public bool ActivationStatus { get; set; }

        public virtual ICollection<Fish> Fishes { get; set; }
    }
}
