using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName = "DateTime2")]
        public DateTime DateIni { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime DateEnd { get; set; }

        public override string ToString()
        {
            return $"{ServiceID} : {Name} from Category {CategoryID}: {Category.Name} - {Description}";
        }
    }
}