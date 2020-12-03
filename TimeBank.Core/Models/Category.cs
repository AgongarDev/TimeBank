using System;
using System.Collections.Generic;
using System.Text;

namespace TimeBank.Core.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Service> Services { get; set; }

        public override string ToString()
        {
            return ID + " : " + Name;
        }
    }

}
