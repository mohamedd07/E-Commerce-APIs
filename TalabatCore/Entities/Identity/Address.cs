namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string street  { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string AppUserId { get; set; }

    }
}