using BridgeRTU.Domain;
using BridgeRTU.Services.Comments.Dto;

namespace BridgeRTU.Services.Projects.Dto
{
    public class ProjectDto
    {
        // Title of the project
        public string Title { get; set; }

        // Detailed description of the project
        public string Description { get; set; }

        // The status of the project (e.g., 'In Progress', 'Completed')
        public string Status { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Interests { get; set; }
        public string? FieldOfStudy { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public string? StudentName { get; set; }
    }

    public class ProjectDetailsDto : ProjectDto
    {
        // The unique identifier of the project
        public int Id { get; set; }

        // The date when the project was created
        public DateTime? CreatedAt { get; set; }

        // The date when the project was last updated
        public DateTime? UpdatedAt { get; set; }

        // The student ID (for referencing which student created the project)
        public int StudentId { get; set; }
    }
}
