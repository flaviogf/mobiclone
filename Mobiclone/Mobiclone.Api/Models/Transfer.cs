using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobiclone.Api.Models
{
    public class Transfer : Transition
    {
        [ForeignKey("To")]
        [Required]
        public int ToId { get; set; }

        public Account To { get; set; }

        [ForeignKey("From")]
        [Required]
        public int FromId { get; set; }

        public Account From { get; set; }
    }
}
