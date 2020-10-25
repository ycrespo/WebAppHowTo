using System.Collections.Generic;

namespace WebAppHowTo.Data.Model
{
    public class Customer : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public IEnumerable<Practice> Practices { get; set; }
    }
}