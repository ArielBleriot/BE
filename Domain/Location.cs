namespace BridgeRTU.Domain
{
    public class Location
    {
        public int Id { get; set; }  // Unique identifier for the location
        public string Name { get; set; }  // e.g., "Main Campus", "Sports Hall"
        public string Address { get; set; }  // Physical address or description of the location
    }

}
