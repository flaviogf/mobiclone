using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.Account
{
    public class StoreAccountViewModel
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Type { get; set; }
    }
}
