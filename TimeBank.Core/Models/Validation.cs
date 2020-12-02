using System;
using System.Collections.Generic;
using System.Text;

namespace TimeBank.Core.Models
{
    public class Validation
    {
        public long ID { get; set; }
        public bool Validated { get; set; }
        public DateTime ValidatedOn { get; set; }
        public DateTime ServiceUsedOn { get; set; }

        public long ServiceID { get; set; }
        public Service Service { get; set; }

        public long TBankUserID { get; set; }
        public User User { get; set; }

        public Payment Paid { get; set; }
        public List<Incidence> Incidences { get; set; }
    }
}
