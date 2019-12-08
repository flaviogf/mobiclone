using System;

namespace Mobiclone.Api.ViewModels.Expense
{
    public class StoreExpenseViewModel
    {
        public string Description { get; set; }

        public int Value { get; set; }

        public DateTime Date { get; set; }
    }
}
