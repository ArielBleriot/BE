namespace BridgeRTU.Services.Students.Dto
{
    public class ConfigurePasswordRequestDto
    {
        public string Token { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }

}
