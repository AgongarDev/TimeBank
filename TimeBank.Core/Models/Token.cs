namespace TimeBank.Core.Models
{
    public class Token
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }

        public override string ToString()
        {
            return ID + " : " + Name + " Valid for " + Hours;
        }
    }
}