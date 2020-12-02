using System.Collections.Generic;

namespace TimeBank.Core.Models
{
    public class Service
    {
        public long ServiceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        
        public long ProviderID { get; set; }
        public User Provider { get; set; }
        
        public Validation Validation { get; set; }
        public List<Token> Price { get; set; }
    }
}