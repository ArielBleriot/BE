using BridgeRTU.Services.Emails;

namespace BridgeRTU.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDto emailDTO);
    }

}
