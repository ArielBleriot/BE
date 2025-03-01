namespace BridgeRTU.Services.Students.Dto
{
    public class StudentSignupDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UniversityName { get; set; }
        public string FieldOfStudy { get; set; }
        public List<string> PersonalInterests { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }

    }
}
