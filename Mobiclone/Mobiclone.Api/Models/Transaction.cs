using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobiclone.Api.Models
{
    public class Transaction: Transition
    {
        [ForeignKey("Account")]
        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
