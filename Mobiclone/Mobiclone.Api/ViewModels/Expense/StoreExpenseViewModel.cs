using System;
using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.ViewModels.Expense
{
    public class StoreExpenseViewModel
    {
        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
