namespace BridgeRTU.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int StudentId { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public DateTime PostingDate { get; set; }
    }
}
