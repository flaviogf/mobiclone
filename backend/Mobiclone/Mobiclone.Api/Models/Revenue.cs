using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobiclone.Api.Models
{
    public class Revenue
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Value { get; set; }

        [Timestamp]
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Account")]
        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
