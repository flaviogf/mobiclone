using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.Account
{
    public class UpdateAccountViewModel
    {
        [StringLength(250)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        [Required]
        public string Type { get; set; }
    }
}
