namespace BridgeRTU.Services.Comments.Dto
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public int ProjectId { get; set; }
        public int StudentId { get; set; }
        public string Description { get; set; } = null!;
        public string StudentName { get; set; } = null!;
        public DateTime? PostingDate { get; set; }
    }
}
