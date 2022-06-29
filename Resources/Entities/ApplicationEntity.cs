using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.Entities.Base;
using Resources.Enums;

namespace Resources.Entities
{
    [Table("Guests_applications")]
    public class ApplicationEntity : BaseEntity
    {
        [Column("Guest_name"), Required, MaxLength(60)]
        public string GuestName { get; set; } = default!;

        [Column("Guest_email"), Required, MaxLength(100)]
        public string GuestEmail { get; set; } = default!;

        [Column("Guests_application_text"), MaxLength(4000), Required]
        public string GuestsApplicationText { get; set; } = default!;

        [Column("Date_receipt_application"), Required]
        public DateTime DateReceiptApplication { get; set; }

        [Column("Application_processing_status"), Required]
        public ApplicationStatus Status { get; set; }
    }
}
