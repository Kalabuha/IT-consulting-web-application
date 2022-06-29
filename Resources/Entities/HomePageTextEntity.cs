using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.Entities.Base;

namespace Resources.Entities
{
    [Table("Home_page_text")]
    public class HomePageTextEntity : PageEntity
    {
        [Column("Text"), MaxLength(4000), Required]
        public string Text { get; set; } = default!;
    }
}
