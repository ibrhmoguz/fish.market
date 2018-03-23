using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishMarket.Model.Entities
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
        [DisplayFormat(DataFormatString = "*.##")]
        public double Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public virtual User User { get; set; }
    }
}
