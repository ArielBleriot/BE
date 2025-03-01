namespace BridgeRTU.Services.Activities.Dto
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public List<string> Interests { get; set; }  // Human-readable interest names
    }

}
