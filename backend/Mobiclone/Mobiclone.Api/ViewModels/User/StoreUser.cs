using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.User
{
    public class StoreUserViewModel
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(255)]
        [Required]
        public string Password { get; set; }
    }
}
