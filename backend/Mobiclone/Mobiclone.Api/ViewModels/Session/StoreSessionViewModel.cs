using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.Session
{
    public class StoreSessionViewModel
    {
        [StringLength(255)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(255)]
        [Required]
        public string Password { get; set; }
    }
}
