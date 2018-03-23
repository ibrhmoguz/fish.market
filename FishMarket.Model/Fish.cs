using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishMarket.Model
{
    public class Fish
    {
        [Key]
        public int FishId { get; set; }
        public int UserId { get; set; }
        [DisplayName("Fish Name")]
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImageData { get; set; }

        public virtual User User { get; set; }
    }
}
