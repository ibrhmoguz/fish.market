using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishMarket.Model.Entities
{
    public class Fish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FishId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Fish Name")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "*.##")]
        public double Price { get; set; }
        public byte[] ImageData { get; set; }

        [MaxLength(10)]
        public string ImageMimeType { get; set; }

        public virtual User User { get; set; }
    }
}
