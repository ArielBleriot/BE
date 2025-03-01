using BridgeRTU.Domain;

namespace BridgeRTU.Interfaces
{
    public interface ITokenService
    {
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<PasswordResetToken> GetPasswordResetToken(string token);
        Task MarkTokenAsUsedAsync(string token);
    }

}
