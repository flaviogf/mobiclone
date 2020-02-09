using System;
using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.Revenue
{
    public class StoreRevenueViewModel
    {
        [StringLength(250)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
