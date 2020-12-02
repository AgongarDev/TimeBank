using System;

namespace TimeBank.Core.Models
{
    public class Comment
    {
        public long ID { get; set; }
        public DateTime CommentDate { get; set; }
        public string Text { get; set; }

        public long IncidenceID { get; set; }
        public Incidence Incidence { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}