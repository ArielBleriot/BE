namespace BridgeRTU.Domain
{
    public class Activity
    {
        public int Id { get; set; }  // Unique identifier for the activity
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public int SeatsLeft{ get; set; }
        public int NoOfSeats { get; set; }
        public DateTime Date { get; set; }  // When the activity takes place
        public string Location { get; set; }  // Can be a specific address or general place
        public List<string> Interest { get; set; }
    }

}
