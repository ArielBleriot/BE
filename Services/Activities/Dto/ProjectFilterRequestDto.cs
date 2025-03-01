namespace BridgeRTU.Services.Activities.Dto
{
    public class ProjectFilterRequestDto
    {
        public string? Location { get; set; }
        public DateTime? StartDate { get; set; }
        public bool BasedOnInterest { get; set; }
    }
}
