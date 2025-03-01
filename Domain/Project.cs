namespace BridgeRTU.Domain
{
    public class Project
    {
        // Unique identifier for the project
        public int Id { get; set; }

        // Title of the project
        public string Title { get; set; }

        // Detailed description of the project
        public string Description { get; set; }

        // The status of the project (e.g., 'In Progress', 'Completed')
        public string Status { get; set; }
        public List<string> Skills { get; set; }
        public string FieldOfStudy { get; set; }

        // The date when the project was created
        public DateTime CreatedAt { get; set; }

        // The date when the project was last updated
        public DateTime UpdatedAt { get; set; }
        public string? ImageUrl { get; set; }

        // Foreign key to Student (or User) entity
        public int StudentId { get; set; }

        // Navigation property to link with the Student (if you have a Student entity)
        public virtual Student Student { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

        // Constructor to set default values
        public Project()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
