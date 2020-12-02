using System;
using System.Collections.Generic;
using System.Text;

namespace TimeBank.Core.Models
{
    public class Address
    {
        public long ID { get; set; }
        public string TypeStreet { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Comments { get; set; }
       
        public User user { get; set; }
    }
}
