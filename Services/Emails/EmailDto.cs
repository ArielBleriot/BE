namespace BridgeRTU.Services.Emails
{
    public class EmailDto
    {
        public string? From { get; set; }
        public string To { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
    }

}
