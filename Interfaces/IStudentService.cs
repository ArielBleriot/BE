using BridgeRTU.Domain;
using BridgeRTU.Services.Students.Dto;

namespace BridgeRTU.Interfaces
{
    public interface IStudentService
    {
        Task<Student> CreateUserAsync(StudentSignupDto userDto);
        Task<Student> GetProfile(int userId);
        Task<Tuple<int, string, string, string, string>> LoginAsync(string email, string password);
        Task<bool> GeneratePasswordResetLinkAsync(string email);
        Task<Tuple<bool,string>> RegisterForActivity(ActivityRegistrationRequestDto registration);
        Task<ConfigurePasswordResponseDto> ConfigurePassword(ConfigurePasswordRequestDto request);
        Task<List<StudentSearchResultDto>> SearchStudentsAsync(StudentSearchDto searchDto);
    }
}
