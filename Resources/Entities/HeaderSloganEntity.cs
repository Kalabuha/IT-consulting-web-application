﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.Entities.Base;

namespace Resources.Entities
{
    [Table("Header_slogans")]

    public class HeaderSloganEntity : BaseEntity
    {
        [Column("Slogans"), Required, MaxLength(24)]
        public string Slogan { get; set; } = default!;

        [Column("Slogan_used"), Required]
        public bool IsUsed { get; set; }
    }
}
