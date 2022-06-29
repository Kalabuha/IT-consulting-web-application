using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.Entities.Base;

namespace Resources.Entities
{
    [Table("Home_page_images")]
    public class HomePageImageEntity : PageEntity
    {
        [Column("Image"), Required]
        public byte[] ImageAsArray64 { get; set; } = default!;
    }
}
