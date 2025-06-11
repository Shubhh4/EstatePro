using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstatePro.Models
{
    public class PropertyImage
    {
        [Key]
        public int ImageId { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
