namespace BridgeRTU.Services.Students.Dto
{
    public class StudentProfileUpdateDto
    {
        public string FullName { get; set; }
        public string UniversityName { get; set; }
        public string FieldOfStudy { get; set; }
        public List<string> PersonalInterests { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
