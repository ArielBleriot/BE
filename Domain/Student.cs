namespace BridgeRTU.Domain
{
    public class Student
    {
        public int Id { get; set; }  // Unique identifier for the student
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UniversityName { get; set; }
        public string FieldOfStudy { get; set; }
        public List<string> PersonalInterests { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }  // In a real application, this should be hashed
        public bool RememberMe { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}
