using System;
using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.Transfer
{
    public class StoreTransferViewModel
    {
        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ToId { get; set; }

        [Required]
        public int FromId { get; set; }
    }
}
