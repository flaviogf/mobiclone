using System;

namespace Mobiclone.Api.Models
{
    public class Revenue
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public DateTime Date { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
