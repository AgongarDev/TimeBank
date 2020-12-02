using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Core.Enums;

namespace TimeBank.Core.Models
{
    public class Payment
    {
        public long ID { get; set; }
        public long TBankUserID { get; set; }
        public User User { get; set; }

        public long ValidationId { get; set; }
        public Validation Validation { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
