namespace BridgeRTU.Services.Students.Dto
{
    public class StudentSearchDto
    {
        public string? UniversityName { get; set; }
        public List<string>? Interests { get; set; }
        public string? FieldOfStudy { get; set; }
    }

    public class StudentSearchResultDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UniversityName { get; set; }
        public string FieldOfStudy { get; set; }
        public string Email { get; set; }
        public List<string> PersonalInterests { get; set; }
    }
}
