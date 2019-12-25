using System;
using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.Models
{
    public class Transition
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
