using System;

namespace WebAppHowTo.Data.Model
{
    public class Practice : EntityBase
    {
        public string Header { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}