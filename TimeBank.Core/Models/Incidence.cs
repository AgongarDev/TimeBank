using System;
using System.Collections.Generic;

namespace TimeBank.Core.Models
{
    public class Incidence
    {
        public long ID { get; set; }
        public DateTime IssueDate { get; set; }
        public string Description { get; set; }
        public bool Solved { get; set; }

        public long ServiceID { get; set; }
        public Service Service { get; set; }

        public List<Comment> Comments { get; set; }
    }
}